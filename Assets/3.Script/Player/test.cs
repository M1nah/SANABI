using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] Rigidbody2D rigd;
    SpriteRenderer spriteRenderer;

    PlayerInput playerInput; //input 가져오기


    private void Awake()
    {
        //컴포넌트 가져오기
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
