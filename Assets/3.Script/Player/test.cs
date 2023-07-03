using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //moving���� �����ϴ� ���ڵ� ��Ȱ ��...
{
    //Climing && slide 
    [SerializeField] Rigidbody2D wallRgd;
    [SerializeField] float sligingSpeed;


    public Transform wallCheck;
    public float wallChkDistance;
    [SerializeField] LayerMask wallLayer;

    Vector2 playerDir;

    bool isWall;

    float isRight=1f; //�ٶ󺸴� ���� 1= right , -1 = Left

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
        //2D������ �⺻������ �������� �ٶ󺸰� �ֱ� ������ Vetor2D�� ���� right�� �����Ѵ�
        //->raycast�� �ٶ󺸰��ִ� ������ ���������� �ʳ�... ���ε־��ϳ�
        Debug.DrawRay(wallCheck.position, Vector2.right * isRight, Color.red);
        
        //playerAni.Setbool("�������̵�",isWall);
    }

    private void FixedUpdate()
    {
        if (isWall) 
        {
            wallRgd.velocity = new Vector2(wallRgd.velocity.x, wallRgd.velocity.y*sligingSpeed);
            Debug.Log("���� ������� �̲�����������" + sligingSpeed); //�۵��� �ϴµ� õõ�� �̲��������� ���� 
        }   
    }
}
