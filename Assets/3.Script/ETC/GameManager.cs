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
    //�÷��̾� ��Ʈ�ѷ��� �ż� ������Ʈ ��ü�� ���鼭 ������ ���߰����� 

    public bool isMenuOpen = false;


    //Player HP && Die
    int playerHP;
    bool isCoroutineLock = false;

    [SerializeField] GameObject deadImage;
    [SerializeField] CinemachineVirtualCamera playerDeadCam; //�׾��� �� �ణ Ȯ��Ǵ� ī�޶�

    [SerializeField] Animator playerDeadAni;
    [SerializeField] GameObject playerArm; //dead�ִϸ��̼��� ����� �� ��������

    [SerializeField] GameObject [] HPHud; //HP UI �̹���


    //���̵��ξƿ�
    FadeInOut fadeInOut;

    //die audio
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource BGM_Bird;


    private void Start()
    {
        playerRgd = Player.GetComponent<Rigidbody2D>();
        fadeInOut = FindObjectOfType<FadeInOut>();

        //���� �ҷ��� ������ ���̵���
        fadeInOut.Fade(false);

        //BGM Roop
        StartCoroutine(BGM_Co());

        //���̵��� ���� HP ���� �ʱ�ȭ 
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
            //HPReset�ڷ�ƾ;
            StartCoroutine(HPReset_Co()); //����� ������ hp1�ϰ�찡 �����ϴϱ� ���� �ڷ�ƾ�� stop�� �ʿ䰡 ����! 
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
        //�׾��� �� player�� �پ��ִ� ������Ʈ ��ũ��Ʈ�� ���� 
        Player.GetComponent<PlayerInput>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<PlayerHookShot>().enabled = false;
        Player.GetComponent<DastGhost>().enabled = false;

        BGM.Stop();
        BGM_Bird.Stop();


        //����� ���
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

        //���̵�ƿ�
        fadeInOut.Fade(true);
        yield return new WaitForSeconds(fadeInOut.fadeTime);
        yield return null;
        ChangeScene(1); //�� �ǵ��ư���(����)
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void BackToGameBtn()
    {
        //�޴��� ������ �� player�� �پ��ִ� ������Ʈ�� �ѱ� 
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
