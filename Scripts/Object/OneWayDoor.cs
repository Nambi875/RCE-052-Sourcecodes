using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneWayDoor : MonoBehaviour
{
    public GameObject door;                 // ドアのオブジェクト
    public float openAngle = 90f;           // ドアが開く角度
    public float openSpeed = 2f;            // ドアが開く速度
    public float closeSpeed = 1f;           // ドアが閉まる速度

    private bool isDoorOpen = false;        // ドアが開いているかどうか
    private bool isDoorMoving = false;      // ドアが移動中かどうか
    private bool isDoorLocked = false;      // ドアがロックされているかどうか
    private bool isPlayerInRange = false;   // プレイヤーが範囲内にいるかどうか
    public GameObject eButtonIcon;          // Eボタンのアイコン

    private float currentAngle = 0f;        // 現在のドアの回転角度
    private float targetAngle = 0f;         // 目標回転角度
    private float startAngle = 0f;          // 開始回転角度
    private float currentLerpTime = 0f;     // 補間時間
    private float currentSpeed = 0f;        // 現在の速度

    AllAudio allAudio;
    private bool OnceRunning = false;  // すでに閉じる処理が実行中かどうか
    private PlayerControls controls;   // Input Systemのコントロール

    private void Awake()
    {
        controls = new PlayerControls();  // Input Systemの初期化
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();

        // 「インタラクト」ボタンが押されたときにドアを開く処理を登録
        controls.Player.Interact.performed += _ => TryOpenDoor();
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
        currentAngle = 0f;  // 初期状態でドアは閉じている
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            // Eボタンのアイコンを表示
            eButtonIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            // Eボタンのアイコンを非表示
            eButtonIcon.SetActive(false);
        }
    }

    // 「インタラクト」ボタンが押されたときにドアを開く処理を実行
    private void TryOpenDoor()
    {
        if (isPlayerInRange && !isDoorOpen && !isDoorMoving && !isDoorLocked)
        {
            OpenDoor();
        }
    }

    private void Update()
    {
        // ドアが移動中である場合、回転を適用
        if (isDoorMoving)
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / currentSpeed;

            t = Mathf.SmoothStep(0f, 1f, t);  // スムーズな動きにするための補間

            currentAngle = Mathf.Lerp(startAngle, targetAngle, t);
            door.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

            // 回転が完了したら、移動状態を解除
            if (t >= 1f)
            {
                isDoorMoving = false;
                Debug.Log("ドアの開閉処理が完了しました");

                // ドアが閉じたらロックする
                if (!isDoorOpen)
                {
                    isDoorLocked = true;
                    Debug.Log("ドアがロックされました");
                }
            }
        }
    }

    // ドアを開く処理
    private void OpenDoor()
    {
        allAudio.PlayBack(allAudio.dooropening);
        targetAngle = openAngle;
        isDoorOpen = true;
        isDoorMoving = true;
        startAngle = currentAngle;
        currentLerpTime = 0f;
        currentSpeed = openSpeed;
    }

    // ドアを閉じる処理（コルーチン）
    public IEnumerator CloseDoor()
    {
        if (OnceRunning) yield break;  // すでに実行中なら終了
        OnceRunning = true;

        allAudio.PlayBack(allAudio.doorclosing);
        targetAngle = 0f;
        isDoorOpen = false;
        isDoorMoving = true;
        startAngle = currentAngle;
        currentLerpTime = 0f;
        currentSpeed = closeSpeed;

        yield return new WaitForSeconds(3f); // 3秒待機

        allAudio.PlayBGM(allAudio.roommusic);
    }
}