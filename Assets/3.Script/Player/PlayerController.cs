using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody; // 물리 이동을 위한 변수 선언
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

    private float vertical; //climing
    bool isPlatform;
    bool isCliming;


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
        Climing();
        Move();
    }



    private void FixedUpdate()
    {
        //climimg Update
        if(isCliming)
        {
            rigidBody.gravityScale = 0f;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, vertical * moveSpeed);
        }
        else
        {
            rigidBody.gravityScale = 6f;
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
        else
        {
            isJump = false;
            jumpCount = 0;
            ani.SetBool("isJumping", false);
        }

        //달릴때 애니메이션이 안먹혀..
        
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


        // Running Animation
        if (rigidBody.velocity.normalized.x == 0) 
        {
            ani.SetBool("isRunning", false);
            Debug.Log("러닝끝남?");
        }

        //else
        //{
        //    ani.SetBool("isRunning", true);
        //    Debug.Log("러닝시작?");
        //}



        //DirectionSprite Flip (with Child Component)
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
            rigidBody.velocity = new Vector2(0 * rigidBody.velocity.normalized.x, rigidBody.velocity.y);
            moveSpeed = 0;
            //ani.SetBool("isRunning", false);
        }
    }

    private void Climing()
    {
        vertical = Input.GetAxis("Vertical");
        if(isPlatform && Mathf.Abs(vertical) > 0f)
        {
            isCliming = true;
        }
    }



    //jump count 초기화
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y>0.1f)
        {
            isGround = true;
            isJump = false;
            jumpCount = 0;
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

    

    //오르기
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("GrabPlatform"))
    //    {
    //        isPlatform = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("GrabPlatform"))
    //    {
    //        isPlatform = false;
    //        isCliming = false;
    //    }
    //}

}
