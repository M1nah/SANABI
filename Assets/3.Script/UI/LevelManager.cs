using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void SelectLevelBtn(int levelTypeNum) //��ư Ŭ�� �޼��� 
    {
        PlayerPrefs.SetInt("Level", levelTypeNum); //player �������� �ڵ����� �������
    }
}
