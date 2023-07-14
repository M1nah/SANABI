using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField] public Rigidbody2D rigid; //물리 이동을 위한 변수 선언
    SpriteRenderer spriteRenderer; //방향전환을 위한 변수

    //move
    [Header("Move")]
    [SerializeField] public float moveSpeed;
    [SerializeField] float maxSpeed;

    //jump
    [Header("Jump")]
    [SerializeField] public float jump;
    int jumpCount = 0;


    public bool isJump = false;
    public bool isGround = false; 

    //Climing && Slide
    [Header("Climing && Slide")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float slidingSpeed;
    [SerializeField] float wallChkDistance;
  
    [SerializeField] LayerMask wallLayer; 
    float isRight = 1f; //바라보는 방향 1= right , -1 = Left => 이거 없으니까 레이캐스트가 안나감

    bool isWall=false;
    bool isWallStay = false;


    //Animation player + Arm
    [Header("Animation player && Arm")]
    public GameObject Arm;
    Animator playerAni;
    Animator armAni;


    PlayerHookShot playerHookShot;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerAni = GetComponent<Animator>();
        armAni = Arm.GetComponent<Animator>();

        playerHookShot = GetComponent<PlayerHookShot>();

    }

    private void Update()
    {
        //move
        if(playerHookShot.isAttach)
        {
            //PlayerHookShot에서 Dash 사용할때 오른쪽인지 왼쪽인지 방향을 체크하기위한 bool값 
            if (playerInput.isMoveLeft)
            {
                playerHookShot.isDirection = false;
            }
            else if (playerInput.isMoveRight)
            {
                playerHookShot.isDirection = true;
            }
        }

        else if (playerInput.isMoveLeft || playerInput.isMoveRight )
        {
            Climb_Ray(); //Wall 검출 RayCast
            Move();
            playerAni.SetBool("isRunning", true);
            armAni.SetBool("ArmIsRunning", true);

        }
        else
        {
            playerAni.SetBool("isRunning", false);
            armAni.SetBool("ArmIsRunning", false);
        }


        //jump
        if (playerInput.isJump)
        {
            Jump();
            playerAni.SetBool("isJumping", true);
            armAni.SetBool("ArmIsJumping", true);

        }
        else if (!playerInput.isJump && !isJump)
        {
            playerAni.SetBool("isJumping", false);
            armAni.SetBool("ArmIsJumping", false);
        }



    }

    private void FixedUpdate()
    {
        Climb();
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
        if (playerInput.isMoveLeft == true || playerInput.isMoveRight == true)
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


    }


    private void Climb_Ray()
    {
        if (playerInput.isMoveRight)
        {
            isWall = Physics2D.Raycast(wallCheck.position, Vector2.right * isRight, wallChkDistance, wallLayer);
            //2D에서는 기본적으로 오른쪽을 바라보고 있기 때문에 Vetor2D의 값을 right로 지정한다
            Debug.DrawRay(wallCheck.position, Vector2.right * isRight, Color.red);
        }
        if (playerInput.isMoveLeft)
        {
            isWall = Physics2D.Raycast(wallCheck.position, Vector2.left, wallChkDistance, wallLayer);
            Debug.DrawRay(wallCheck.position, Vector2.left, Color.blue);
        }
    }

    //climb
    private void Climb() 
    {
        if (isWall && isWallStay )
        {
            if (playerInput.isMoveUp || playerInput.isMoveDown)
            {
                rigid.gravityScale = 0;
                float ver = Input.GetAxis("Vertical");
                rigid.velocity = new Vector2(rigid.velocity.x, ver * slidingSpeed);

                playerAni.SetBool("isWallCilmbUp", true);
                armAni.SetBool("ArmIsWallClimbUp", true);
            }

            if (!playerInput.isMoveUp && !playerInput.isMoveDown)
            {
                //벽중간에서 멈춤
                slidingSpeed = 0f;
                //rigid.AddForce(Vector2.zero, ForceMode2D.Force); // 그래도 올라갈때 밀리네.. 
                Debug.Log("climb 일시정지"); //들어가짐 
                //들어가지고 멈추긴 하는데 그래도 밀림.. 
            }
            else
            {
                slidingSpeed = 2f;
            }

        }
        //else if(!isWall && !isWallStay && climbTest) //이부분 계속 올라가..  
        //{
        //    climbTest = false;
        //    rigid.gravityScale = 3f;
        //    playerAni.SetBool("isWallCilmbUp", false);
        //    armAni.SetBool("ArmIsWallClimbUp", false);
        //    Debug.Log("climb 끝");
        //}
    }

    //jumpCount Reset 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGround = true;
            isJump = false;
            jumpCount = 0;
            //Debug.Log("jump reset"); //점프 리셋이 전혀 안되는디
                                     //isGround bool값을 만들어주고 체크하니까 이제 리셋 됨
                                     //그리고 공중에서 점프 안하게 됨 해결 !  -> 근대 오ㅔ 또 애니메이션 고장난거야
                                     // -> jumpCount++ 되는 곳에 isGround 체크해줬더니 애니메이션 또 됨 완벽하게 해결! 
                                     //Running 도중 jumping ani로 바뀌는 건 그냥 트렌지션에 Running 값도 true로 바꿔줬더니 잘만 나오더라 하... @@
        }
    }

    //Wall Climb Check
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform") && isWall)
        {
            isWallStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform"))
        {
            isWallStay = false;
            rigid.gravityScale = 3f;
            playerAni.SetBool("isWallCilmbUp", false);
            armAni.SetBool("ArmIsWallClimbUp", false);
            Debug.Log("1");
        }
    }
}
