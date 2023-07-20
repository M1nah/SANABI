using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlatform : MonoBehaviour
{
    //밟으면 카메라 흔들기

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("Level") != 0)
        {
            GameManager.instance.HP();
            
            Debug.Log("데드장판밟음");
        }
    }

}
