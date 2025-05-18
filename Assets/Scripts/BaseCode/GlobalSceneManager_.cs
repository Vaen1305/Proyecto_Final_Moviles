using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class GlobalSceneManager_s : MonoBehaviour
{
    public static Action<string> OnSceneLoaded;
    public static Action<string> OnSceneUnloaded;

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(sceneName));
        
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => operation.isDone);
        OnSceneLoaded?.Invoke(sceneName);

        yield return null;

        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.IsValid())
        {
            SceneManager.SetActiveScene(loadedScene);
        }

        UnloadAllScenesExceptActive();
    }

    public void UnloadScenes(string sceneName)
    {
        StartCoroutine(UnloadSceneAsync(sceneName));
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
        yield return new WaitUntil(() => operation.isDone);
        OnSceneUnloaded?.Invoke(sceneName);
    }

    public void UnloadAllScenesExceptActive()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene != activeScene)
            {
                UnloadScenes(scene.name);
            }
        }
    }
}
