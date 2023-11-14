using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public Text Timer;
    public Text Score;
    float CountDownTime = 15.0f;

    public float GetCountDownTime()
    {
        return this.CountDownTime;
    }

    public void AddTime()
    {
        this.CountDownTime += 5.0f;
    }

    public void DecreaseTime()
    {
        this.CountDownTime -= 5.0f;
    }

    public void ShowSeore()
    {
        int score = CalcScore();
        Score.text = "SCORE : " + score + "m";
    }

    void Start()
    {

    }

    void Update()
    {
        // カウントダウンタイムを整形して表示
        Timer.text = string.Format("Time: {0:00.00}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;

        //タイマーが0になったらタイマーが止まる
        if (CountDownTime <= 0.0F)
        {
            enabled = false;
        }

    }

    int CalcScore()
    {
        //進んだ距離の計算
        return (int)player.transform.position.z;
    }
}
