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
    public static GameManager i;    // 어디서든 게임매니저를 접근할 수 있는 전역변수 선언.
    TextMeshProUGUI timeT;          // TimeText 컴포넌트를 담아둘 변수
    TextMeshProUGUI bestT;          // BestText 컴포넌트를 담아둘 변수    
    TextMeshProUGUI shootCount;     // ShootCount 컴포넌트를 담아둘 변수

    float t = 0.0f;                 // 플레이 타임
    //float score = 0.0f;             // 점수

    GameObject obj;


    bool isGameOver = false;        // 게임이 끝났음을 판단할 변수

    void Start()
    {
        i = this;   // 전역 변수에 해당 컴포넌트를 참조.
        timeT = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        bestT = GameObject.Find("BestText").GetComponent<TextMeshProUGUI>();
        shootCount = GameObject.Find("ShootCount").GetComponent<TextMeshProUGUI>();
        obj = GameObject.Find("Player");
    }

    void Update()
    {
        //if (isGameOver) return; // 게임이 끝나면 Update 함수를 빠져나간다.
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
        string min = (t / 60).ToString();           //분

        if (int.Parse(min) < 10)
        {
            min = "0" + min;    //10분 아래면 0을 붙여준다.
        }  

        string sec = (t % 60).ToString();           // 초

        if (int.Parse(sec) < 10)
        { 
            sec = "0" + sec;    //10초 아래면 0을 붙여준다
        }  

        return min + ":" + sec;
    }

    public void GameOver()      // 캐릭터가 방해물에 부딪히면 호출 될 함수
    {
        isGameOver = true;      // 게임 오버 상태로 전환.
        SetBestTime();          // 베스트 타임 설정.
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
        if (PlayerPrefs.HasKey("BEST"))         // BEST라는 키가 저장되어 있다면
        {
            int b = PlayerPrefs.GetInt("BEST"); // b변수에 저장된 값을 담아줌

            if ((int)t > b)
            {
                PlayerPrefs.SetInt("BEST", b = (int)t);
            }

            bestT.text = $"BEST\n{SetTime(b)}"; // 텍스트 표현
        }
        else
        {
            PlayerPrefs.SetInt("BEST", (int)t); // BEST라는 키로 현재 플레이 시간 저장
            bestT.text = $"BEST\n{SetTime((int)t)}"; // 텍스트 표현
        }

        bestT.enabled = true;                   // 베스트 텍스트 컴포넌트 활성화
    }
}
