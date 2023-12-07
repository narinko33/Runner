using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;
    public GameObject target;  //追従ターゲットプロパティ
    public Material[] material; //スカイボックスマテリアルを入れる配列
    public float followSpeed;

    void Start()
    {
        diff = target.transform.position - transform.position;  //追従距離の計算
        if (SceneManager.GetActiveScene().name == "ObstacleStage")
        {
            //田中さんのシーンでは使わないためリターン
            return;
        }
        else
        {
            //配列に入れたスカイボックスの中からランダムに設定
            this.GetComponent<Skybox>().material = material[Random.Range(0, material.Length)];
        }
    }

    private void LateUpdate()
    {  //線形補間関数によるスムージング
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed
        );
    }
}
