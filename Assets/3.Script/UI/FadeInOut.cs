using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public GameObject fade;
    public Image fadeImage;

    [SerializeField] [Range(0.01f,10f)]
    public float fadeTime; // fadeSpeed���� 10�̸� 1�� (���� Ŭ���� ����)


        //fade-In. ����� ���İ��� 1���� 0����(ȭ���� ���� �������)
        //StartCoroutine(Fade_Co(1,0));

        //fade-Out. ����� ���İ��� 0���� 1��(ȭ���� ���� ��ο�����)
        //StartCoroutine(Fade_Co(0,1));


        //<���̵��ξƿ� �����> 
        //ȣ����� �� fade������Ʈ�� image������Ʈ�� �Ҵ�
        //���� image ���İ��� 0���� �۴ٸ� fade-In
        //���� image ���İ��� 1���� ũ�ٸ� fade-out;
        //������ ������ fade ������Ʈ�� ������Ʈ�� ����

    public void Fade(bool fade_in)
    {
        if (fade_in)
        {
        StartCoroutine(Fade_Co(0, 1));
        }
        else
        {
        StartCoroutine(Fade_Co(1,0));
        }
    }

    private IEnumerator Fade_Co(float start, float end)
    {

        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent<1)
        {
            //fadeTime���� ����� fadeTime �ð� ����
            //percent���� 1���� -> 1�� �����ϵ��� ��
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            //���İ��� start���� ene���� fadeTime���� ��ȭ��Ų��
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;

            yield return null;
        }
    }



    //������Ʈ�� ���������� �ȵǴ� ���� �־ ex.ui�ۿ����� ��Ʈ�� ��
    //�Ϻη� ������Ʈ�� ���� ���ִ� �޼��带 ���� ����
    public void ActiveFadeImage(bool isActive)
    {
        if (isActive) 
        { 
        fade.GetComponent<Image>().enabled = true;
        }
        else
        {
        fade.GetComponent<Image>().enabled = false;
        }

    }

}
