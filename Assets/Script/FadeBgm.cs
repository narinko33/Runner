using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBgm : MonoBehaviour
{
    AudioSource audioSource;
    bool IsFadeOut = true;
    float FadeSeconds = 2.0f;//フェードしていく秒数
    float FadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        Debug.Log(audioSource.clip.length - audioSource.time);//デバッグ用

        if (audioSource.volume <= 0)// ボリュームが0以下のとき
        {
            audioSource.volume = 1;//ボリュームを1に戻す
            FadeDeltaTime = 0;//FadeDeltaTimeを0に戻す
            IsFadeOut = false;

        }
        if (IsFadeOut) //IsFadeOutがtrueの時
        {
            if (audioSource.clip.length - audioSource.time <= 2)//サウンド再生時間が残り2秒以下のとき
            {
                FadeDeltaTime += Time.deltaTime;
                audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeSeconds);//フェードアウト計算
                if (audioSource.volume <= 0.1)// ボリュームが0.1以下のとき
                {
                    audioSource.volume = 0;//ボリュームを0に戻す
                }

            }

        }
        else //IsFadeOutがfalseの時
        {
            FadeDeltaTime += Time.deltaTime;
            audioSource.volume = (float)(FadeDeltaTime / FadeSeconds);//フェードイン計算
            if (audioSource.volume >= 1)// ボリュームが1以上
            {
                IsFadeOut = true;
                FadeDeltaTime = 0;

            }
        }

    }
}

