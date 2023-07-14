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

        //알파값(a)이 1보다 작으면 a값 증가
        if (color.a < 1)
        {
            color.a += Time.deltaTime * 0.5f;
        }

        //바뀐 색상 정보를 fadeImage.coloer에 저장
        fadeImage.color = color;
    }
}
