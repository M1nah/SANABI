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


    //bgm
    public AudioSource BGM;



    // Start is called before the first frame update
    void Start()
    {
        InitUI();

        if (PlayerPrefs.GetFloat("Volume") != 0)
        {
            BGM.volume = PlayerPrefs.GetFloat("Volume");
        }
    }

    void InitUI() //해상도 목록
    {
        for(int i = 0; i<Screen.resolutions.Length; i++)
        {
                Debug.Log(resolutions[i]);
            if(Screen.resolutions[i].refreshRate == 75) 
            {//해상도 값을 가져올 때 화면 재생 빈도가 75인 값만 가져와서 해상도 list에 넣어주기
                resolutions.Add(Screen.resolutions[i]);
            }
        }
         //==> 특정한 해상도 값을 골라서 가져올 수 없나?

        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            Debug.Log("해상도 가져오기" + option);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
                optionNum++; //↓
            }
        }
        resolutionDropdown.RefreshShownValue(); //새로고침 함수

        //풀스크린모드 초기화
        fullSceenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        //screenMode가 isFull(참)이면 풀스크린, 거짓이면 윈도우 모드
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
 
    public void OkBtnClick() //해상도 변경 버튼
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }


    //BGM Volume
    public void SetMusicVol(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        BGM.volume = volume;

        //씬 넘어가서도 설정 꺼지지 않게...
        //DontDestroyOnLoad(this.gameObject);
    }



}
