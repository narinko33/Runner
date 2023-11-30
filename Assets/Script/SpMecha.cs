using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpMecha : MonoBehaviour
{
    Transform tr;
    public GameObject[] mechas;

    public void MechaGenerator()
    {
        GameObject Mecha = mechas[Random.Range(0, mechas.Length)];
        GameObject sm = Instantiate(Mecha,
        new Vector3(tr.position.x, tr.position.y, tr.position.z),
        Quaternion.Euler(0, 180, 0));

    }


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        MechaGenerator();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
