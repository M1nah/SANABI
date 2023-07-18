using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject MenuPanelUI;
    [SerializeField] GameObject Player;
    //�÷��̾� ��Ʈ�ѷ��� �ż� ������Ʈ ��ü�� ���鼭 ������ ���߰����� 
    

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

            //�޴��� ������ �� player�� �پ��ִ� ������Ʈ ��ũ��Ʈ�� ���� 
            Player.GetComponent<PlayerInput>().enabled = false;
            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponent<PlayerHookShot>().enabled = false;
            Player.GetComponent<DastGhost>().enabled = false;

            isMenuOpen = true;
            MenuPanelUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            //�޴��� ������ �� player�� �پ��ִ� ������Ʈ�� �ѱ� 
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

        //���İ�(a)�� 1���� ������ a�� ����
        if (color.a < 1)
        {
            color.a += Time.deltaTime*0.5f;
        }
        fadeImage.color = color;
    }
}
