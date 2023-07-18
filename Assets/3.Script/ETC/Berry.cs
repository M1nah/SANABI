using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Berry : MonoBehaviour
{
    public GameObject berry;

    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameManager gameManager;

    bool isGetBerry=false;

    bool isFadeOut = false; //fadeOut 처리 

    private void Update()
    {
        if (playerInput.isInteraction && isGetBerry)
        {
            Debug.Log("E키를 누름");
            gameManager.ChangeScene(0);
            //SceneManager.LoadScene(0);
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
