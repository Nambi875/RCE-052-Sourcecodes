using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    AllAudio allAudio;
    public float footstepInterval = 0.5f; // �߼Ҹ� ����
    public float delayBeforeSound = 0.2f; // �̵� �� �߼Ҹ� ���� �ð�
    public float speedThreshold = 0.1f; // �̵� ���� �ּ� �ӵ�

    private float timer = 0f;
    private PlayerMovement playerMovement; // PlayerMovement ��ũ��Ʈ ����
    private bool isMoving = false;


    private void Awake()
    {
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }
    void Start()
    {
        // PlayerMovement ��ũ��Ʈ ��������
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement ��ũ��Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    void Update()
    {
        // �̵� �ӵ� ���
        float currentSpeed = playerMovement.direction.magnitude * playerMovement.playerSpeed;

        if (currentSpeed > speedThreshold)
        {
            if (!isMoving)
            {
                isMoving = true;
                timer = delayBeforeSound; // �̵� ���� �߼Ҹ� ����
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
            isMoving = false; // �̵� �������� ������ �ʱ�ȭ
            timer = 0f;
        }
    }

    void PlayFootstep(float currentSpeed)
    {
        allAudio.PlayFoot(allAudio.footstep);
    }
}
