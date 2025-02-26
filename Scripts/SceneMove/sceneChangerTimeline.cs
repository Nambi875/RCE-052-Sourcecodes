using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangerTimeline : MonoBehaviour
{
    public string mainMenuSceneName;

    public void RestartToMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            GameMng.Getins.ResetScore();
            ClearDontDestroyOnLoad();

            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    private void ClearDontDestroyOnLoad()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var obj in rootObjects)
        {
            if (obj.scene.name == null || obj.scene.name == "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }

        System.GC.Collect();
    }
}