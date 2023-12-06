using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleController : MonoBehaviour
{
    public GameObject RedBluePanel;
    public GameObject FlyPanel;
    public GameObject BoostPanel;
    public GameObject HeroPanel;
    // public GameObject ExplanPanel;
    AudioSource se;
    // Transform rs;
    public RectTransform explanPanel;
    private bool animationStarted = false;
    Vector3 originalScale;


    void Start()
    {
        se = GetComponent<AudioSource>();
        // rs = ExplanPanel.GetComponent<RectTransform>();
        // StartCoroutine(ScaleOverTime(Vector3.one * 0.2f, 1.0f));
        originalScale = explanPanel.localScale;
    }





    // public void Smaller()
    // {

    //     // Vector3 objScale = rs.localScale;

    //     se.Play();
    //     Vector3 newScale = new Vector3(0.2f, 0.2f, 0.2f);
    //     rs.localScale = newScale;
    //     // Debug.Log("nani");

    // }
    IEnumerator SeGoTitle()
    {
        while (se.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Title");
    }



    public void RedBlueButton()
    {
        se.Play();
        RedBluePanel.SetActive(true);
        FlyPanel.SetActive(false);
        BoostPanel.SetActive(false);
        HeroPanel.SetActive(false);
    }
    public void FlyButton()
    {
        se.Play();
        RedBluePanel.SetActive(false);
        FlyPanel.SetActive(true);
        BoostPanel.SetActive(false);
        HeroPanel.SetActive(false);
    }
    public void BoostButton()
    {
        se.Play();
        RedBluePanel.SetActive(false);
        FlyPanel.SetActive(false);
        BoostPanel.SetActive(true);
        HeroPanel.SetActive(false);
    }
    public void HeroButton()
    {
        se.Play();
        RedBluePanel.SetActive(false);
        FlyPanel.SetActive(false);
        BoostPanel.SetActive(false);
        HeroPanel.SetActive(true);
    }
    public void Reversal()
    {
        // if (Input.GetMouseButtonDown(0))


        se.Play();
        if (animationStarted)
        {
            StartCoroutine(ScaleOverTime(originalScale, 1.0f));
        }
        else
        {
            StartCoroutine(ScaleOverTime(Vector3.one * 0.2f, 1.0f));
        }
        animationStarted = !animationStarted;
        // animationStarted = true;
        // StartCoroutine(ScaleOverTime(Vector3.one * 0.2f, 1.0f));


    }
    public void Return()
    {
        se.Play();
        StartCoroutine(SeGoTitle());

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {


    }
    IEnumerator ScaleOverTime(Vector3 toScale, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 startScale = explanPanel.localScale;

        while (elapsedTime < duration)
        {
            explanPanel.localScale = Vector3.Lerp(startScale, toScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // アニメーションが終了したら最終的なスケールを設定
        explanPanel.localScale = toScale;
    }

}

