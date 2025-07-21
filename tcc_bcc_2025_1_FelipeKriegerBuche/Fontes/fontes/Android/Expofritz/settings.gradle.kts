pluginManagement {
    repositories {
        google {
            content {
                includeGroupByRegex("com\\.android.*")
                includeGroupByRegex("com\\.google.*")
                includeGroupByRegex("androidx.*")
            }
        }
        mavenCentral()
        gradlePluginPortal()
    }
}

include(":unityLibrary")
project(":unityLibrary").projectDir = file("unityLibrary")


dependencyResolutionManagement {
    repositoriesMode.set(RepositoriesMode.FAIL_ON_PROJECT_REPOS)
    repositories {
        google()
        mavenCentral()
        maven { url = uri("https://jitpack.io") }
        mavenLocal()
        flatDir {
            dirs("${project(":unityLibrary").projectDir}/libs")
        }
    }
}

rootProject.name = "Expofritz"
include(":app")