using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetRankingManager : MonoBehaviour
{
    public GetDatabase getdatabase;

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
        StartCoroutine(getdatabase.GetRanking());
    }
}