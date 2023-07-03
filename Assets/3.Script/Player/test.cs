using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //moving���� �����ϴ� ���ڵ� ��Ȱ ��...
{
    PlayerInput playerInput;
    SpriteRenderer spR;

    //Climing && slide 
    [SerializeField] Rigidbody2D wallRgd;
    public float slidingSpeed;

    public Transform wallCheck;
    public float wallChkDistance;
    [SerializeField] LayerMask wallLayer;
    bool isWall;

    bool isWallStay=false;

    float isRight = 1f; //�ٶ󺸴� ���� 1= right , -1 = Left

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        spR = GetComponent<SpriteRenderer>();

        wallRgd = GetComponent<Rigidbody2D>();

    }

    private void Update()
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



    private void FixedUpdate()
    {
        if (isWall && isWallStay)
        {
            wallRgd.velocity = new Vector2(wallRgd.velocity.x, wallRgd.velocity.y * slidingSpeed);
            Debug.Log("���� ������� �̲�����������"); //�۵��� �ϴµ� õõ�� �̲��������� ���� 
                                                       //����(-)�� �۵��ϴµ� ������(+)�� �۵�����
                                                       //collider�� �ʹ� ��Ƽ� ���� �ȵǴ� �ſ���...box�� �ٲٴ� �ߵ� �̷�����
                                                       // �׷��ٸ� ���� collider�� �β��������ΰ�
        }
    }

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

        if (collision.CompareTag("GrabPlatform")&& playerInput.isJump)
        {
            isWallStay = false;
        }
    }
}
