using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string[] sceneList;                          // 이동할 씬 리스트
    public float holdTime = 2.0f;                       // E키를 꾹 누르는 시간
    private float holdTimer = 0.0f;
    private bool isPlayerInside = false;
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
    private void Update()
    {
        if (isPlayerInside && holdTimer > 0)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                LoadRandomScene();
                ResetHoldTimer(); // 씬 이동 후 타이머 리셋
            }
        }
    }

    private void StartHoldTimer()
    {
        if (isPlayerInside)
        {
            holdTimer = 0.01f; // 0이 아닌 값으로 설정하여 Update에서 증가하도록 함
        }
    }

    private void ResetHoldTimer()
    {
        holdTimer = 0.0f;
    }

    private void LoadRandomScene()
    {
        int randomIndex = Random.Range(0, sceneList.Length);
        SceneManager.LoadScene(sceneList[randomIndex]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            holdTimer = 0.0f;
        }
    }
}
