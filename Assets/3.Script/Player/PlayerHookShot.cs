using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookShot : MonoBehaviour //hookshot && dash
{
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
    [SerializeField]float dashTime; //dash �ð�

    public bool isDirection = false;

    public bool isDash = false; //dash ����

    public Animator ani;

    //DashGhost
    public DastGhost ghost;


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
            Debug.Log("Hook �߻�");
        }

        if (isHookActive && !isLineMax && !isAttach) //isHookActive�� ���̰� lineMax�� ������ ���� ��ũ�� ���ư��Բ� �ϱ�
        {
            hook.Translate(mouseDirection.normalized * Time.deltaTime * 12);


            if (Vector2.Distance(transform.position, hook.position) > 2) //Distance(a,b)=> a���� b������ �Ÿ����ϱ��Լ�
            {
                //hook�� player�� �Ÿ��� 5���� Ŭ �� lineMax�� ���̴�
                isLineMax = true;
            }
        }
        else if (isHookActive && isLineMax && !isAttach)
        { //���콺 ��ư�� ���� isHookActive�� ���̰� line�� �ְ������� �������� ��(isLineMax�� ���϶�) ��ũ�� ���ƿ�
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 12);

            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            { //hook�� player�� ������ 0.1���� �۾����� isHookActive�� isLineMax�� ��Ȱ��ȭ => ���� �������� �ʰ� �Ѵ� 
                isHookActive = false;
                isLineMax = false;
                GrabHook.SetActive(false);
                Debug.Log("hook ȸ��");
            }
        }
        else if (isAttach) //platform���� ��ũ�� ������ �� ��� bool ������ ��Ȱ��ȭ
        {

            if (Input.GetMouseButtonUp(0))
            {
                /*
                 * <dash>
                 1. hook�� �������� (platform�� �Ŵ޷����� ������)
                 2. Dash�� ���� ��
                 3. player.rigid.velocity.y !=0��
                4.  ground�� ���� �ʾ��� �� 

                => Horizontal  �Է°��� �ް� �ű⿡ x��*� �ӵ�?(�̰� ��� �޾ƿ;���) ���ؼ� �̵�
                 */
                isAttach = false;
                isHookActive = false;
                isDash = false;
                isLineMax = false;
                GrabHook.GetComponent<Hook>().joint2D.enabled = false;
                GrabHook.SetActive(false);

                if (isDirection && !isAttach && playerController.rigid.velocity.y >= 0) //���⿡ ���� �ӵ� ���ϱ�...���� ���ǽ� ���� �̻��� 
                {
                    playerController.rigid.AddForce(Vector2.right * 500, ForceMode2D.Force);
                    Debug.Log("11111" + Vector2.right);
                }
                else if (!isDirection && !isAttach && playerController.rigid.velocity.y >= 0)
                {
                    playerController.rigid.AddForce(Vector2.left * 500, ForceMode2D.Force);
                    Debug.Log("22222" + Vector2.left);
                }
          
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && isAttach)
        {
            StartCoroutine(Dash_Co());



        }
        else if(!isAttach)
        {
            ghost.makeGhost = false; //�ܻ�off... �׳� �������� �����ұ�...
        }
    }

    private IEnumerator Dash_Co()
    {
        isDash = true;
        ghost.makeGhost = true; //�ܻ� on 
        if (isDirection) //������ȯ
        {
            playerController.rigid.velocity = new Vector2(1 * dashSpeed, playerController.rigid.velocity.y);
        }
        else
        {
            playerController.rigid.velocity = new Vector2(-1 * dashSpeed, playerController.rigid.velocity.y);
        }


        //if (dashTime <= 0)
        //{
        //    if (isDash)
        //    {
        //        //dashtime�� 0���� ���� �� ����Ʈ�� ������ dashtime�� defaulttime���� �ʱ�ȭ...��?
        //        dashTime = defaultTime;
        //    }
        //}
        //else
        //{
        //    //�� �ܿ��� dashtime�� �������� delftatime��ŭ ���ֱ�....��...? �������� �� ������ �����ּ���..?
        //    dashTime -= Time.deltaTime;
        //    speed = dashSpeed;
        //} 
        //����?? ��� �ƹ��� ������ ���ݾ�?? �𸣰����ϱ� ���� �׳�

        yield return null;

      

    }



}
