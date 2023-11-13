using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public Text Timer;
    float CountDownTime;

    bool GameOver()
    {
        return CountDownTime == 0;
    }

    void Start () 
    {
        CountDownTime = 10.0f;
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
}
