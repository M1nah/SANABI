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
    #endregion //�ν��Ͻ� ����

    [SerializeField] GameObject MenuPanelUI;
    [SerializeField] GameObject Player;
    Rigidbody2D playerRgd;
    //�÷��̾� ��Ʈ�ѷ��� �ż� ������Ʈ ��ü�� ���鼭 ������ ���߰����� 

    public bool isMenuOpen =false;


    //Player HP && Die
    int playerHP = 4;
    bool isCoroutineLock =false;
    
    [SerializeField] GameObject deadImage;
    [SerializeField] CinemachineVirtualCamera playerDeadCam; //�׾��� �� �ణ Ȯ��Ǵ� ī�޶�

    [SerializeField] Animator playerDeadAni;
    [SerializeField] GameObject playerArm; //dead�ִϸ��̼��� ����� �� ��������


    //���̵��ξƿ�
    FadeInOut fadeInOut;

    private void Start()
    {
        playerRgd = Player.GetComponent<Rigidbody2D>();
        fadeInOut = FindObjectOfType<FadeInOut>();

        //���� �ε� �ɶ����� ���̵���
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

        //�׾��� �� player�� �پ��ִ� ������Ʈ ��ũ��Ʈ�� ���� 
        Player.GetComponent<PlayerInput>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<PlayerHookShot>().enabled = false;
        Player.GetComponent<DastGhost>().enabled = false;

        StartCoroutine(DieScene_co());

        //�÷��̾� ��� ������ ���� 
        //deadUI Ȱ��ȭ
        //�� �ε��ϱ�
    }

    private IEnumerator DieScene_co() //�����
    {
        playerDeadCam.gameObject.SetActive(true);
        deadImage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerArm.SetActive(false);
        playerDeadAni.SetTrigger("isDie");
        //���� �÷��̾� ���� �ִϸ��̼� true
        yield return new WaitForSeconds(2);

        //���̵�ƿ�
        fadeInOut.Fade(true);
        yield return new WaitForSeconds(fadeInOut.fadeTime);
        yield return null;
        ChangeScene(1); //�� �ǵ��ư���
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

}
