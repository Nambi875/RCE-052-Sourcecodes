using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu_Headbobbing : MonoBehaviour
{
    public Transform head; // 頭（カメラまたは頭オブジェクト）
    public float rotationSpeed = 2f; // 回転速度（値を下げると滑らかになる）
    public float returnSpeed = 1.5f; // 元の位置に戻る速度
    public float maxRotationAngle = 20f; // 最大回転角度（左右移動の制限）
    public float bobSpeed = 2f; // ヘッドボビングの速度
    public float bobAmount = 0.05f; // ヘッドボビングの大きさ
    public float deadZoneRadius = 50f; // マウスの中心からの反応範囲（ピクセル単位）

    private Quaternion initialRotation;
    private Vector3 initialPosition;

    void Start()
    {
        initialRotation = head.localRotation; // 初期回転値を保存
        initialPosition = head.localPosition; // 初期位置を保存
    }

    void Update()
    {
        RotateHeadToMouse();
        ApplyHeadBobbing();
    }

    void RotateHeadToMouse()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mousePos = Input.mousePosition;

        float distanceFromCenter = Vector2.Distance(screenCenter, mousePos);

        if (distanceFromCenter > deadZoneRadius)
        {
            // マウスがデッドゾーンを超えた場合に回転
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane + 1f));

            Vector3 direction = (worldMousePos - head.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // 初期回転値を基準に角度制限を適用
            Quaternion limitedRotation = Quaternion.RotateTowards(initialRotation, targetRotation, maxRotationAngle);

            // スムーズに回転
            head.rotation = Quaternion.Slerp(head.rotation, limitedRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // マウスがデッドゾーン内に戻った場合、元の位置に戻る
            head.rotation = Quaternion.Slerp(head.rotation, initialRotation, Time.deltaTime * returnSpeed);
        }
    }

    void ApplyHeadBobbing()
    {
        float bobOffset = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        head.localPosition = initialPosition + new Vector3(0, bobOffset, 0);
    }
}