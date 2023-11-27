using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetDatabase : MonoBehaviour
{
    public User[] users;
    public GameObject scorePrefab;
    public Transform content;

    // データベースからランキング取得
    public IEnumerator GetRanking()
    {
        // リクエスト先URL
        string url = "http://localhost/runner/getranking.py";

        // GETリクエスト送信
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
                default:
                    // レスポンス内容のJSONからRankingクラスのインスタンスに変換
                    string responseText = uwr.downloadHandler.text;
                    Debug.Log(responseText);
                    Ranking ranking = JsonUtility.FromJson<Ranking>(responseText);
                    users = ranking.result;
                    break;
            }
        }

        ShowRanking();
    }

    // 取得したランキング情報を表示
    void ShowRanking()
    {
        for (int i = 0; i < users.Length; i++)
        {
            // ランキングのユーザーデータを1件取得
            User user = users[i];
            // 画面表示用のテキストプレハブから生成
            GameObject score = Instantiate(scorePrefab, content);

            // Textコンポーネント取得、ランキングのスコア表示
            Text scoreText = score.GetComponent<Text>();
            scoreText.text = $"No.{i + 1:00} {user.name}:{user.score}m";
        }
    }
}

