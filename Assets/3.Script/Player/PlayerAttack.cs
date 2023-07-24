using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
    CursorPoint cursor;
    PlayerController playerController;
    PlayerHookShot hookShot;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] float atkSpeed;

    public float hitCount;

    public bool findEnemy;
    public bool isAtk;

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
                    //player�� enemy.position���� �̵�
                    // => ���콺 ��ư�� ������ ���� ���� ���ӵǾ���
                    //gravityScale�� �ٲ���� ���� �߻�.. 
                    hitCount++;
                    //hookShot.enabled = false;
                }
            }
            else if(Input.GetMouseButtonUp(0) && hitCount>=1)
            {
                //hookShot.enabled = true;
                findEnemy = false;
            }
    }

    private void Attack()
    {
        if(findEnemy && hitCount >= 1)
        {
            //using animation
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
