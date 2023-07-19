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
    FadeInOut fadeInOut;

    private void Start()
    {
        fadeInOut = FindObjectOfType<FadeInOut>();
    }

    private void Update()
    {
        if (playerInput.isInteraction && isGetBerry)
        {
            Debug.Log("E키를 누름");
            StartCoroutine(ReturnToIntro());
        }
    }

    private IEnumerator ReturnToIntro()
    {
        fadeInOut.Fade(true);
        yield return new WaitForSeconds(fadeInOut.fadeTime);
        gameManager.ChangeScene(0);
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
