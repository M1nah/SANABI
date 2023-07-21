using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookShot : MonoBehaviour //hookshot && dash
{

    [Header("SFX")]
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip playerHookShot;
    [SerializeField] private AudioClip playerDash;
    [Space]



    [SerializeField] GameObject GrabHook;
    public Transform hook;
    public LineRenderer line;
    public LineRenderer guideline;


    Vector2 mouseDirection; //���콺 Ŀ�� ��ġ

    public bool isHookActive; //���콺 ��ư�� ������ �� ������ �ٲ��ִ� ����
    public bool isLineMax;
    public bool isAttach = false; // �� ������ ���� �� platform�� �ٴ´� linemax�� �ߵ� ���� 


    //dash
    PlayerController playerController;
    PlayerInput playerInput;



    //public float speed; //<=> moveSpeed ���� ��ü(���� player speed) 
    public float dashSpeed;
    public float defaultTime;//�⺻ �ð�
    [SerializeField] float dashTime; //dash �ð�

    public bool isDirection = false;

    public bool isDash = false; //dash ����

    public Animator ani;

    //ETC
    public DastGhost ghost; //DashGhost

    private void Awake()
    {
        //dash
        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.02f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        line.useWorldSpace = true;
        //����η������� �߰��Ǿ��ִ� ������Ʈ�� ��ġ�� �������
        //���� ��ǥ�� �������� ȭ�鿡 ������ �׷����� ��
    }

    private void Update()
    {
        //hook�� player�� ��ġ�� �ٲ�� �������� ����ǰ�
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(0) && !isHookActive)
        {
            //hoook�� player�� ��ġ���� ���ư����ϴϱ�
            //���콺 ��ư�� ������ �� hook�� ��ġ�� player�� ��ġ�� �ʱ�ȭ �����ش�
            hook.position = transform.position;

            //���콺 �������� ��ũ�� �������� ���� ��ȯ�ϴϱ� ���� ��ǥ�� �ٲ��� ��
            //player�� ��ġ���� ���ָ� ���� ���ư��� ������ ���Ͱ��� �˼� �ִ�
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            isHookActive = true;
            isLineMax = false;
            GrabHook.SetActive(true);
            playerAudio.PlayOneShot(playerHookShot);
        }

        if (isHookActive && !isLineMax && !isAttach) //isHookActive�� ���̰� lineMax�� ������ ���� ��ũ�� ���ư��Բ� �ϱ�
        {
            hook.Translate(mouseDirection.normalized * Time.deltaTime * 10);


            if (Vector2.Distance(transform.position, hook.position) > 2) //Distance(a,b)=> a���� b������ �Ÿ����ϱ��Լ�
            {
                //hook�� player�� �Ÿ��� 5���� Ŭ �� lineMax�� ���̴�
                isLineMax = true;
            }
        }
        else if (isHookActive && isLineMax && !isAttach)
        { //���콺 ��ư�� ���� isHookActive�� ���̰� line�� �ְ������� �������� ��(isLineMax�� ���϶�) ��ũ�� ���ƿ�
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 10);

            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            { //hook�� player�� ������ 0.1���� �۾����� isHookActive�� isLineMax�� ��Ȱ��ȭ => ���� �������� �ʰ� �Ѵ� 
                isHookActive = false;
                isLineMax = false;
                GrabHook.SetActive(false);
            }
        }
        else if (isAttach) //platform���� ��ũ�� ������ �� ��� bool ������ ��Ȱ��ȭ
        {
            if (Input.GetMouseButtonUp(0))
            {
                isAttach = false;
                isHookActive = false;
                //isDash = false;
                isLineMax = false;
                GrabHook.GetComponent<Hook>().joint2D.enabled = false;
                GrabHook.SetActive(false);

                if (isDirection && !isAttach && playerController.rigid.velocity.y >= 0) //���⿡ ���� �ӵ� ���ϱ�...���� ���ǽ� ���� �̻��� 
                {
                    playerController.rigid.AddForce(Vector2.right * 50, ForceMode2D.Force);
                    Debug.Log("11111" + Vector2.right);
                }
                else if (!isDirection && !isAttach && playerController.rigid.velocity.y >= 0)
                {
                    playerController.rigid.AddForce(Vector2.left * 50, ForceMode2D.Force);
                    Debug.Log("22222" + Vector2.left);
                }
            }

        }

        // dash�� �Է��ϰ� isAttach�� ���϶� ==> dash�� true
        if (Input.GetKeyDown(KeyCode.LeftShift) && isAttach)
        {
            isDash = true;
            playerAudio.PlayOneShot(playerDash);
            ghost.makeGhost = true; //�ܻ� on 

            if (isDirection) //dash ���� ����ְ� 
            {
                playerController.rigid.velocity = new Vector2(1 * dashSpeed, playerController.rigid.velocity.y);
            }
            else
            {
                playerController.rigid.velocity = new Vector2(-1 * dashSpeed, playerController.rigid.velocity.y);
            }
        }
        else if (!isAttach) //���� õ�忡 �پ����� ������ 
        {
            ghost.makeGhost = false; //�ܻ�off...
        }

        //õ�忡�� �������� ��ư�� ������ isash�� ���˶� ==> dashStay �ڷ�ƾ ����
        if (Input.GetMouseButtonUp(0) && isDash)
        {
            Debug.Log("dash �ڷ�ƾ ����"); //����
            StartCoroutine(DashStay_Co());
        }


        if (!isAttach && !playerController.isGround) //õ�忡 �������ְ� player�� ���� �پ����� ���� ��
        {
            playerController.rigid.velocity *= new Vector2(2, 1.2f); //�Ȱ��� player ������ٵ� ���� �����ؼ� ������ �������� ��
            Debug.Log("�۵���?"); //�۵���... �׷���  isAttach�� isGround bool ����װ� �� �̻���
        }
    }


        private IEnumerator DashStay_Co()
        {
            Vector2 dashStay = playerController.rigid.velocity; //player ������ٵ� �����ϴ� ���� 
            float timeCheck = 0;//�ڷ�ƾ �ð� üũ 

            while (true)
            {
                timeCheck += Time.deltaTime; //�ð��� �带����
                playerController.rigid.velocity = dashStay; // �� �����Ӹ��� player������ٵ�� dashSatay���� �ȴ� 

                yield return null;

                if (timeCheck >= 0.1) //�ð��� 0.1 ������ ��
                {
                    playerController.rigid.velocity *= new Vector2(2, 1.2f); //player������ٵ� ���� new Vector2�� �ٽ� �������ְ�
                    isDash = false; //dash�� ����
                    Debug.Log("dash �ڷ�ƾ ����"); //���� 
                    yield break;
                }
            }
        }
    } 

