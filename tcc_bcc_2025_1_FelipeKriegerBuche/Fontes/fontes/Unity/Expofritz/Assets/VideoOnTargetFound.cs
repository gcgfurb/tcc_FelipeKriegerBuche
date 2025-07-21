using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

/// Dá Play quando o alvo é rastreado e Pause quando perde.
/// Esconde/mostra automaticamente o Renderer usado pelo VideoPlayer.
public class VideoOnTargetFound : MonoBehaviour
{
    [Header("VideoPlayer")]
    [SerializeField] private VideoPlayer videoPlayer;

    private Renderer screenRenderer;      // encontrado automaticamente
    private VideoSetup setup;
    private bool wantPlay;
    private Coroutine waitCoroutine;

    private void Awake()
    {
        var obs = GetComponent<ObserverBehaviour>();
        if (obs) obs.OnTargetStatusChanged += OnStatusChanged;

        setup = videoPlayer.GetComponent<VideoSetup>();

        // Descobre o renderer que o VideoPlayer já está usando
        screenRenderer = videoPlayer.targetMaterialRenderer 
                         ?? videoPlayer.GetComponent<Renderer>();

        if (screenRenderer) screenRenderer.enabled = false;
    }

    private void OnStatusChanged(ObserverBehaviour _, TargetStatus status)
    {
        bool tracked = status.Status == Status.TRACKED ||
                       status.Status == Status.EXTENDED_TRACKED;

        if (tracked)
        {
            wantPlay = true;
            if (screenRenderer) screenRenderer.enabled = true;

            if (setup.IsPrepared)
                videoPlayer.Play();
            else if (waitCoroutine == null)
                waitCoroutine = StartCoroutine(PlayWhenPrepared());
        }
        else
        {
            wantPlay = false;
            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            videoPlayer.Pause();
            if (screenRenderer) screenRenderer.enabled = false;
        }
    }

    private IEnumerator PlayWhenPrepared()
    {
        while (!setup.IsPrepared)
            yield return null;
        if (wantPlay) videoPlayer.Play();
        waitCoroutine = null;
    }
}
