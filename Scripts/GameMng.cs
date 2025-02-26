using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng
{
    static GameMng instance;
    public static GameMng Getins
    {
        get
        {
            if (instance == null) instance = new GameMng();
            return instance;
        }
    }
    //ここからコーディングをする
    public int score = 0;
    int Maxscore = 7;
    public void AddScore(int a)
    {
        score += a;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public bool HasReachedMaxScore()
    {
        return score >= Maxscore;
    }

    public FadeInManager fm = null;
}