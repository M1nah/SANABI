using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public GameObject fade;
    public Image fadeImage;

    [SerializeField] [Range(0.01f,10f)]
    public float fadeTime; // fadeSpeed값이 10이면 1초 (값이 클수록 빠름)


        //fade-In. 배경의 알파값이 1에서 0으로(화면이 점점 밝아진다)
        //StartCoroutine(Fade_Co(1,0));

        //fade-Out. 배경의 알파값이 0에서 1로(화면이 점점 어두워진다)
        //StartCoroutine(Fade_Co(0,1));


        //<페이드인아웃 사용방법> 
        //호출됐을 때 fade오브젝트의 image컴포넌트를 켠다
        //만약 image 알파값이 0보다 작다면 fade-In
        //만약 image 알파값이 1보다 크다면 fade-out;
        //실행이 끝나면 fade 오브젝트의 컴포넌트를 끈다

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
            //fadeTime으로 나누어서 fadeTime 시간 동안
            //percent값이 1에서 -> 1로 증가하도록 함
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            //알파값을 start부터 ene까지 fadeTime동안 변화시킨다
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;

            yield return null;
        }
    }



    //컴포넌트가 켜져있으면 안되는 씬도 있어서 ex.ui밖에없는 인트로 씬
    //일부러 컴포넌트를 끄고 켜주는 메서드를 따로 뺐다
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
