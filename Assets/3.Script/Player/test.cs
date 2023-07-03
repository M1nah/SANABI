using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //moving부터 시작하는 리코딩 생활 하...
{
    //Climing && slide 
    [SerializeField] Rigidbody2D wallRgd;
    [SerializeField] float sligingSpeed;


    public Transform wallCheck;
    public float wallChkDistance;
    [SerializeField] LayerMask wallLayer;

    Vector2 playerDir;

    bool isWall;

    float isRight=1f; //바라보는 방향 1= right , -1 = Left

    public GameObject Arm;
    Animator playerAni;
    Animator armAni;


    private void Awake()
    {
        wallRgd = GetComponent<Rigidbody2D>();

        playerAni = GetComponent<Animator>();
        armAni = Arm.GetComponent<Animator>();
    }

    private void Update()
    {
        isWall= Physics2D.Raycast(wallCheck.position, Vector2.right * isRight, wallChkDistance, wallLayer); 
        //2D에서는 기본적으로 오른쪽을 바라보고 있기 때문에 Vetor2D의 값을 right로 지정한다
        //->raycast가 바라보고있는 방향이 뒤집어지지 않네... 따로둬야하나
        Debug.DrawRay(wallCheck.position, Vector2.right * isRight, Color.red);
        
        //playerAni.Setbool("벽슬라이딩",isWall);
    }

    private void FixedUpdate()
    {
        if (isWall) 
        {
            wallRgd.velocity = new Vector2(wallRgd.velocity.x, wallRgd.velocity.y*sligingSpeed);
            Debug.Log("벽에 닿았으며 미끄러져내려감" + sligingSpeed); //작동은 하는데 천천히 미끄러지지가 않음 
        }   
    }
}
