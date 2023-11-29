using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    AudioSource se;
    public GameObject canvas;

    // タイトルシーンからステージシーンへ切り替え
    public void OnStartButtonClicked(string sceneName)
    {
        se.Play();
        StartCoroutine(SeGoScene(sceneName));

    }
    public void ActiveCanvas()
    {
        se.Play();
        canvas.SetActive(true);
    }

    // タイトルシーンからランキングボタン押したらランキング画面に移動
    public void GoRanking()
    {
        se.Play();
        StartCoroutine(SeGoRanking());
    }

    // タイトルシーンからRuleボタンを押してルール画面に移動
    public void GoRule()
    {
        se.Play();
        StartCoroutine(SeGoRule());
    }

    // ステージ選択画面からBackボタンを押してタイトルシーンに戻る
    public void ReturnTitle()
    {
        se.Play();
        canvas.SetActive(false);
    }

    void Start()
    {
        // 別シーンからタイトルシーンに戻ってくるときの動き
        Time.timeScale = 1;
        se = GetComponent<AudioSource>();

    }

    void Update()
    {

    }

    IEnumerator SeGoScene(string sceneName)
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator SeGoRanking()
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Ranking");
    }

    IEnumerator SeGoRule()
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Rule");
    }
}
