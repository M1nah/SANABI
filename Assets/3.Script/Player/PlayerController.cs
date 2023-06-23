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

    bool isRun = false;
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
        else if (ani.GetBool("isJumping"))
        {
            isJump = false;
            jumpCount = 0;
            ani.SetBool("isJumping", false);
        }
        //애니메이션이 안먹혀..
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

        // Animation
        if (rigidBody.velocity.normalized.x == 0)
        {
            ani.SetBool("isRunning", false);
            Debug.Log("너 계속 돌아가는중이니? 러닝끝남?< 계속 돌아가네... ");
        }
        else
        {
            ani.SetBool("isRunning", true);
            Debug.Log("너도 계속 돌아가는중이니? 러닝시작?<너도... ");
        }


        //DirectionSprite 방향 바꾸기
        if (plyerInput.isMoveLeft)
        {
            spriteRenderer.transform.localScale = new Vector2(-2, 2);
        }
        else if (plyerInput.isMoveRight)
        {
            spriteRenderer.transform.localScale = new Vector2(2, 2);
        }


        //stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidBody.velocity = new Vector2(0.1f * rigidBody.velocity.normalized.x, rigidBody.velocity.y);
        }
    }

}
