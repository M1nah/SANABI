using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //fade-in-out
    public Image fadeImage;


    //Player HP && Die
    int playerHP = 4;
    bool isCoroutineLock =false;
    
    [SerializeField] GameObject deadImage;



    private void Start()
    {
        playerRgd = Player.GetComponent<Rigidbody2D>();
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



    public void FadeOut()
    {
        Color color = fadeImage.color;

        //알파값(a)이 1보다 작으면 a값 증가
        if (color.a < 1)
        {
            color.a += Time.deltaTime*0.5f;
        }
        fadeImage.color = color;
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

        yield return new WaitForSeconds(2);

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


        //플레이어 모든 움직임 멈춤 
        //deadUI 활성화
        //씬 로드하기
    }

    private IEnumerator DieScene_co()
    {
        /*
         1. 검은 오브젝트 배경 .setActive(true) 
         2. 플레이어 다이 애니메이션 내보내고 +글리치이펙트
         3. 3초 텀을 둔다음
                => 오브젝트 배경 setActive(false)
         4. 씬 가장 처음으로 이동 페이드인
         5. player에 붙은 모든 컴모넌트 스크립트들 켜기
        */
        
        
        deadImage.SetActive(true);
        //여기 플레이어 다이 애니메이션 true
        yield return new WaitForSeconds(3);
        deadImage.SetActive(false);
        ChangeScene(1);

        Player.GetComponent<PlayerInput>().enabled = true;
        Player.GetComponent<PlayerController>().enabled = true;
        Player.GetComponent<PlayerHookShot>().enabled = true;
        Player.GetComponent<DastGhost>().enabled = true;

    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }


}
