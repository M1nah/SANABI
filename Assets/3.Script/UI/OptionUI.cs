using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionUI : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();

    int resolutionNum;

    //bgm
    //public AudioSource BGM;
    public AudioMixer audioMixer;

    private void Start()
    {
        InitUI();

        //if (PlayerPrefs.GetFloat("Volume") != 0)
        //{
        //    BGM.volume = PlayerPrefs.GetFloat("Volume");
        //}
    }

    public void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
                optionNum++;
            }
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void FullScreenBtnClick(bool isFullSceen)
    {
        screenMode = isFullSceen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void SelectResolutionsBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
    }

    //BGM Volume
    public void SetMusicVol(float volume)
    {
        //씬 넘어가서도 설정 꺼지지 않게...
        PlayerPrefs.SetFloat("Volume", volume);

        audioMixer.SetFloat("MasterParam", volume);
        audioMixer.SetFloat("SFX", volume);
    }

}
