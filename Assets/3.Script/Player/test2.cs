using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour //dash ...÷���� �Ẹ��
{
    Rigidbody2D rgd;

    float defaultSpeed; 
    public float speed; //<=> moveSpeed ���� ��ü(���� player speed) 
    public float dashSpeed;
    public float defaultTime;//�⺻ �ð�
    float dashTime; //dash �ð�


    bool isDash;

    private void Start()
    {
        defaultSpeed = speed; //��...? �׳� speed �������ָ� �ȵ�?
        rgd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        rgd.velocity = new Vector2(hor * defaultSpeed, rgd.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDash = true;
        }

        if(dashTime <= 0)
        {
            //dashtime�� 0���� ���� �� ����Ʈ�� ������ dashtime�� defaulttime���� �ʱ�ȭ
            defaultSpeed = speed;
            if (isDash)
            {
                dashTime = defaultTime;
            }
        }
        else
        {
            //�� �ܿ��� dashtime�� �������� delftatime��ŭ ���ֱ�....��? 
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        isDash = false;

    }
}
