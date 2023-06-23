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
        //�ִϸ��̼��� �ȸ���..
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

        // Animation
        if (rigidBody.velocity.normalized.x == 0)
        {
            ani.SetBool("isRunning", false);
            Debug.Log("�� ��� ���ư������̴�? ���׳���?< ��� ���ư���... ");
        }
        else
        {
            ani.SetBool("isRunning", true);
            Debug.Log("�ʵ� ��� ���ư������̴�? ���׽���?<�ʵ�... ");
        }


        //DirectionSprite ���� �ٲٱ�
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
