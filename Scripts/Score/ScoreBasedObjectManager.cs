using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBasedObjectManager : MonoBehaviour
{
    [Header("Score-Based Objects")]
    public GameObject[] scoreObjects; 

    private int currentScore = -1; //ôøÑ¢ûù
    private void Start()
    {
        UpdateObjects(GameMng.Getins.score);  //ôøÑ¢ûù
    }
    public void UpdateScoreBasedObjects()
    {
        if (GameMng.Getins != null && GameMng.Getins.score != currentScore)
        {
            UpdateObjects(GameMng.Getins.score);
        }
    }
    private void UpdateObjects(int newScore)
    {
        if (currentScore >= 0 && currentScore < scoreObjects.Length)
        {
            if (scoreObjects[currentScore] != null)
            {
                scoreObjects[currentScore].SetActive(false);
            }
        }

        if (newScore >= 0 && newScore < scoreObjects.Length)
        {
            if (scoreObjects[newScore] != null)
            {
                scoreObjects[newScore].SetActive(true);
            }
        }

        currentScore = newScore;
    }
}