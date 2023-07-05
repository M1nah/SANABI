using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //playerController에 dash 기능 추가중 
{
    public PlayerInput playerInput;
    [SerializeField] Rigidbody2D rigid; 
    SpriteRenderer spriteRenderer;

    //move
    [Header("Move")]
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;

    //jump
    [Header("Jump")]
    [SerializeField] public float jump;
    int jumpCount = 0;

    bool isJump = false;

    //Climing && Slide
    [Header("Climing && Slide")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float slidingSpeed;
    [SerializeField] float wallChkDistance;

    [SerializeField] LayerMask wallLayer;
    float isRight = 1f;

    bool isWall;
    bool isWallStay = false;

    //Animation player + Arm
    [Header("Animation player && Arm")]
    public GameObject Arm;
    Animator playerAni;
    Animator armAni;



    [Header("Dash Speed")]
    PlayerHookShot hookShot;

    bool isDash;
    float dashSpd = 7f; // dash 스피드
    float defaultTime;
    float dashTime;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerAni = GetComponent<Animator>();
        armAni = Arm.GetComponent<Animator>();

        hookShot = GetComponent<PlayerHookShot>();
    }

    private void Update()
    {
        //move
        if (playerInput.isMoveLeft || playerInput.isMoveRight)
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
        if (isWall && isWallStay)
        {
            if (playerInput.isMoveUp || playerInput.isMoveDown)
            {
                float ver = Input.GetAxis("Vertical");
                rigid.velocity = new Vector2(rigid.velocity.x, ver * slidingSpeed);
                playerAni.SetBool("isWallCilmbUp", true);
                armAni.SetBool("ArmIsWallClimbUp", true);

                Debug.Log("climb 시작"); //들어가짐
            }
            else if (!playerInput.isMoveUp && !isWallStay || !playerInput.isMoveDown && !isWallStay)
            {
                //↑ 뭔가 조건을 잘못 걸어둔거같은데 
                playerAni.SetBool("isWallCilmbUp", false);
                armAni.SetBool("ArmIsWallClimbUp", false);
                Debug.Log("climb 끝"); //안들어가짐 
            }
        }


        //dash => 공중에 매달릴 때만 써야함
        if (Input.GetKeyDown(KeyCode.LeftShift) && hookShot.isHookActive)
        {
            isDash = true;
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

    //jumpCount Reset 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJump = false;
            jumpCount = 0;
            Debug.Log("jump reset"); //점프 리셋이 전혀 안되는디
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform") && isWall)
        {
            isWallStay = true;
        }

        if (collision.CompareTag("GrabPlatform") && playerInput.isJump)
        {
            isWallStay = false;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform") && playerInput.isJump)
        {
            isWallStay = false;
        }
    }
}
