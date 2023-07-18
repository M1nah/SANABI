using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Berry : MonoBehaviour
{
    public GameObject berry;

    PlayerInput playerInput;

    bool isGetBerry=false;

    bool isFadeOut;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.isInteraction && isGetBerry)
        {
            Debug.Log("E키를 누름");
            SceneManager.LoadScene(0);
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
