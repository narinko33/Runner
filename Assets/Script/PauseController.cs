using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameController gameController;

    //ポーズ画面
    public void Pause()
    {
        if (gameController.state == State.GameOver) return;
        Debug.Log("Pause実行");
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
        SceneManager.LoadScene(sceneTitle);

    }

    //ポーズ画面から「リトライ」ボタンを押した処理
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void Start()
    {

    }

    void Update()
    {
    }
}
