using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordScore : MonoBehaviour
{
    private float[] bestScore = new float[5];

    void ScoreSet(float currentScore)
    {
        // 일단 현재에 저장하고 시작.
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        float tmpScore = 0.0f;

        for (int i = 0; i < 5; i++)
        {
            // 저장된 최고점수
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");

            // 현재 점수가 랭킹에 오를 수 있을 때
            while (bestScore[i] < currentScore)
            {
                // 자리 바꾸기
                tmpScore = bestScore[i];
                bestScore[i] = currentScore;

                // 랭킹에 저장
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);

                // 다음 반복을 위한 준비
                currentScore = tmpScore;
            }
        }
        // 랭킹에 맞춰 점수와 이름 저장
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
        }

    }

    public TextMeshProUGUI[] scoreTexts; // 점수를 표시할 TextMeshProUGUI 컴포넌트의 배열

    private string[] rankScores = {}; // 점수 데이터

    void Start()
    {
        // 배열의 길이만큼 순회하면서 UI에 값을 할당
        for (int i = 0; i < rankScores.Length; i++)
        {
            scoreTexts[i].text = rankScores[i];
        }
    }
}
