using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlatform : MonoBehaviour
{
    //������ ī�޶� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("Level") != 0)
        {
            GameManager.instance.HP();
            
            Debug.Log("�������ǹ���");
        }
    }

}
