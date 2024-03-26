using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public void LoadScene()
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (IsSceneLoaded(sceneName))
        {
            Debug.Log($"{sceneName} is already loaded.");
            return;
        }

        SceneManager.LoadScene(sceneName);
        Debug.Log($"Loading {sceneName}.");
    }
     
    public static bool IsSceneLoaded(string sceneName)
    {
#if UNITY_EDITOR
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
#else
        return SceneManager.GetSceneByName(sceneName).isLoaded;
#endif
    }
}