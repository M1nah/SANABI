using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject MenuPanelUI;
    [SerializeField] GameObject Player;
    //플레이어 컨트롤러와 훅샷 컴포넌트 자체를 끄면서 움직임 멈추게하자 
    

    public bool isMenuOpen =false;

    //fade-in-out
    public Image fadeImage;

    private void Start()
    {
        fadeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        MenuOpen();
    }

    public void MenuOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {

            //메뉴가 열렸을 때 player에 붙어있는 컴포넌트 스크립트들 끄기 
            Player.GetComponent<PlayerInput>().enabled = false;
            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponent<PlayerHookShot>().enabled = false;
            Player.GetComponent<DastGhost>().enabled = false;

            isMenuOpen = true;
            MenuPanelUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            //메뉴가 닫혔을 때 player에 붙어있는 컴포넌트들 켜기 
            Player.GetComponent<PlayerInput>().enabled = true;
            Player.GetComponent<PlayerController>().enabled = true;
            Player.GetComponent<PlayerHookShot>().enabled = true;
            Player.GetComponent<DastGhost>().enabled = true;

            isMenuOpen = false;
            MenuPanelUI.SetActive(false);
        }
    }


    private void FadeOut()
    {
        Color color = fadeImage.color;

        //알파값(a)이 1보다 작으면 a값 증가
        if (color.a < 1)
        {
            color.a += Time.deltaTime*0.5f;
        }
        fadeImage.color = color;
    }
}
