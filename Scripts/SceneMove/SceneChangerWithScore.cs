using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChangerWithScore : MonoBehaviour
{
    [Header("Scene Settings")]
    public string[] sceneList;
    public float holdTime = 2.0f;

    [Header("UI Settings")]
    public Image gaugeImage;
    public Image eButtonIcon;

    [Header("Fade Settings")]
    public float fadeDuration = 1.0f;

    private float holdTimer = 0.0f;
    private bool isPlayerInside = false;
    private bool isTransitioning = false;

    private string lastScene = "";
    private int correctStreak = 0; 
    private int wrongStreak = 0;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Interact.performed += _ => StartHoldTimer();
        controls.Player.Interact.canceled += _ => ResetHoldTimer();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = 0;
            gaugeImage.gameObject.SetActive(false);
        }
        if (eButtonIcon != null) eButtonIcon.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!isPlayerInside || isTransitioning || gaugeImage == null) return;

        if (holdTimer > 0)
        {
            holdTimer += Time.deltaTime;
            gaugeImage.gameObject.SetActive(true);
            gaugeImage.fillAmount = holdTimer / holdTime;

            if (holdTimer >= holdTime)
            {
                StartCoroutine(HandleSceneTransition());
                ResetHoldTimer(); 
            }
        }
        else
        {
            holdTimer = 0.0f;
            if (gaugeImage != null)
            {
                gaugeImage.fillAmount = 0;
                gaugeImage.gameObject.SetActive(false);
            }
        }
    }

    private void StartHoldTimer()
    {
        if (isPlayerInside)
        {
            holdTimer = 0.01f; 
        }
    }

    private void ResetHoldTimer()
    {
        holdTimer = 0.0f;
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = 0;
            gaugeImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (eButtonIcon != null) eButtonIcon.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            holdTimer = 0.0f;
            if (eButtonIcon != null) eButtonIcon.gameObject.SetActive(false);
            if (gaugeImage != null)
            {
                gaugeImage.fillAmount = 0;
                gaugeImage.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        isTransitioning = true;

        AnswerSystem answerSystem = GetComponent<AnswerSystem>();
        if (answerSystem != null)
        {
            answerSystem.CheckAnswer();
        }

        yield return StartCoroutine(GameMng.Getins.fm.FadeOut());

        if (GameMng.Getins.HasReachedMaxScore())
        {
            SceneManager.LoadScene("EndingScene1");
        }
        else
        {
            string nextScene = GetWeightedRandomScene();
            if (!string.IsNullOrEmpty(nextScene))
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }

        if (GameMng.Getins != null)
        {
            GameMng.Getins.AddScore(1);
        }

        StartCoroutine(GameMng.Getins.fm.FadeIn());
        isTransitioning = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private string GetWeightedRandomScene()
    {
        if (sceneList.Length == 0) return null;

        string specialScene = sceneList[0]; 
        List<string> otherScenes = new List<string>(sceneList);
        otherScenes.RemoveAt(0);

        float specialSceneWeight = 0.4f;

        if (correctStreak == 1) specialSceneWeight = 0.25f;
        else if (correctStreak >= 2) specialSceneWeight = 0.1f;

        if (wrongStreak == 1) specialSceneWeight = 0.65f;
        else if (wrongStreak >= 2) specialSceneWeight = 0.8f;

        float randomValue = Random.value;

        if (randomValue < specialSceneWeight)
        {
            lastScene = specialScene;
            correctStreak++;
            wrongStreak = 0;
            return specialScene;
        }
        else
        {
            int randomIndex = Random.Range(0, otherScenes.Count);
            lastScene = otherScenes[randomIndex];
            wrongStreak++; 
            correctStreak = 0;
            return lastScene;
        }
    }
}