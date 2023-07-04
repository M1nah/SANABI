using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour //Hookshot 
{
    public LineRenderer line;
    public Transform hook;

    Vector2 mouseDirection;

    private void Start()
    {
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0,transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;//라인렌더러가 추가되어있는 오브젝트의 위치와 상관없이 월드 좌표를 기준으로 화면에 라인이 그려지게 됨
    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(0))
        {
            hook.position = transform.position;
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
    }

}
