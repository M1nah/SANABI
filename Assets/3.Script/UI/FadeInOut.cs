using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private Image fadeImage;

    [SerializeField] Button levelText;
    ChangeScene changeScene;


    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        changeScene = GetComponent<ChangeScene>();
    }



    // Update is called once per frame
    void Update()
    {
        if (levelText)
        {

            fadeImage.enabled = true;
            StartCoroutine(FadeOut());

            changeScene.StartButton("01 Prologue");
        }
        else
        {
            return;
        }

    }
    private IEnumerator FadeOut()
    {
        Color color = fadeImage.color;


        //���İ�(a)�� 1���� ������ a�� ����
        if (color.a < 1)
        {
            color.a += Time.deltaTime * 0.5f;
        }

        //�ٲ� ���� ������ fadeImage.coloer�� ����
        fadeImage.color = color;

        yield return new WaitForSeconds(2f);


    }
}
