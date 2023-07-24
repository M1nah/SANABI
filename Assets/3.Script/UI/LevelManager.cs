using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void SelectLevelBtn(int levelTypeNum) //버튼 클릭 메서드 
    {
        PlayerPrefs.SetInt("Level", levelTypeNum); //player 프리팹을 자동으로 만들어줌
    }
}
