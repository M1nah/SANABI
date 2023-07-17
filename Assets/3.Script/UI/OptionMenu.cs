using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    FullScreenMode screenMode;

    public Dropdown resolutionDropdown;
    public Toggle fullSceenBtn;

    List<Resolution> resolutions = new List<Resolution>();

    int resolutionNum;


    // Start is called before the first frame update
    void Start()
    {
        InitUI();

    }

    void InitUI() //�ػ� ���
    {
        for(int i = 0; i<Screen.resolutions.Length; i++)
        {
            if(Screen.resolutions[i].refreshRate == 75) 
            {//�ػ� ���� ������ �� ȭ�� ��� �󵵰� 75�� ���� �����ͼ� �ػ� list�� �־��ֱ�
                resolutions.Add(Screen.resolutions[i]);
            }
        }
         //==> Ư���� �ػ� ���� ��� ������ �� ����?

        //resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
                optionNum++;
            }
        }
        resolutionDropdown.RefreshShownValue(); //���ΰ�ħ �Լ�

        //Ǯ��ũ����� �ʱ�ȭ
        fullSceenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        //screenMode�� isFull(��)�̸� Ǯ��ũ��, �����̸� ������ ���
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
 
    public void OkBtnClick() //�ػ� ���� ��ư
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

}
