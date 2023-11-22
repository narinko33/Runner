using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }


    //ポーズ画面
    public void Pause()
    {
        //ポーズボタン押したらpausecanvasが出てくる
        pauseCanvas.SetActive(true);
        //ポーズ
        Time.timeScale = 0;

    }
    //ポーズ画面解除
    public void Active()
    {
        //ステージシーンへ戻る
        pauseCanvas.SetActive(false);
        //ポーズ解除
        Time.timeScale = 1;

    }

    // ポーズ画面から「タイトルに戻る」ボタン押してタイトルシーンへ戻る
    public void OnStartButtonClicked(string sceneTitle)
    {
        SceneManager.LoadScene(sceneTitle);

    }

    void Update()
    {
    }
}
