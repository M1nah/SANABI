using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //moving���� �����ϴ� ���ڵ� ��Ȱ ��...
{
    public PlayerInput playerInput;

    [SerializeField] Rigidbody2D rigid; //���� �̵��� ���� ���� ����
    SpriteRenderer spriteRenderer; //������ȯ�� ���� ����
    
    //move
    [SerializeField]float moveSpeed;
    [SerializeField]float maxSpeed;

    //jump
    [SerializeField] float jump;
    int jumpCount = 0;

    bool isJump = false;
    bool isGround = false; //�����ϰ� �ٴڿ� ��Ҵ��� üũ

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
   
        if(playerInput.isMoveLeft || playerInput.isMoveRight)
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
        else if (!playerInput.isJump && !isJump)
        {

            playerAni.SetBool("isJumping", false);
            armAni.SetBool("ArmIsJumping", false);
        }


        Climing();
    }

    private void FixedUpdate()
    {

        if (isCliming)
        {

            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, vertical * moveSpeed);
        }
        else
        {
            rigid.gravityScale = 3f;
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
            spriteRenderer.transform.localScale = new Vector2(-1,1);
        }
        else if (playerInput.isMoveRight)
        {
            spriteRenderer.transform.localScale = new Vector2(1,1);
        }

        //stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            moveSpeed = 0;
        }
        
    }

    private void Jump()
    {
        if (jumpCount < 1)
        {
            isGround = true;
            jumpCount++;
            rigid.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
            isJump = true;
        }
        else if (isGround)
        {
            isJump = false;
            jumpCount = 0;
            Debug.Log("jump stop"); // isGround= false �� jumpCount= 0���� �ٲ�... �׷��� ���Ⱑ �ȵ���
        }
    }

    //jumpCount Reset 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isGround = true;
            isJump = false;
            jumpCount = 0;
            Debug.Log("jump reset"); //���� ������ ���� �ȵǴµ�
                                     //isGround bool���� ������ְ� üũ�ϴϱ� ���� ���� ��
                                     //�׸��� ���߿��� ���� ���ϰ� �� �ذ� !  -> �ٴ� ���� �� �ִϸ��̼� ���峭�ž�
                                     // -> jumpCount++ �Ǵ� ���� isGround üũ������� �ִϸ��̼� �� �� �Ϻ��ϰ� �ذ�! 
                                     //Running ���� jumping ani�� �ٲ�� �� �׳� Ʈ�����ǿ� Running ���� true�� �ٲ������ �߸� �������� ��...
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
        if(isPlatform && Mathf.Abs(vertical)>0f)
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
        }
    }


}
