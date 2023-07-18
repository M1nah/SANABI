using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlatform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.HP();
            
            Debug.Log("데드장판밟음");
        }
    }

}
