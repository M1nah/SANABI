using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject MenuPanelUI;
    GameObject Player;
    //�÷��̾� ��Ʈ�ѷ��� �ż� ������Ʈ ��ü�� ���鼭 ������ ���߰����� 
    
    OptionMenu option;

    bool isMenuOpen =false;

    private void Start()
    {
        Player = GetComponent<GameObject>();
        option = GetComponent<OptionMenu>();
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
            isMenuOpen = true;
            MenuPanelUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            isMenuOpen = false;
            MenuPanelUI.SetActive(false);
        }
    }

}
