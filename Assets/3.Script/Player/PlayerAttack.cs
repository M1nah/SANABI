using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour //후크에 스크립트 다는게 좋겠음 
{

    CursorPoint cursor;
    PlayerController playerController;
    PlayerHookShot hookShot;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] float atkSpeed;

    float hitCount;
    float enemyHP = 3;


    bool findEnemy;
    bool isAtk;

    private void Awake()
    {
        cursor = GetComponent<CursorPoint>();
        playerController = GetComponent<PlayerController>();
        hookShot = GetComponent<PlayerHookShot>();
    }


    private void Update()
    {
            if (findEnemy && hitCount == 0)
            {
                if (Input.GetMouseButton(0))
                {
                    player.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
                    //player.transform.position += new Vector3(enemy.transform.position.x, enemy.transform.position.y);
                    //player는 enemy.position으로 이동
                    // => 마우스 버튼을 누르고 있을 동안 지속되야함
                    //gravityScale을 바꿨더니 오류 발생.. 
                    hitCount++;
                    //hookShot.enabled = false;
                    Attack();
                    Debug.Log("Enemy 클릭");
                }
            }
            else if(Input.GetMouseButtonUp(0) && hitCount>=1)
            {
                //hookShot.enabled = true;
                findEnemy = false;
                Debug.Log("Enemy 클릭 해제");
            }
    }

    private void Attack()
    {
        if(findEnemy && hitCount >= 1)
        {
            //using animation

        }
    }


    private void Enemy()
    {
        for(int i=0; i<enemyHP; i++)
        {
            enemyHP--;
            if (hitCount <= 0) //맞기
            {
                
            }
            else
            {

            }
        }

        if(enemyHP == 0) //죽음
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            findEnemy = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            findEnemy = true;
            isAtk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            findEnemy = false;
            isAtk = false;
            //hit reset
            hitCount = 0;
        }
        
    }

}
