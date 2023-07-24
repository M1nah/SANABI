using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    #region instance
    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] GameObject MenuPanelUI;
    [SerializeField] GameObject Player;
    Rigidbody2D playerRgd;
    //플레이어 컨트롤러와 훅샷 컴포넌트 자체를 끄면서 움직임 멈추게하자 

    public bool isMenuOpen = false;


    //Player HP && Die
    int playerHP;
    bool isCoroutineLock = false;

    [SerializeField] GameObject deadImage;
    [SerializeField] CinemachineVirtualCamera playerDeadCam; //죽었을 때 약간 확대되는 카메라

    [SerializeField] Animator playerDeadAni;
    [SerializeField] GameObject playerArm; //dead애니메이션이 실행될 때 꺼져야함

    [SerializeField] GameObject [] HPHud; //HP UI 이미지


    //페이드인아웃
    FadeInOut fadeInOut;

    //die audio
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource BGM_Bird;


    private void Start()
    {
        playerRgd = Player.GetComponent<Rigidbody2D>();
        fadeInOut = FindObjectOfType<FadeInOut>();

        //씬을 불러올 때마다 페이드인
        fadeInOut.Fade(false);

        //BGM Roop
        StartCoroutine(BGM_Co());

        //난이도에 따른 HP 세팅 초기화 
        if(PlayerPrefs.GetInt("Level") == 3)
        {
            playerHP = 1;
        }
        else
        {
            playerHP = 4;
        }

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


    public void HP()
    {
        playerHP--;
        
        HPCondition();

        if (playerHP <= 0)
        {
            Die();
        }
        else if (!isCoroutineLock)
        {
            isCoroutineLock = true;
            StartCoroutine(Hp_Co());
        }


        if (playerHP == 1)
        {
            //HPReset코루틴;
            StartCoroutine(HPReset_Co()); //여기는 조건이 hp1일경우가 유일하니까 따로 코루틴을 stop할 필요가 없음! 
        }
    }

    public void HPCondition()
    {
        switch (playerHP)
        {
            case 1:
                HPHud[0].SetActive(true);
                HPHud[1].SetActive(false);
                HPHud[2].SetActive(false);
                HPHud[3].SetActive(false);
                break;

            case 2:
                HPHud[0].SetActive(false);
                HPHud[1].SetActive(true);
                HPHud[2].SetActive(false);
                HPHud[3].SetActive(false);
                break;

            case 3:
                HPHud[0].SetActive(false);
                HPHud[1].SetActive(false);
                HPHud[2].SetActive(true);
                HPHud[3].SetActive(false);
                break;

            case 4:
                HPHud[0].SetActive(false);
                HPHud[1].SetActive(false);
                HPHud[2].SetActive(false);
                HPHud[3].SetActive(true);
                break;
        }
    }


    private IEnumerator HPReset_Co()
    {
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            yield return new WaitForSeconds(5f);
            playerHP = 4;
            HPCondition();
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            yield return new WaitForSeconds(5f);
            playerHP = 2;
            HPCondition();
        }
    }

    private IEnumerator Hp_Co()
    {
        Vector2 saveVelocity = playerRgd.velocity;
        yield return new WaitForSeconds(0.2f);
        playerRgd.velocity = new Vector2(saveVelocity.x * -1, saveVelocity.y * -1).normalized * 8;
        yield return new WaitForSeconds(2f);
        isCoroutineLock = false;
    }

    public void Die()
    {
        //죽었을 때 player에 붙어있는 컴포넌트 스크립트들 끄기 
        Player.GetComponent<PlayerInput>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<PlayerHookShot>().enabled = false;
        Player.GetComponent<DastGhost>().enabled = false;

        BGM.Stop();
        BGM_Bird.Stop();


        //데드씬 출력
        StartCoroutine(DieScene_co());

    }

    private IEnumerator DieScene_co() 
    {
        playerDeadCam.gameObject.SetActive(true);
        deadImage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerArm.SetActive(false);
        playerDeadAni.SetTrigger("isDie");
        yield return new WaitForSeconds(2);

        //페이드아웃
        fadeInOut.Fade(true);
        yield return new WaitForSeconds(fadeInOut.fadeTime);
        yield return null;
        ChangeScene(1); //씬 되돌아가기(리셋)
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void BackToGameBtn()
    {
        //메뉴가 닫혔을 때 player에 붙어있는 컴포넌트들 켜기 
        Player.GetComponent<PlayerInput>().enabled = true;
        Player.GetComponent<PlayerController>().enabled = true;
        Player.GetComponent<PlayerHookShot>().enabled = true;
        Player.GetComponent<DastGhost>().enabled = true;

        isMenuOpen = false;
        MenuPanelUI.SetActive(false);
    }


    public IEnumerator BGM_Co()
    {
        yield return new WaitForSeconds(1.2f);
        BGM_Bird.Play();
        yield return new WaitForSeconds(6f);
        BGM.Play();
    }
}
