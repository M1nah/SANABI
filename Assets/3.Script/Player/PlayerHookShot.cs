using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookShot : MonoBehaviour //hookshot && dash
{
    [SerializeField] GameObject GrabHook;
    public Transform hook;
    public LineRenderer line;
    public LineRenderer guideline;

    Vector2 mouseDirection; //마우스 커서 위치

    public bool isHookActive; //마우스 버튼이 눌렸을 때 참으로 바꿔주는 변수
    public bool isLineMax;
    public bool isAttach = false; // 이 변수가 참일 때 platform에 붙는다 linemax가 발동 안함 


    //dash
    PlayerController playerController;
    PlayerInput playerInput;

    //public float speed; //<=> moveSpeed 변수 대체(기존 player speed) 
    public float dashSpeed;
    public float defaultTime;//기본 시간
    [SerializeField]float dashTime; //dash 시간

    public bool isDirection = false;

    public bool isDash = false; //dash 상태

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
        //↑라인렌더러가 추가되어있는 오브젝트의 위치와 상관없이
        //월드 좌표를 기준으로 화면에 라인이 그려지게 됨
    }

    private void Update()
    {
        //hook와 player의 위치가 바뀌면 포지션이 변경되게
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(0) && !isHookActive)
        {
            //hoook는 player의 위치에서 날아가야하니까
            //마우스 버튼을 눌렀을 때 hook의 위치를 player의 위치로 초기화 시켜준다
            hook.position = transform.position;

            //마우스 포지션은 스크린 기준으로 값을 반환하니까 월드 좌표로 바꿔준 뒤
            //player의 위치값을 빼주면 고리가 날아가는 방향의 백터값을 알수 있다
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            isHookActive = true;
            isLineMax = false;
            GrabHook.SetActive(true);
            Debug.Log("Hook 발사");
        }

        if (isHookActive && !isLineMax && !isAttach) //isHookActive가 참이고 lineMax가 거짓일 때만 후크가 날아가게끔 하기
        {
            hook.Translate(mouseDirection.normalized * Time.deltaTime * 12);


            if (Vector2.Distance(transform.position, hook.position) > 2) //Distance(a,b)=> a에서 b까지의 거리구하기함수
            {
                //hook와 player의 거리가 5보다 클 때 lineMax는 참이다
                isLineMax = true;
            }
        }
        else if (isHookActive && isLineMax && !isAttach)
        { //마우스 버튼을 떼고 isHookActive가 참이고 line이 최고지점에 도달했을 때(isLineMax가 참일때) 후크가 돌아옴
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 12);

            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            { //hook와 player의 간격이 0.1보다 작아지면 isHookActive와 isLineMax를 비활성화 => 고리가 움직이지 않게 한다 
                isHookActive = false;
                isLineMax = false;
                GrabHook.SetActive(false);
                Debug.Log("hook 회수");
            }
        }
        else if (isAttach) //platform에서 후크가 떨어질 때 모든 bool 변수가 비활성화
        {

            if (Input.GetMouseButtonUp(0))
            {
                /*
                 * <dash>
                 1. hook를 놓았을때 (platform에 매달려있지 않을때)
                 2. Dash를 썼을 때
                 3. player.rigid.velocity.y !=0떄
                4.  ground에 닿지 않았을 때 

                => Horizontal  입력값을 받고 거기에 x축*어떤 속도?(이건 어디서 받아와야함) 곱해서 이동
                 */
                isAttach = false;
                isHookActive = false;
                isDash = false;
                isLineMax = false;
                GrabHook.GetComponent<Hook>().joint2D.enabled = false;
                GrabHook.SetActive(false);

                if (isDirection && !isAttach && playerController.rigid.velocity.y >= 0) //방향에 따라 속도 곱하기...여기 조건식 뭔가 이상함 
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
            ghost.makeGhost = false; //잔상off... 그냥 이정도로 만족할까...
        }
    }

    private IEnumerator Dash_Co()
    {
        isDash = true;
        ghost.makeGhost = true; //잔상 on 
        if (isDirection) //방향전환
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
        //        //dashtime이 0보다 작을 때 쉬프트가 눌리면 dashtime을 defaulttime으로 초기화...왜?
        //        dashTime = defaultTime;
        //    }
        //}
        //else
        //{
        //    //그 외에는 dashtime을 매프레임 delftatime만큼 빼주기....왜...? 곰돌선생 왜 설명을 안해주세요..?
        //    dashTime -= Time.deltaTime;
        //    speed = dashSpeed;
        //} 
        //뭐임?? 없어도 아무런 지장이 없잖아?? 모르겠으니까 빼자 그냥

        yield return null;

      

    }



}
