using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager
{
    static UImanager instance;
    public static UImanager Getins
    {
        get
        {
            if (instance == null) instance = new UImanager();
            return instance;
        }
    }
    //ここからコーディングをする
    public PauseUI pauseUI;
    public FirstPersonCamera Camera;
    public bool IsPause = false;
    public void Pause()
    { 
        IsPause = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseUI.ActivePauseUI();
        Camera.ConfinedCursor();
    }

    public void Resume()
    {
        IsPause = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseUI.HidePauseUI();
        Camera.LockCursor();
    }
}
