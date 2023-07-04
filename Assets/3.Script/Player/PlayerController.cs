using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField] Rigidbody2D rigid; //���� �̵��� ���� ���� ����
    SpriteRenderer spriteRenderer; //������ȯ�� ���� ����

    //move
    [Header("Move")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float maxSpeed;

    //jump
    [Header("Jump")]
    [SerializeField] public float jump;
    int jumpCount = 0;

    bool isJump = false;
    bool isGround = false; //�����ϰ� �ٴڿ� ��Ҵ��� üũ ->�̰� ���ֵ� �ǰڴ�.. ����: �̹� Tag�� üũ�ϰ�����

    //Climing && Slide
    [Header("Climing && Slide")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float slidingSpeed;
    [SerializeField] float wallChkDistance;
  
    [SerializeField] LayerMask wallLayer; //����ĳ��Ʈ ���� ���̾�
    float isRight = 1f; //�ٶ󺸴� ���� 1= right , -1 = Left => �̰� �����ϱ� ����ĳ��Ʈ�� �ȳ���

    bool isWall;
    bool isWallStay = false;

    //Animation player + Arm
    [Header("Animation player && Arm")]
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
        //move
        if (playerInput.isMoveLeft || playerInput.isMoveRight)
        {
            Climb_Ray(); //Wall ���� RayCast
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
            if (playerInput.isMoveUp)
            {
                float ver = Input.GetAxis("Vertical");
                rigid.velocity = new Vector2(rigid.velocity.x, ver * slidingSpeed);
            }

            ////�̰� õõ�� �̲������°� �������
            //wallRgd.velocity = new Vector2(wallRgd.velocity.x, wallRgd.velocity.y * slidingSpeed);
            //Debug.Log("���� ������� �̲�����������"); //�۵��� �ϴµ� õõ�� �̲��������� ���� 
            //����(-)�� �۵��ϴµ� ������(+)�� �۵�����
            //collider�� �ʹ� ��Ƽ� ���� �ȵǴ� �ſ���...box�� �ٲٴ� �ߵ� �̷�����
            // �׷��ٸ� ���� collider�� �β��������ΰ�
        }
    }


    private void Move()
    {
        //player Movement
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        #region maxSpeed ����
        if (rigid.velocity.x > maxSpeed) //maxSpeed�� ������
        {
            //�ش� ������Ʈ�� �ӷ��� maxSpeed
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
            //2D������ �⺻������ �������� �ٶ󺸰� �ֱ� ������ Vetor2D�� ���� right�� �����Ѵ�
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
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("GrabPlatform"))
        {
            isGround = true;
            isJump = false;
            jumpCount = 0;
            Debug.Log("jump reset"); //���� ������ ���� �ȵǴµ�
                                     //isGround bool���� ������ְ� üũ�ϴϱ� ���� ���� ��
                                     //�׸��� ���߿��� ���� ���ϰ� �� �ذ� !  -> �ٴ� ���� �� �ִϸ��̼� ���峭�ž�
                                     // -> jumpCount++ �Ǵ� ���� isGround üũ������� �ִϸ��̼� �� �� �Ϻ��ϰ� �ذ�! 
                                     //Running ���� jumping ani�� �ٲ�� �� �׳� Ʈ�����ǿ� Running ���� true�� �ٲ������ �߸� �������� ��... @@
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
