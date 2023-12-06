using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpMecha : MonoBehaviour
{
    public GameObject[] mechas;

    //空のオブジェクトに配列に入れたメカの中からランダムで1つを作り出す
    public void MechaGenerator()
    {
        GameObject Mecha = mechas[Random.Range(0, mechas.Length)];
        GameObject sm = Instantiate(Mecha,
        new Vector3(transform.position.x, transform.position.y, transform.position.z),
        Quaternion.Euler(0, 180, 0));
    }

    void Start()
    {
        MechaGenerator();
    }
}
