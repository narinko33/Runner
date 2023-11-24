using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Database : MonoBehaviour
{
    public GameController gameController;
    public User[] users;

    // データベースにスコアを送信
    public IEnumerator SendScore(InputField input)
    {
        // リクエスト先URL
        string url = "http://localhost/runner/sendscore.py";

        // リクエストパラメータを追加
        WWWForm form = new WWWForm();
        form.AddField("name", input.text);
        form.AddField("score", gameController.score);

        // Postリクエスト送信
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
            }
        }
        SceneManager.LoadScene("Ranking");
    }

}

// データ格納用クラス
public class Ranking
{
    public User[] result;
}

[Serializable]
public class User
{
    public int id;
    public string name;
    public int score;
}

