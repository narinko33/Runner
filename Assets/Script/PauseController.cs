using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    AudioSource se;
    public GameObject pauseCanvas;
    public GameController gameController;

    //ポーズ画面
    public void Pause()
    {
        if (gameController.state == State.GameOver) return;
        Debug.Log("Pause実行");
        se.Play();
        //ポーズボタン押したらpausecanvasが出てくる
        pauseCanvas.SetActive(true);
        //ポーズ
        Time.timeScale = 0;
        // 佐々木修正案
        gameController.SwitchPause();
    }
    //ポーズ画面解除
    public void Active()
    {
        Debug.Log("Active");
        se.Play();
        //ステージシーンへ戻る
        pauseCanvas.SetActive(false);
        //ポーズ解除
        Time.timeScale = 1;
        // 佐々木修正案
        StartCoroutine(ReleasePause());
    }

    IEnumerator ReleasePause()
    {
        yield return new WaitForSeconds(0.1f);
        gameController.isPause = false;
    }

    // ポーズ画面から「タイトルに戻る」ボタン押してタイトルシーンへ戻る
    public void OnStartButtonClicked(string sceneTitle)
    {
        se.Play();
        StartCoroutine(SeTitle());
    }

    //ポーズ画面から「リトライ」ボタンを押した処理
    public void Retry()
    {
        se.Play();
        StartCoroutine(SeRetry());
    }

    IEnumerator SeRetry()
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SeTitle()
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    void Start()
    {
        se = GetComponent<AudioSource>();
    }

    void Update()
    {
    }
}

