using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    FadeInOut fadeinout;

    private void Start()
    {
        fadeinout = FindObjectOfType<FadeInOut>();
    }


    public void StartButton(int  levelTypeNum)//버튼 클릭 메서드 
    {
        PlayerPrefs.SetInt("Level", levelTypeNum); //player 프리팹을 자동으로 만들어줌
        StartCoroutine(Start_Co());
    }

    private IEnumerator Start_Co()
    {
        fadeinout.ActiveFadeImage(true); //이 스크립트는 intro씬에서밖에 안쓰이기 때문에 컴포넌트를 끄고켜주는 메서드를 불러왔다
        yield return null;
        fadeinout.Fade(true);
        yield return new WaitForSeconds(fadeinout.fadeTime);
        SceneManager.LoadScene(1);
    }

    public void BacktoMain()
    {
        SceneManager.LoadScene(0);
    }

}
