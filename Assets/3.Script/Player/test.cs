using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] Rigidbody2D rigd;
    SpriteRenderer spriteRenderer;

    PlayerInput playerInput; //input ��������


    private void Awake()
    {
        //������Ʈ ��������
        rigd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerInput = GetComponent<PlayerInput>();

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigd.AddForce(Vector2.right*h,ForceMode2D.Impulse);
    }

}
