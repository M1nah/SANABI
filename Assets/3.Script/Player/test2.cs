using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour //dash ...첨부터 써보자
{
    Rigidbody2D rgd;

    float defaultSpeed; 
    public float speed; //<=> moveSpeed 변수 대체(기존 player speed) 
    public float dashSpeed;
    public float defaultTime;//기본 시간
    float dashTime; //dash 시간


    bool isDash;

    private void Start()
    {
        defaultSpeed = speed; //왜...? 그냥 speed 지정해주면 안돼?
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
            //dashtime이 0보다 작을 때 쉬프트가 눌리면 dashtime을 defaulttime으로 초기화
            defaultSpeed = speed;
            if (isDash)
            {
                dashTime = defaultTime;
            }
        }
        else
        {
            //그 외에는 dashtime을 매프레임 delftatime만큼 빼주기....왜? 
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        isDash = false;

    }
}
