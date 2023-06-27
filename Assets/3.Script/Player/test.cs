using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform grabHook;
    public Transform player;

    //Camera cam; // <-> 플레이어 위치로 바꾸기
    public LayerMask GrabPlatform; //<-> 태그로 구분

    RaycastHit2D hit;
    LineRenderer line; //왜 위치가 0 0 0 이지?

    bool onGrapping = false;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndShoot();
        }
        DrawRope();
    }

    private void RopeShoot()
    {
        if(Physics2D.Raycast(player.transform.position, grabHook.position, GrabPlatform)) //위치, 방향, 거리 
        {
            Debug.Log("Platform 검출");//마우스 커서에 따라 레이캐스트 쏘기

            onGrapping = true;
            line.positionCount = 2;
            line.SetPosition(0, this.transform.position); //player의 포지션
            line.SetPosition(1, hit.point); //grabhook의 포지션
        }
    }

    private void EndShoot()
    {
        onGrapping = false;
        line.positionCount = 0;
    }

    private void DrawRope()
    {
        if (onGrapping)
        {
            line.SetPosition(0,this.transform.position);
        }
    }

}
