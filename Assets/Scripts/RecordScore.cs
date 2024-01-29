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
        // �ϴ� ���翡 �����ϰ� ����.
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        float tmpScore = 0.0f;

        for (int i = 0; i < 5; i++)
        {
            // ����� �ְ�����
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");

            // ���� ������ ��ŷ�� ���� �� ���� ��
            while (bestScore[i] < currentScore)
            {
                // �ڸ� �ٲٱ�
                tmpScore = bestScore[i];
                bestScore[i] = currentScore;

                // ��ŷ�� ����
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);

                // ���� �ݺ��� ���� �غ�
                currentScore = tmpScore;
            }
        }
        // ��ŷ�� ���� ������ �̸� ����
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
        }

    }

    public TextMeshProUGUI[] scoreTexts; // ������ ǥ���� TextMeshProUGUI ������Ʈ�� �迭

    private string[] rankScores = {}; // ���� ������

    void Start()
    {
        // �迭�� ���̸�ŭ ��ȸ�ϸ鼭 UI�� ���� �Ҵ�
        for (int i = 0; i < rankScores.Length; i++)
        {
            scoreTexts[i].text = rankScores[i];
        }
    }
}
