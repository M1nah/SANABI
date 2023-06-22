using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody; // ���� �̵��� ���� ���� ����
    SpriteRenderer spriteRenderer; // ���� ��ȯ�� ���� ����
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

        // falling�� �� ���� �� ���� ���������ϱ�
    }

    private void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");
        rigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        #region maxSpeed ����
        if (rigidBody.velocity.x > maxSpeed) //maxSpeed�� ������
        {
            //�ش� ������Ʈ�� �ӷ��� maxSpeed
            rigidBody.velocity = new Vector2(maxSpeed, rigidBody.velocity.y);
        }
        else if (rigidBody.velocity.x < maxSpeed * (-1))
        {
            rigidBody.velocity = new Vector2(maxSpeed * -1, rigidBody.velocity.y);
        }
        #endregion

        //DirectionSprite ���� �ٲٱ�
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            Debug.Log("����ٲٱ�");
        }

        //stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidBody.velocity = new Vector2(0.3f * rigidBody.velocity.normalized.x, rigidBody.velocity.y);
        }



    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].normal.y > 0.7f) //playercollier�� �پ��ִ� �Լ�
        {
            isGround = true;
            isJump = false;
            jumpCount = 0; //jumpcount �ʱ�ȭ
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        isGround = false;
        ani.SetBool("isJumping", false);
    }
}
