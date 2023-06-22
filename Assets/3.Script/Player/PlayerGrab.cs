using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerController playerController;

    bool isUp=false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GrabPlatform"))
        {
            isUp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("GrabPlatform"))
        {
            isUp = false;
        }
    }

    private void GrabHill()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //사다리 응용
            //transform.position += new Vector2(transform.position.y, 0f);
        }

    }

}
