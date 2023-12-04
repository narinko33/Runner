using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public State state;

    public int score;

    int stop = 1;

    public bool isPause;

    public PlayerController player;
    public Text Timer;
    public Text Score;
    public Text StateText;
    public Canvas ResultCanvas;
    public GameObject PauseBotton;
    float CountDownTime = 2.0f;


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

    public void SwitchPause()
    {
        isPause = !isPause;
    }


    public void ShowSeore()
    {
        score = CalcScore();
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
        // Debug.Log(state);
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
                Timer.text = string.Format("TIME: {0:00.00}", CountDownTime);
                // 経過時刻を引いていく
                CountDownTime -= Time.deltaTime * stop;

                //タイマーが0になったらタイマーが止まる
                if (CountDownTime <= 0.0F)
                {
                    this.stop = 0;
                    Timer.text = "TIME: 00.00";
                    GameOver();
                }
                break;
            case State.GameOver:
                StateText.enabled = true;
                //StateTextの更新
                StateText.text = "TIME OVER";
                StartCoroutine(TimeOverOut());

                break;

        }
    }

    void Ready()
    {
        state = State.Ready;

        player.SetSteerActive(false);
        PauseBotton.SetActive(false);

        StateText.text = "READY";
        player.animator.SetFloat("Blend", 0.0f);
    }

    void GameStart()
    {
        state = State.Play;
        player.SetSteerActive(true);
        PauseBotton.SetActive(true);
        StateText.enabled = false;

        // カウントダウンタイムを整形して表示
        Timer.text = string.Format("TIME: {0:00.00}", CountDownTime);
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

        //プレイヤーの動きを止める
        player.SetSteerActive(false);
        player.animator.SetFloat("Blend", 0.0f);
        PauseBotton.SetActive(false);
    }

    int CalcScore()
    {
        //進んだ距離の計算
        return (int)player.transform.position.z;
    }


    IEnumerator TimeOverOut()
    {
        //3秒後に動くコルーチン
        yield return new WaitForSeconds(3.0f);
        StateText.gameObject.SetActive(false);
        ResultCanvas.gameObject.SetActive(true);
        ShowSeore();

    }

}
