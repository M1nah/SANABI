using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform grabHook;
    public Transform player;

    //Camera cam; // <-> �÷��̾� ��ġ�� �ٲٱ�
    public LayerMask GrabPlatform; //<-> �±׷� ����

    RaycastHit2D hit;
    LineRenderer line; //�� ��ġ�� 0 0 0 ����?

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
        if(Physics2D.Raycast(player.transform.position, grabHook.position, GrabPlatform)) //��ġ, ����, �Ÿ� 
        {
            Debug.Log("Platform ����");//���콺 Ŀ���� ���� ����ĳ��Ʈ ���

            onGrapping = true;
            line.positionCount = 2;
            line.SetPosition(0, this.transform.position); //player�� ������
            line.SetPosition(1, hit.point); //grabhook�� ������
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
