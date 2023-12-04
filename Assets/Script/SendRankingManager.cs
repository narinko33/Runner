using UnityEngine;
using UnityEngine.UI;

public class SendRankingManager : MonoBehaviour
{
    AudioSource se;
    public GameObject ResultCanvas;
    public InputField input;
    public Database database;

    public void Start()
    {
        se = GetComponent<AudioSource>();
    }

    // スコア送信
    public void SendScore()
    {
        se.Play();
        ResultCanvas.SetActive(false);
        StartCoroutine(database.SendScore(input));
    }
}