using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour //Hookshot(Hook)
{
    [SerializeField] PlayerHookShot grapping;
    [SerializeField] public DistanceJoint2D joint2D;

    private void Start()
    {
        //grapping은 "Player" 태그가 붙은 GameObject를 찾아서 거기에 붙은 test 컴포넌트를 가져오다는 뜻
        //직접 변수를 할당해줘도 괜춘함
        //grapping = GameObject.Find("Player").GetComponent<test>();
        //joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform"))
        {
            joint2D.enabled = true;
            grapping.isAttach = true;
        }
    }

}
