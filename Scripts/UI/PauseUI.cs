using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [Header("------UI------")]
    public GameObject pauseUI;
    public GameObject UI;
    public GameObject documentUIEng;
    public GameObject documentUIJp;

    public string ResetRoomName;
    AllAudio allAudio;

    private void Awake()
    {
        UImanager.Getins.pauseUI = this;
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }

    private void Start()
    {
        Resume();
    }

    public void ActivePauseUI()
    {
        pauseUI.SetActive(true);
    }

    public void ActiveDocumentUI()
    {
        allAudio.PlaySFX(allAudio.UIclick);

        // 言語設定によって表示する書類が変わる
        if (FindObjectOfType<Mainmenu>().isEnglish)
        {
            documentUIEng.SetActive(true);
            documentUIJp.SetActive(false);
        }
        else
        {
            documentUIJp.SetActive(true);
            documentUIEng.SetActive(false);
        }

        pauseUI.SetActive(false);
    }

    public void CloseDocumentUI()
    {
        allAudio.PlaySFX(allAudio.UIclick);
        documentUIEng.SetActive(false);
        documentUIJp.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        pauseUI.SetActive(false);
        UI.SetActive(false);
        documentUIEng.SetActive(false);
        documentUIJp.SetActive(false);
    }
    public void RestartToRoom()
    {
        if (!string.IsNullOrEmpty(ResetRoomName))
        {
            GameMng.Getins.ResetScore();
            SceneManager.LoadScene(ResetRoomName);
        }
    }
    public void Resume()
    {
        UImanager.Getins.IsPause = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        UImanager.Getins.pauseUI.HidePauseUI();
        UImanager.Getins.Camera.LockCursor();
    }

    public void Quit()
    {
        UImanager.Getins.IsPause = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("mainmenu");
    }
}
