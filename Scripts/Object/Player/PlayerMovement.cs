using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform head;                      // カメラ
    public float playerSpeed = 5.0f;            // プレイヤーの移動速度
    public float playerAcceleration = 2.0f;     // 加速度
    public float gravity = -9.81f;              // 重力

    private CharacterController controller;
    private Vector3 velocity;
    public Vector3 direction;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Pause.performed += _ =>
        {
            if (UImanager.Getins.IsPause == false)
            {
                UImanager.Getins.Pause();
            }
            else
            {
                UImanager.Getins.Resume();
            }
        };
    }
    private void OnEnable()
    {
        controls.Enable();  // コントロールを有効化
    }

    private void OnDisable()
    {
        controls.Disable(); // コントロールを無効化
    }
    private void Start()
    {
        // CharacterController
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
                                                                // 移動方向計算
        direction = (Input.GetAxisRaw("Horizontal") * head.right + Input.GetAxisRaw("Vertical") * head.forward).normalized;
        direction.y = 0f;                                       // 垂直移動防止用

        // 移動速度計算
        Vector3 move = direction * playerSpeed;
        controller.Move(move * Time.deltaTime);

        // 重力適用
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;                                    // 地面に着くと重力加速度は０になる。
        }

        // キャラクターに重力を適用
        controller.Move(velocity * Time.deltaTime);
    }
}