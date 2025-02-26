using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    AllAudio allAudio;
    public float footstepInterval = 0.5f; // 발소리 간격
    public float delayBeforeSound = 0.2f; // 이동 후 발소리 지연 시간
    public float speedThreshold = 0.1f; // 이동 감지 최소 속도

    private float timer = 0f;
    private PlayerMovement playerMovement; // PlayerMovement 스크립트 참조
    private bool isMoving = false;


    private void Awake()
    {
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }
    void Start()
    {
        // PlayerMovement 스크립트 가져오기
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement 스크립트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        // 이동 속도 계산
        float currentSpeed = playerMovement.direction.magnitude * playerMovement.playerSpeed;

        if (currentSpeed > speedThreshold)
        {
            if (!isMoving)
            {
                isMoving = true;
                timer = delayBeforeSound; // 이동 직후 발소리 지연
            }

            timer += Time.deltaTime;
            if (timer >= footstepInterval + delayBeforeSound)
            {
                PlayFootstep(currentSpeed);
                timer = 0f;
            }
        }
        else
        {
            isMoving = false; // 이동 감지되지 않으면 초기화
            timer = 0f;
        }
    }

    void PlayFootstep(float currentSpeed)
    {
        allAudio.PlayFoot(allAudio.footstep);
    }
}
