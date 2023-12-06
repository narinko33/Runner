using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLine : MonoBehaviour
{
    public PlayerController player;
    void OnTriggerEnter(Collider other)
    {
        //タグPlayerがトリガーに触れたらポジションを変更する
        if (other.gameObject.tag == "Player")
        {
            player.transform.position += new Vector3(0.0f, 20.0f, -25.0f);
            player.moveDirection.z = 1;
        }
    }

}
