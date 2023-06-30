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
            //오르는 상태일때 중력을 0으로 두고 velocity.y값만 움직일 수 있게 해두었다
            vertical = Input.GetAxis("Vertical");
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, vertical * moveSpeed); 
            //아래 stopSpeed 부분에서 멈추느라 속도를 0으로 만들어버려서 그뒤로 쭉 moveSpeed에 0이 적용된다...
            //stopSpeed 쪽에서 input.isMoveRight와 input.isMoveRight를 조건문으로 걸어 moveSpeed를 지정해주었더니 해결 !
        }
        else
        {
            //오르는 상태가 아니라면 중력을 원래대로 되돌려주기 
            rigid.gravityScale = 3f;
        }
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
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("GrabPlatform"))
        {
            isPlatform = false;
            isCliming = false;

            //if (playerInput.isJump)
            //{
            //    rigid.gravityScale = 0f;
            //    rigid.velocity = new Vector2(rigid.velocity.x, 10 * moveSpeed);
            //}
        }
    }


   
}
