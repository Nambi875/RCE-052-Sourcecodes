using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSystem : MonoBehaviour
{
    [Header("���� ���θ� üũ�ϼ���.")]
    public bool isCorrectAnswer; // Inspector���� ���� ���θ� ����

    public void CheckAnswer()
    {
        if (isCorrectAnswer)
        {
            GameMng.Getins.AddScore(1); // ���� ����
        }
        else
        {
            GameMng.Getins.ResetScore(); // ���� �ʱ�ȭ
        }
    }
}
