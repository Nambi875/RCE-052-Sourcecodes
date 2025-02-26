using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FirstPersonCamera : MonoBehaviour
{
    public Transform _camera;                       // カメラのTransform
    public Transform hand;                          // 手のTransform
    public Slider MouseSensitivitySlider;           // マウス感度のスライダー
    public float cameraSensitivity = 1.0f;          // カメラ感度
    public float cameraAcceleration = 5.0f;         // カメラの加速

    private float rotation_x_axis;                  // X軸の回転角度
    private float rotation_y_axis;                  // Y軸の回転角度
    private Vector2 lookInput;                      // 入力された視点移動の値

    private PlayerControls controls;                // InputSystemのコントロール

    private void Awake()
    {
        UImanager.Getins.Camera = this;             // UIマネージャーにカメラを設定
        controls = new PlayerControls();            // Input Systemのインスタンスを生成
    }

    void OnEnable()
    {
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();  // 視点移動の入力を取得
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;               // 入力がキャンセルされた場合、ゼロにリセット
        controls.Enable();                                                              // 入力を有効化
    }

    void OnDisable()
    {
        controls.Disable();                                                             // 入力を無効化
    }

    void Start()
    {
        LockCursor();

        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            LoadMouse();
        }
        else
        {
            SetMouse();
        }
        MouseSensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivity);
        SetMouse();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        float controllerX = lookInput.x * cameraSensitivity * Time.deltaTime;
        float controllerY = lookInput.y * cameraSensitivity * Time.deltaTime;
        // 視点移動の入力を適用
        rotation_x_axis += (mouseY + controllerY);
        rotation_y_axis += (mouseX + controllerX);

        rotation_x_axis = Mathf.Clamp(rotation_x_axis, -90.0f, 90.0f);                      // X軸の回転制限

        hand.localRotation = Quaternion.Euler(-rotation_x_axis, rotation_y_axis, 0);        // 手の回転設定

        transform.localRotation = Quaternion.Lerp(transform.localRotation,
            Quaternion.Euler(0, rotation_y_axis, 0), cameraAcceleration * Time.deltaTime);  // Y軸の回転をスムーズに適用

        _camera.localRotation = Quaternion.Lerp(_camera.localRotation,
            Quaternion.Euler(-rotation_x_axis, 0, 0), cameraAcceleration * Time.deltaTime); // カメラの回転をスムーズに適用
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;   // カーソルをロック
    }

    public void ConfinedCursor()
    {
        Cursor.lockState = CursorLockMode.Confined; // カーソルをウィンドウ内に制限
    }
    private void UpdateMouseSensitivity(float value)
    {
        cameraSensitivity = value;
        PlayerPrefs.SetFloat("MouseSensitivity", cameraSensitivity);
        PlayerPrefs.Save();
    }

    private void LoadMouse()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            cameraSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            MouseSensitivitySlider.value = cameraSensitivity;
        }
    }

    public void SetMouse()
    {
        float mouse = cameraSensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", mouse);
        PlayerPrefs.Save();

        MouseSensitivitySlider.value = mouse;
    }
}