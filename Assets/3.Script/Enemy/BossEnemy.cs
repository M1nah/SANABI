using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    PlayerAttack playerAttack;
    PlayerController playerController;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;


    int enemyHp = 3; //enemy 총 hp
    int enemyHit; // enemy 데미지 담아둘 변수

    bool enemyOnDamage;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerController = GetComponent<PlayerController>();    
    }


    private void Enemy_Hit() //player가 enemy를 공격함
    {
        for(int i=0; i<enemyHp; i++) //쓸 필요가 있나? 어차피 enemyHP가 3으로 정해져있는데 
        {
            i = enemyHit;
        }

        if (playerAttack.isAtk && playerAttack.findEnemy) //player Attack이 true일떄
        {
            enemyOnDamage = true; // OnDamage가 true
            enemyHit--; //Hp가 1 깎임
        }
        else 
        {
            enemyOnDamage = false;
        }

        if(enemyHit == 0 || enemyHit <= 0)
        {
            enemy.gameObject.SetActive(false); 
            //비활성화가 아니라 움직이지 않게 하고 쓰러진 animation 내보내기
        }

    }

    private void Enemy_Damage() //enemy가 player를 공격함
    {
        /*
        1. 레이저 raycast 발사
        2. player의 위치를 따라다님
        3. playe가 1.5f시간동안 가만히 있으면 
        4. 레이저 위치 고정
        5. 2f 시간 뒤에 발사
        6. 레이저에 닿으면 playerHP --; (총 4)
        7. if(enemyHP가 0이되면 비활성화)
        */
    }

}
