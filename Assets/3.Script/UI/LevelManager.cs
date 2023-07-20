using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
    [0] easy = HP가 안깎임 => 발판 밟아도 HP = 4; 
    [1] normal = Hp가 1이 되면 => 일정한 시간이 지나면(5f) HP=4;
    [2] hard = Hp가 1이 되면 => 일정한 시간이 지나면 (5f) HP =2; 
    [3] superHard = HP =1; (즉사)
     */


    public void SelectLevelBtn(int levelTypeNum) //버튼 클릭 메서드 
    {
        PlayerPrefs.SetInt("Level", levelTypeNum); //player 프리팹을 자동으로 만들어줌
    }



}
