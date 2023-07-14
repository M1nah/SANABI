using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public GameObject berry;

    ChangeScene sceneChange;
    PlayerInput playerInput;
    public FadeInOut fade;

    bool isGetBerry=false;

    private void Start()
    {
        sceneChange = GetComponent<ChangeScene>(); //아래 컴포넌트로 찾자 0714
        fade = GetComponent<FadeInOut>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.isInteraction && isGetBerry)
        {
            //fade.FadeOut(); //fade 안됨 
            Debug.Log("베리를 만짐");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isGetBerry = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isGetBerry = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isGetBerry = false;
        }
    }
}
