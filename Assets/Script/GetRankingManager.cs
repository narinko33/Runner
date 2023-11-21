using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetRankingManager : MonoBehaviour
{
    public RDatabase rdatabase;

    public void Start()
    {
        GetRanking();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }

    // ランキングの取得
    public void GetRanking()
    {
        StartCoroutine(rdatabase.GetRanking());
    }
}