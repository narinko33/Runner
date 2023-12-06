using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Image img;
    Color originalColor; // ボタンの初期色を保持する変数

    // ボタンが押された時に呼び出されるメソッド（赤と青のボタン）
    public void RedBlueButton()
    {
        // ボタンが押された時に画像の色を黒に変更する
        img.color = Color.black;

        // 他のボタンの色を元に戻す
        ResetOtherButtons();
    }

    // ボタンが押された時に呼び出されるメソッド（オレンジのボタン）
    public void OrangeButton()
    {
        // ボタンが押された時に画像の色を黒に変更する
        img.color = Color.black;

        // 他のボタンの色を元に戻す
        ResetOtherButtons();
    }

    // ボタンが押された時に呼び出されるメソッド（黄色のボタン）
    public void YellowButton()
    {
        // ボタンが押された時に画像の色を黒に変更する
        img.color = Color.black;

        // 他のボタンの色を元に戻す
        ResetOtherButtons();
    }

    // ボタンが押された時に呼び出されるメソッド（緑のボタン）
    public void GreenButton()
    {
        // ボタンが押された時に画像の色を黒に変更する
        img.color = Color.black;

        // 他のボタンの色を元に戻す
        ResetOtherButtons();
    }

    // スクリプトが開始される時に呼び出されるメソッド
    void Start()
    {
        // Imageコンポーネントを取得してimgに格納する
        img = GetComponent<Image>();

        // 初期色を保持する
        originalColor = img.color;
    }

    // フレームごとに呼び出されるメソッド
    void Update()
    {
        // ここに何か追加する場合はここにコードを書く
    }

    // 他のボタンの色を元に戻すメソッド
    void ResetOtherButtons()
    {
        // すべてのボタンを取得する
        ButtonController[] allButtons = FindObjectsOfType<ButtonController>();

        // 各ボタンの色を初期色に戻す
        foreach (ButtonController button in allButtons)
        {
            if (button != this) // 自分以外のボタンのみ処理する
            {
                button.img.color = button.originalColor;
            }
        }
    }
}
