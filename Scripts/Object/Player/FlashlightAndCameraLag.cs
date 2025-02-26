using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class FlashlightAndCameraLag : MonoBehaviour
{
    public Transform playerBody; // 플레이어 몸체의 Transform
    public Transform flashlight; // 플래시라이트의 Transform
    public Transform cameraTransform; // 카메라의 Transform

    public float flashlightSpeed = 15f; // 플래시라이트 회전 속도
    public float cameraLagSpeed = 5f; // 카메라 지연 속도

    private Vector2 rotation = Vector2.zero; // 마우스 입력 값
    private Vector2 cameraRotation = Vector2.zero; // 카메라가 따라갈 회전 값

    void Update()
    {
        // 마우스 입력값 읽기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 입력값으로 플레이어의 Y축 회전 업데이트 (수평 회전)
        rotation.x += mouseX;

        // 입력값으로 카메라와 플래시라이트의 X축 회전 업데이트 (수직 회전)
        rotation.y -= mouseY;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f); // 수직 회전 제한

        // 플레이어 몸체 회전 (수평 회전만)
        playerBody.localRotation = Quaternion.Euler(0, rotation.x, 0);

        // 플래시라이트는 즉각적으로 목표 회전값에 도달
        Quaternion targetFlashlightRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
        flashlight.localRotation = Quaternion.Slerp(flashlight.localRotation, targetFlashlightRotation, Time.deltaTime * flashlightSpeed);

        // 카메라는 플래시라이트를 천천히 따라감
        cameraRotation = Vector2.Lerp(cameraRotation, rotation, Time.deltaTime * cameraLagSpeed);
        Quaternion targetCameraRotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);
        cameraTransform.localRotation = targetCameraRotation;
    }
}