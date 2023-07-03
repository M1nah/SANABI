using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //moving부터 시작하는 리코딩 생활 하...
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

    float isRight = 1f; //바라보는 방향 1= right , -1 = Left

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
            //2D에서는 기본적으로 오른쪽을 바라보고 있기 때문에 Vetor2D의 값을 right로 지정한다
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
            Debug.Log("벽에 닿았으며 미끄러져내려감"); //작동은 하는데 천천히 미끄러지지가 않음 
                                                       //왼쪽(-)은 작동하는데 오른쪽(+)은 작동안함
                                                       //collider가 너무 얇아서 검출 안되던 거였다...box로 바꾸니 잘됨 이런젠장
                                                       // 그렇다면 왼쪽 collider는 두꺼웠던것인가
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
