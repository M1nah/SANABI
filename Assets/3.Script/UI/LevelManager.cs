using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
    [0] easy = HP�� �ȱ��� => ���� ��Ƶ� HP = 4; 
    [1] normal = Hp�� 1�� �Ǹ� => ������ �ð��� ������(5f) HP=4;
    [2] hard = Hp�� 1�� �Ǹ� => ������ �ð��� ������ (5f) HP =2; 
    [3] superHard = HP =1; (���)
     */


    public void SelectLevelBtn(int levelTypeNum) //��ư Ŭ�� �޼��� 
    {
        PlayerPrefs.SetInt("Level", levelTypeNum); //player �������� �ڵ����� �������
    }



}
