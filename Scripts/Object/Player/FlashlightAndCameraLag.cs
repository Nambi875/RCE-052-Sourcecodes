using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class FlashlightAndCameraLag : MonoBehaviour
{
    public Transform playerBody; // �÷��̾� ��ü�� Transform
    public Transform flashlight; // �÷��ö���Ʈ�� Transform
    public Transform cameraTransform; // ī�޶��� Transform

    public float flashlightSpeed = 15f; // �÷��ö���Ʈ ȸ�� �ӵ�
    public float cameraLagSpeed = 5f; // ī�޶� ���� �ӵ�

    private Vector2 rotation = Vector2.zero; // ���콺 �Է� ��
    private Vector2 cameraRotation = Vector2.zero; // ī�޶� ���� ȸ�� ��

    void Update()
    {
        // ���콺 �Է°� �б�
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // �Է°����� �÷��̾��� Y�� ȸ�� ������Ʈ (���� ȸ��)
        rotation.x += mouseX;

        // �Է°����� ī�޶�� �÷��ö���Ʈ�� X�� ȸ�� ������Ʈ (���� ȸ��)
        rotation.y -= mouseY;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f); // ���� ȸ�� ����

        // �÷��̾� ��ü ȸ�� (���� ȸ����)
        playerBody.localRotation = Quaternion.Euler(0, rotation.x, 0);

        // �÷��ö���Ʈ�� �ﰢ������ ��ǥ ȸ������ ����
        Quaternion targetFlashlightRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
        flashlight.localRotation = Quaternion.Slerp(flashlight.localRotation, targetFlashlightRotation, Time.deltaTime * flashlightSpeed);

        // ī�޶�� �÷��ö���Ʈ�� õõ�� ����
        cameraRotation = Vector2.Lerp(cameraRotation, rotation, Time.deltaTime * cameraLagSpeed);
        Quaternion targetCameraRotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);
        cameraTransform.localRotation = targetCameraRotation;
    }
}