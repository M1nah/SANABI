using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
    }
    public void FadeOut()
    {
        Color color = fadeImage.color;

        //���İ�(a)�� 1���� ������ a�� ����
        if (color.a < 1)
        {
            color.a += Time.deltaTime * 0.5f;
        }

        //�ٲ� ���� ������ fadeImage.coloer�� ����
        fadeImage.color = color;
    }
}
