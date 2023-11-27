using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour, IPointerClickHandler
{
    public GameObject canvas;
    public TitleController titleController;

    // タイトルシーンからステージシーンへ切り替え
    public void OnStartButtonClicked(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        titleController.ActiveCanvas();

    }

    public void ActiveCanvas()
    {
        canvas.SetActive(true);
    }

    public void GoRanking()
    {
        SceneManager.LoadScene("Ranking");
    }

    void Start()
    {
        // 別シーンからタイトルシーンに戻ってくるときの動き
        Time.timeScale = 1;

    }

    void Update()
    {

    }
}
