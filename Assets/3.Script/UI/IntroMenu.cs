using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IntroMenu : MonoBehaviour
{


    [SerializeField] GameObject SANNABI_Logo;
    [SerializeField] GameObject IntroBtn;
    [SerializeField] GameObject levelpanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject QuitPanel;


    bool levelPanelActive=false;
    bool optionPanelActive=false;
    bool quitPanelActive=false;


    public void StartBtn()
    {

        levelPanelActive = true;
        SANNABI_Logo.SetActive(false);
        IntroBtn.SetActive(false);
        levelpanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape) && levelPanelActive)
        {
            Back();
            levelPanelActive = false;
        }
    }

    public void OptionBtn() 
    {
        optionPanelActive = true;
        SANNABI_Logo.SetActive(false);
        IntroBtn.SetActive(false);
        optionPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape) && optionPanelActive)//esc≈∞ æ»∏‘¿Ω
        {
            Back();
            optionPanelActive = false;
        }


    }
    public void QuitBtn()
    {
        quitPanelActive = true;
        SANNABI_Logo.SetActive(false);
        IntroBtn.SetActive(false);
        QuitPanel.SetActive(true);


        if (Input.GetKeyDown(KeyCode.Escape)  && quitPanelActive)
        {
            Back();
            quitPanelActive = false;
        }
    }



    public void Back()
    {
        SANNABI_Logo.SetActive(true);
        IntroBtn.SetActive(true);


        QuitPanel.SetActive(false);
        levelpanel.SetActive(false);
        optionPanel.SetActive(false);
    }


    public void ClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
