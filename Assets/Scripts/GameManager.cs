using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager i;    // ��𼭵� ���ӸŴ����� ������ �� �ִ� �������� ����.
    TextMeshProUGUI timeT;          // TimeText ������Ʈ�� ��Ƶ� ����
    TextMeshProUGUI bestT;          // BestText ������Ʈ�� ��Ƶ� ����    
    TextMeshProUGUI shootCount;     // ShootCount ������Ʈ�� ��Ƶ� ����

    float t = 0.0f;                 // �÷��� Ÿ��
    //float score = 0.0f;             // ����

    GameObject obj;


    bool isGameOver = false;        // ������ �������� �Ǵ��� ����

    void Start()
    {
        i = this;   // ���� ������ �ش� ������Ʈ�� ����.
        timeT = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        bestT = GameObject.Find("BestText").GetComponent<TextMeshProUGUI>();
        shootCount = GameObject.Find("ShootCount").GetComponent<TextMeshProUGUI>();
        obj = GameObject.Find("Player");
    }

    void Update()
    {
        //if (isGameOver) return; // ������ ������ Update �Լ��� ����������.
        ChangeCount();
        if (!isGameOver)
        {
            t += Time.deltaTime;
            timeT.text = $"TIME\n{SetTime((int)t)}";
        }
        else
        {
            StartCoroutine(Gameover());
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    IEnumerator Gameover()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("End");
    }

    public string SetTime(int t)
    {
        string min = (t / 60).ToString();           //��

        if (int.Parse(min) < 10)
        {
            min = "0" + min;    //10�� �Ʒ��� 0�� �ٿ��ش�.
        }  

        string sec = (t % 60).ToString();           // ��

        if (int.Parse(sec) < 10)
        { 
            sec = "0" + sec;    //10�� �Ʒ��� 0�� �ٿ��ش�
        }  

        return min + ":" + sec;
    }

    public void GameOver()      // ĳ���Ͱ� ���ع��� �ε����� ȣ�� �� �Լ�
    {
        isGameOver = true;      // ���� ���� ���·� ��ȯ.
        SetBestTime();          // ����Ʈ Ÿ�� ����.
    }

    public void ChangeCount()
    {
        if (obj != null)
        {
            shootCount.text = $"Shoot : {obj.GetComponent<Player>().canShoot}";
        }
    }

    void SetBestTime()
    {
        if (PlayerPrefs.HasKey("BEST"))         // BEST��� Ű�� ����Ǿ� �ִٸ�
        {
            int b = PlayerPrefs.GetInt("BEST"); // b������ ����� ���� �����

            if ((int)t > b)
            {
                PlayerPrefs.SetInt("BEST", b = (int)t);
            }

            bestT.text = $"BEST\n{SetTime(b)}"; // �ؽ�Ʈ ǥ��
        }
        else
        {
            PlayerPrefs.SetInt("BEST", (int)t); // BEST��� Ű�� ���� �÷��� �ð� ����
            bestT.text = $"BEST\n{SetTime((int)t)}"; // �ؽ�Ʈ ǥ��
        }

        bestT.enabled = true;                   // ����Ʈ �ؽ�Ʈ ������Ʈ Ȱ��ȭ
    }
}
