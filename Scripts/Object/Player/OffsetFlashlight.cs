using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFlashlight : MonoBehaviour
{
    public Light flashlight;             // フラッシュライトのLightコンポーネント
    public AudioSource audioSource;      // オーディオソース（Inspectorで設定）
    private PlayerControls controls;     // InputSystemのコントロール

    private void Awake()
    {
        controls = new PlayerControls();  // Input Systemのインスタンスを生成
        controls.Player.FlashLight.performed += _ => ToggleFlashlight();  // フラッシュライトの入力を取得
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
        if (flashlight != null)
        {
            flashlight.enabled = true;  // 初期状態はオン
        }
    }
    private void ToggleFlashlight()
    {
        if (flashlight == null) return;
        flashlight.enabled = !flashlight.enabled;
        PlayToggleSound();
    }

    private void PlayToggleSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // 効果音を再生
        }
    }
}
