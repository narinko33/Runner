using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetRankingManager : MonoBehaviour
{
    public GetDatabase getdatabase;
    AudioSource se;

    public void Start()
    {
        se = GetComponent<AudioSource>();
        GetRanking();
    }

    public void GoTitle()
    {
        se.Play();
        StartCoroutine(SeGoTitle());
    }

    // ランキングの取得
    public void GetRanking()
    {
        StartCoroutine(getdatabase.GetRanking());
    }

    IEnumerator SeGoTitle()
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Title");
    }


}