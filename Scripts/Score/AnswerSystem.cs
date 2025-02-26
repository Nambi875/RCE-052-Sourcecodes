using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSystem : MonoBehaviour
{
    [Header("정답 여부를 체크하세요.")]
    public bool isCorrectAnswer; // Inspector에서 정답 여부를 설정

    public void CheckAnswer()
    {
        if (isCorrectAnswer)
        {
            GameMng.Getins.AddScore(1); // 점수 증가
        }
        else
        {
            GameMng.Getins.ResetScore(); // 점수 초기화
        }
    }
}
