using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoSetup : MonoBehaviour
{
    [SerializeField] private string fileName = "video.mp4";
    private VideoPlayer vp;

    public bool IsPrepared => vp && vp.isPrepared;

    private void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        vp.playOnAwake = false;          // quem manda dar Play é o tracking
    }

    private IEnumerator Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string url = Path.Combine(Application.streamingAssetsPath, fileName);   // jar:file://...
#else
        string url = "file://" + Path.Combine(Application.streamingAssetsPath, fileName);
#endif
        vp.url = url;

        vp.Prepare();
        while (!vp.isPrepared) yield return null;
        // Não toca aqui — aguardamos o ModelTarget dizer “play”.
    }
}
