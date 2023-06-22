using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody; // 물리 이동을 위한 변수 선언
    SpriteRenderer spriteRenderer; // 방향 전환을 위한 변수
    public PlayerInput plyerInput;

    Animator ani;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField]  private float jump;
    private int jumpCount = 0;


    bool isJump = false;
    bool isGround = false;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        plyerInput = transform.GetComponent<PlayerInput>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        Move();

        // Animation
        if (rigidBody.velocity.normalized.x == 0)
        {
            ani.SetBool("isRunning", false);
        }
        else
        {
            ani.SetBool("isRunning", true);
        }

    }

    private void Jump()
    {
        //jump 
        if (plyerInput.isJump && jumpCount < 1)
        {
            jumpCount++;
            rigidBody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isJump = true;
            ani.SetBool("isJumping", true);
        }

        // falling할 때 조금 더 빨리 떨어지게하기
    }

    private void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");
        rigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        #region maxSpeed 관리
        if (rigidBody.velocity.x > maxSpeed) //maxSpeed를 넘으면
        {
            //해당 오브젝트의 속력은 maxSpeed
            rigidBody.velocity = new Vector2(maxSpeed, rigidBody.velocity.y);
        }
        else if (rigidBody.velocity.x < maxSpeed * (-1))
        {
            rigidBody.velocity = new Vector2(maxSpeed * -1, rigidBody.velocity.y);
        }
        #endregion

        //DirectionSprite 방향 바꾸기
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            Debug.Log("방향바꾸기");
        }

        //stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidBody.velocity = new Vector2(0.3f * rigidBody.velocity.normalized.x, rigidBody.velocity.y);
        }



    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].normal.y > 0.7f) //playercollier에 붙어있는 함수
        {
            isGround = true;
            isJump = false;
            jumpCount = 0; //jumpcount 초기화
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        isGround = false;
        ani.SetBool("isJumping", false);
    }
}
