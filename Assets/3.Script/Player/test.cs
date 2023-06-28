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

    //climing
    float vertical;
    bool isPlatform;
    bool isCliming;


    Animator ani;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ani = GetComponent<Animator>();
    }

    private void Update()
    {
   
        if(playerInput.isMoveLeft || playerInput.isMoveRight)
        {
            Move();
        }
        
        if (playerInput.isJump)
        {
            Jump();
        }


    }

    private void FixedUpdate()
    {
        if (isCliming)
        {
            Climing();
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
            spriteRenderer.transform.localScale = new Vector2(-2,2);
        }
        else if (playerInput.isMoveRight)
        {
            spriteRenderer.transform.localScale = new Vector2(2,2);
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
            jumpCount++;
            isJump = true;
            rigid.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        }
        else
        {
            isJump = false;
            jumpCount = 0;
        }
    }

    //jumpCount Reset 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isJump = false;
            jumpCount = 0;
        }
    }


    private void Climing()
    {
        vertical = Input.GetAxis("Vertical");
        if(isPlatform && Mathf.Abs(vertical)>0.1f)
        {
            isCliming = true;
            //rigid.velocity = new Vector2(rigid.velocity.x, vertical*moveSpeed);
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
            isCliming = false;
        }
    }


}
