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
    #endregion //인스턴스 선언

    [SerializeField] GameObject MenuPanelUI;
    [SerializeField] GameObject Player;
    Rigidbody2D playerRgd;
    //플레이어 컨트롤러와 훅샷 컴포넌트 자체를 끄면서 움직임 멈추게하자 

    public bool isMenuOpen =false;


    //Player HP && Die
    int playerHP = 4;
    bool isCoroutineLock =false;
    
    [SerializeField] GameObject deadImage;
    [SerializeField] CinemachineVirtualCamera playerDeadCam; //죽었을 때 약간 확대되는 카메라

    [SerializeField] Animator playerDeadAni;
    [SerializeField] GameObject playerArm; //dead애니메이션이 실행될 때 꺼져야함


    //페이드인아웃
    FadeInOut fadeInOut;

    private void Start()
    {
        playerRgd = Player.GetComponent<Rigidbody2D>();
        fadeInOut = FindObjectOfType<FadeInOut>();

        //씬이 로드 될때마다 페이드인
        fadeInOut.Fade(false);
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
        
        if (playerHP <= 0)
        {
            Die();
        }
        else if(!isCoroutineLock)
        {
            isCoroutineLock = true;
            StartCoroutine(Hp_Co());
        }
        
    }


    private IEnumerator Hp_Co()
    {
        Vector2 saveVelocity = playerRgd.velocity;

        yield return new WaitForSeconds(0.1f);

        playerRgd.velocity = new Vector2(saveVelocity.x * -1, saveVelocity.y * -1).normalized * 10;
        isCoroutineLock = false;
    }

    public void Die()
    {

        //죽었을 때 player에 붙어있는 컴포넌트 스크립트들 끄기 
        Player.GetComponent<PlayerInput>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<PlayerHookShot>().enabled = false;
        Player.GetComponent<DastGhost>().enabled = false;

        StartCoroutine(DieScene_co());

        //플레이어 모든 움직임 멈춤 
        //deadUI 활성화
        //씬 로드하기
    }

    private IEnumerator DieScene_co() //데드씬
    {
        playerDeadCam.gameObject.SetActive(true);
        deadImage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerArm.SetActive(false);
        playerDeadAni.SetTrigger("isDie");
        //여기 플레이어 다이 애니메이션 true
        yield return new WaitForSeconds(2);

        //페이드아웃
        fadeInOut.Fade(true);
        yield return new WaitForSeconds(fadeInOut.fadeTime);
        yield return null;
        ChangeScene(1); //씬 되돌아가기
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

}
