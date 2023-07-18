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
    #endregion //�ν��Ͻ� ����


    [SerializeField] GameObject MenuPanelUI;
    [SerializeField] GameObject Player;
    Rigidbody2D playerRgd;
    //�÷��̾� ��Ʈ�ѷ��� �ż� ������Ʈ ��ü�� ���鼭 ������ ���߰����� 

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



    public void FadeOut()
    {
        Color color = fadeImage.color;

        //���İ�(a)�� 1���� ������ a�� ����
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

        //�׾��� �� player�� �پ��ִ� ������Ʈ ��ũ��Ʈ�� ���� 
        Player.GetComponent<PlayerInput>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<PlayerHookShot>().enabled = false;
        Player.GetComponent<DastGhost>().enabled = false;


        //�÷��̾� ��� ������ ���� 
        //deadUI Ȱ��ȭ
        //�� �ε��ϱ�
    }

    private IEnumerator DieScene_co()
    {
        /*
         1. ���� ������Ʈ ��� .setActive(true) 
         2. �÷��̾� ���� �ִϸ��̼� �������� +�۸�ġ����Ʈ
         3. 3�� ���� �д���
                => ������Ʈ ��� setActive(false)
         4. �� ���� ó������ �̵� ���̵���
         5. player�� ���� ��� �ĸ��Ʈ ��ũ��Ʈ�� �ѱ�
        */
        
        
        deadImage.SetActive(true);
        //���� �÷��̾� ���� �ִϸ��̼� true
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
