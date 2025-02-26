using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonSceneChanger : MonoBehaviour
{
    [Header("Target Scene")]
    public string targetSceneName; 

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("None Target Scene Name");
        }
    }
}