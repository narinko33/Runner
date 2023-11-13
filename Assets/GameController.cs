using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public Text Timer;
    public float countDownTime;

    void Start () 
    {
        countDownTime = 60.0f;
    }
   
    void Update()
    {
       // カウントダウンタイムを整形して表示
    Timer.text = string.Format("Time: {0:00.00}", countDownTime);
    // 経過時刻を引いていく
    countDownTime -= Time.deltaTime;
    }
}
