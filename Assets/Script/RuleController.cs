using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleController : MonoBehaviour
{
    public GameObject RedBluePanel;
    public GameObject FlyPanel;
    public GameObject BoostPanel;
    public GameObject HeroPanel;
    AudioSource se;

    void Start()
    {
        se = GetComponent<AudioSource>();

    }
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
}
