using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource bgm;
    public AudioClip[] Clips;
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.clip = Clips[0];
    }

    public void InvincibleBGM()
    {
        bgm.clip = Clips[1];
        bgm.pitch = 1.2f;
        bgm.Play();
    }

    public void MainBGM()
    {
        bgm.clip = Clips[0];
        bgm.pitch = 1;
        bgm.Play();
    }


}
