using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [Header("------UI Panel------")]
    public GameObject mainMenu = null;
    public GameObject optionsMenu = null;
    public TMP_Text tmpTextComponent = null;
    public GameObject firstSelectedButton;
    public float scrollSpeed = 0.1f;

    public bool IsMainmenu = true;
    public bool isEnglish = true;

    [Header("Target Scene")]
    public string JpSceneName;
    public string EngSceneName;

    AllAudio allAudio;
    private PlayerControls controls;

    private void Awake()
    {
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
        controls = new PlayerControls();
        controls.UI.Submit.performed += _ => PressSelectedButton();
    }

    private void OnEnable()
    {
        controls.Enable();
        EventSystem.current.SetSelectedGameObject(firstSelectedButton); 
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        if (IsMainmenu == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            allAudio.PlayBGM(allAudio.mainmenu);
        }

        isEnglish = PlayerPrefs.GetInt("Language", 1) == 1;  // Default = JP

        if (tmpTextComponent != null)
        {
            tmpTextComponent.text = isEnglish ? "Language: Eng" : "Language: Jp";
        }
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }
    private void PressSelectedButton()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        if (selectedButton != null)
        {
            selectedButton.GetComponent<UnityEngine.UI.Button>()?.onClick.Invoke();
        }
    }

    public void OpenScene()
    {
        allAudio.PlaySFX(allAudio.UIclick);
        string targetSceneName = isEnglish ? EngSceneName : JpSceneName;

        if (!string.IsNullOrEmpty(targetSceneName))
        {
            Invoke("LoadTargetScene", 2f);
        }
        else
        {
            Debug.LogError("Target Scene Name is Null!");
        }
    }

    private void LoadTargetScene()
    {
        string targetSceneName = isEnglish ? EngSceneName : JpSceneName;
        SceneManager.LoadScene(targetSceneName);
    }

    public void OpenOptions()
    {
        allAudio.PlaySFX(allAudio.UIclick);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsMenu.transform.GetChild(0).gameObject);
    }

    public void CloseOptions()
    {
        allAudio.PlaySFX(allAudio.UIclick);
        mainMenu.SetActive(true); 
        optionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void QuitGame()
    {
        allAudio.PlaySFX(allAudio.UIclick);
        Debug.Log("Quit Game Called!");
        Application.Quit();
#if UNITY_EDITOR
        // 에디터 환경에서는 Application.Quit이 동작하지 않으므로, 강제 종료를 호출
        UnityEditor.EditorApplication.isPlaying = false;
#endif


    }

    public void ToggleLanguage()
    {
        allAudio.PlaySFX(allAudio.UIclick);
        isEnglish = !isEnglish;

        if (tmpTextComponent != null)
        {
            tmpTextComponent.text = isEnglish ? "Language: Eng" : "Language: Jp";
        }

        PlayerPrefs.SetInt("Language", isEnglish ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnPointerEnter()
    {
        allAudio.PlaySFX(allAudio.UIonthemouse);
    }

}