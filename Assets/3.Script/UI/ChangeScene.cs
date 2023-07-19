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

    public void StartButton()
    {
        StartCoroutine(Start_Co());
    }

    private IEnumerator Start_Co()
    {
        fadeinout.ActiveFadeImage(true); //�� ��ũ��Ʈ�� intro�������ۿ� �Ⱦ��̱� ������ ������Ʈ�� �������ִ� �޼��带 �ҷ��Դ�
        yield return null;
        fadeinout.Fade(true);
        yield return new WaitForSeconds(fadeinout.fadeTime);
        SceneManager.LoadScene(1);
    }

}
