using UnityEngine;
using UnityEngine.UI;

public class SendRankingManager : MonoBehaviour
{
    public GameObject ResultCanvas;
    public InputField input;
    public Database database;

    public void Start()
    {

    }

    // スコア送信
    public void SendScore()
    {
        ResultCanvas.SetActive(false);
        StartCoroutine(database.SendScore(input));
    }
}