using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    PlayerAttack playerAttack;
    PlayerController playerController;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;


    int enemyHp = 3; //enemy �� hp
    int enemyHit; // enemy ������ ��Ƶ� ����

    bool enemyOnDamage;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerController = GetComponent<PlayerController>();    
    }


    private void Enemy_Hit() //player�� enemy�� ������
    {
        for(int i=0; i<enemyHp; i++) //�� �ʿ䰡 �ֳ�? ������ enemyHP�� 3���� �������ִµ� 
        {
            i = enemyHit;
        }

        if (playerAttack.isAtk && playerAttack.findEnemy) //player Attack�� true�ϋ�
        {
            enemyOnDamage = true; // OnDamage�� true
            enemyHit--; //Hp�� 1 ����
        }
        else 
        {
            enemyOnDamage = false;
        }

        if(enemyHit == 0 || enemyHit <= 0)
        {
            enemy.gameObject.SetActive(false); 
            //��Ȱ��ȭ�� �ƴ϶� �������� �ʰ� �ϰ� ������ animation ��������
        }

    }

    private void Enemy_Damage() //enemy�� player�� ������
    {
        /*
        1. ������ raycast �߻�
        2. player�� ��ġ�� ����ٴ�
        3. playe�� 1.5f�ð����� ������ ������ 
        4. ������ ��ġ ����
        5. 2f �ð� �ڿ� �߻�
        6. �������� ������ playerHP --; (�� 4)
        7. if(enemyHP�� 0�̵Ǹ� ��Ȱ��ȭ)
        */
    }

}
