using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//ゲームステート
public enum State
{
    Ready,
    Play,
    GameOver
}

public class GameController : MonoBehaviour
{
    // //ゲームステート
    // public enum State
    // {
    //     Ready,
    //     Play,
    //     GameOver
    // }

    public State state;

    public PlayerController player;
    public Text Timer;
    public Text Score;
    public Text StateText;
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
        Ready();
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        switch (state)
        {
            case State.Ready:
                if (Input.GetMouseButtonUp(0))
                {
                    GameStart();
                }
                break;
            case State.Play:
                // カウントダウンタイムを整形して表示
                Timer.text = string.Format("Time: {0:00.00}", CountDownTime);
                // 経過時刻を引いていく
                CountDownTime -= Time.deltaTime;

                //タイマーが0になったらタイマーが止まる
                if (CountDownTime <= 0.0F)
                {
                    enabled = false;
                }
                //タイマーが0になったらGameOver
                if (CountDownTime <= 0.0f) GameOver();
                break;
            case State.GameOver:
                //タッチしたらタイトルシーンに移動

                break;

        }
    }

    void Ready()
    {
        state = State.Ready;

        player.SetSteerActive(false);

        StateText.text = "Ready";
        player.animator.SetFloat("Blend", 0.0f);
    }

    void GameStart()
    {
        state = State.Play;
        player.SetSteerActive(true);
        StateText.enabled = false;

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

    public void GameOver()
    {
        state = State.GameOver;

        player.moveDirection.x = 0.0f;
        player.moveDirection.y = 0.0f;
        player.moveDirection.z = 0.0f;
        player.animator.SetFloat("Blend", 0.0f);
        player.ScoreText.enabled = true;
        ShowSeore();
    }

    int CalcScore()
    {
        //進んだ距離の計算
        return (int)player.transform.position.z;
    }
}
