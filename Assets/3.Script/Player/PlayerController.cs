using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;

    [SerializeField] Rigidbody2D rigid; //물리 이동을 위한 변수 선언
    SpriteRenderer spriteRenderer; //방향전환을 위한 변수

    //move
    [SerializeField] float moveSpeed=3f;
    [SerializeField] float maxSpeed;

    //jump
    [SerializeField] float jump;
    int jumpCount = 0;

    bool isJump = false;
    bool isGround = false; //점프하고 바닥에 닿았는지 체크

    //climing
    float vertical;
    bool isPlatform;
    bool isCliming;


    //Animation player + Arm
    public GameObject Arm;
    Animator playerAni;
    Animator armAni;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerAni = GetComponent<Animator>();
        armAni = Arm.GetComponent<Animator>();
    }

    private void Update()
    {

        if (playerInput.isMoveLeft || playerInput.isMoveRight)
        {
            Move();
            playerAni.SetBool("isRunning", true);
            armAni.SetBool("ArmIsRunning", true);

        }
        else
        {
            playerAni.SetBool("isRunning", false);
            armAni.SetBool("ArmIsRunning", false);
        }


        if (playerInput.isJump)
        {
            Jump();
            playerAni.SetBool("isJumping", true);
            armAni.SetBool("ArmIsJumping", true);
        }
        else if (!playerInput.isJump)
        {

            playerAni.SetBool("isJumping", false);
            armAni.SetBool("ArmIsJumping", false);
        }

    }

    private void FixedUpdate()
    {
        //Climing();


        if (isCliming)
        {
            vertical = Input.GetAxis("Vertical");
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, vertical * moveSpeed); //3 = moveSpeed였음 
            //아래 stopSpeed 부분에서 멈추느라 속도를 0으로 만들어버려서 그뒤로 쭉 moveSpeed에 0이 적용된다...
        }
        else
        {
            rigid.gravityScale = 3f;
        }

        //if (isCliming)
        //{
        //    rigid.gravityScale = 0f; //벽에 닿았을 때 중력을 0으로
        //    rigid.velocity = new Vector2(rigid.velocity.x, vertical * moveSpeed);
        //    Debug.Log("벽 오르기");
        //}
        //
        //else if(!(isCliming))
        //{
        //    isCliming = false;
        //    rigid.gravityScale = 3f; //아닐때 중력을 되돌리기 
        //    Debug.Log("벽에서 떨어짐"); //걍 isMoveUp 키만 눌러도 여기가 무한적으로 올라가는구만..
        //}
    }


    private void Move()
    {
        //player Movement
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        #region maxSpeed 관리
        if (rigid.velocity.x > maxSpeed) //maxSpeed를 넘으면
        {
            //해당 오브젝트의 속력은 maxSpeed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
        }
        #endregion

        //Direction Sprite Flip(with Child Component)
        if (playerInput.isMoveLeft)
        {
            spriteRenderer.transform.localScale = new Vector2(-1, 1);
        }
        else if (playerInput.isMoveRight)
        {
            spriteRenderer.transform.localScale = new Vector2(1, 1);
        }

        //stop Speed
        if(playerInput.isMoveLeft ==true || playerInput.isMoveRight == true)
        {
            moveSpeed = 3f;
        }
        else
        {
            moveSpeed = 0f;
        }

        //if (Input.GetButtonUp("Horizontal"))
        //{
        //    moveSpeed = 0f; //여기 스피드가 0이 되느라 climb.y 값을 구할떄 0이 곱해진다... 그렇다면 어케해야하느냐... 
        //    Debug.Log("moveSpeed=0");
        //}
        //else if (Input.GetButtonDown("Horizontal"))
        //{
        //    moveSpeed = 3f;
        //    Debug.Log("moveSpeed=3");
        //}

    }

    private void Jump()
    {
        if (jumpCount < 1)
        {
            isGround = true;
            jumpCount++;
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isJump = true;
        }
        //else if (isGround)
        //{
        //    isJump = false;
        //    jumpCount = 0;
        //    Debug.Log("jump stop"); //isGround= false 면 jumpCount= 0으로 바꿈... 그런데 여기가 안들어가짐..
        //                            //그렇지만 작동하는걸... 된거 아닐까? 여기 필요 없는거 아냐?
        //                            //주석 처리해도 작동하잖아! 필요 없었잔아! @@
        //}
    }

    //jumpCount Reset 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isGround = true;
            isJump = false;
            jumpCount = 0;
            Debug.Log("jump reset"); //점프 리셋이 전혀 안되는디
                                     //isGround bool값을 만들어주고 체크하니까 이제 리셋 됨
                                     //그리고 공중에서 점프 안하게 됨 해결 !  -> 근대 오ㅔ 또 애니메이션 고장난거야
                                     // -> jumpCount++ 되는 곳에 isGround 체크해줬더니 애니메이션 또 됨 완벽하게 해결! 
                                     //Running 도중 jumping ani로 바뀌는 건 그냥 트렌지션에 Running 값도 true로 바꿔줬더니 잘만 나오더라 하... @@
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }



    //Platform Climing
    private void Climing()
    {
        vertical = Input.GetAxis("Vertical");
        if (isPlatform && Mathf.Abs(vertical) > 0f)
        {
            isCliming = true;
        }

       
    }



    //Touch the Climing "GrabPlatform" Tag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform"))
        {
            isCliming = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform"))
        {
            if (playerInput.isJump)
            {
                rigid.gravityScale = 0f;
                rigid.velocity = new Vector2(rigid.velocity.x, 10 * moveSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("GrabPlatform"))
        {
            isPlatform = false;
            isCliming = false;
        }
    }



}
