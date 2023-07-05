using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookShot : MonoBehaviour
{
    [SerializeField] GameObject GrabHook;
    public Transform hook;
    public LineRenderer line;

    Vector2 mouseDirection; //마우스 커서 위치

    public bool isHookActive; //마우스 버튼이 눌렸을 때 참으로 바꿔주는 변수
    public bool isLineMax;
    public bool isAttach; // 이 변수가 참일 때 platform에 붙는다 linemax가 발동 안함 

    private void Start()
    {
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.02f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        line.useWorldSpace = true;
        //↑라인렌더러가 추가되어있는 오브젝트의 위치와 상관없이
        //월드 좌표를 기준으로 화면에 라인이 그려지게 됨

        isAttach = false; 
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
            hook.Translate(mouseDirection.normalized * Time.deltaTime * 8);

            if (Vector2.Distance(transform.position, hook.position) > 2) //Distance(a,b)=> a에서 b까지의 거리구하기함수
            {
                //hook와 player의 거리가 5보다 클 때 lineMax는 참이다
                isLineMax = true;
            }
        }
        else if (isHookActive && isLineMax && !isAttach)
        { //마우스 버튼을 떼고 isHookActive가 참이고 line이 최고지점에 도달했을 때(isLineMax가 참일때) 후크가 돌아옴
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 8);

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
                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                GrabHook.GetComponent<Hook>().joint2D.enabled = false;
                GrabHook.SetActive(false);
            }
        }
    }

}
