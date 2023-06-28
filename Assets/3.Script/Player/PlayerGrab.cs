using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] GameObject GrabHook;
    public LineRenderer line; //라인을 그리기 위한 라인렌더러
    public Transform grabhook; //후크 위치

    Vector2 mouseDirection; //마우스 커서의 위치 

    private bool isHookActive;
    private bool isLineMax;
    public bool isAttach = false; //이 변수가 참이면 GrabPlatform에 붙음 islinemax 함수가 작동 x


    private void Start()
    {
        line.positionCount = 2; // 라인을 그리는 포지션 (한 점은 player의 포지션, 한 점은 grabhook의 포지션)
        line.endWidth = line.startWidth = 0.04f;
        line.SetPosition(0, transform.position); //player의 포지션
        line.SetPosition(1, grabhook.position); //grabhook의 포지션
        line.useWorldSpace = true; //라인렌더러가 추가되어있는 오브젝트의 위치와 상관없이 월드 좌표를 기준으로 화면에 라인이 그려지게 됨
    }

    private void Update()
    {
        //player와 grabhook의 위치가 바뀌면 라인의 포지션값이 바뀌게 update함수에 넣어준다
        line.SetPosition(0, transform.position); //player의 포지션
        line.SetPosition(1, grabhook.position); //grabhook의 포지션

       //grabhoook 발사하기
       if (Input.GetMouseButtonDown(0)&& !isHookActive) //마우스 좌클릭시 후크 발사 
       {
           //grabhook은 플레이어의 위치에서 발사되어야 하니까 grabhook의 초기 위치값은 플레이어의 위치값으로 설정
           grabhook.position = transform.position;
       
           //마우스 포지션은 스크린 기준으로 값을 반환하니까 월드 좌표로 바꿔준 뒤
           //player의 위치값을 빼주면 고리가 날아가는 방향의 백터값을 알수 있다
           mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
           isHookActive = true;
           isLineMax = false;
           GrabHook.SetActive(true); 
       }
       
        else if (Input.GetMouseButtonUp(0) && isHookActive)
       {
           isHookActive = false;
           GrabHook.SetActive(false);
       }

        if (isHookActive && !isLineMax && !isAttach) 
        {
            grabhook.Translate(mouseDirection.normalized * Time.deltaTime * 45);

            ///////youtu.be/jBw3wUDvQ8Y?list=LL&t=210 참고한거...뭔소리여 다시 보기///////
            if (Vector2.Distance(transform.position, grabhook.position) > 7)
            {
                isLineMax = true;
            }

            //↓↓↓여기서부터 작동을 안하네잉↓↓↓//
            else if (isHookActive && isLineMax && !isAttach)
            { 
                grabhook.position = Vector2.MoveTowards(grabhook.position, transform.position, Time.deltaTime * 5);

                //후크와 player의 간격이 0.1보다 작아지면 후크 비활성화
                if (Vector2.Distance(transform.position, grabhook.position) < 0.1f)
                {
                    isHookActive = false;
                    isLineMax = false;
                    GrabHook.SetActive(false);
                    Debug.Log("후크 사라지니? 작동을 아예 안하네.. ");
                }
            }
            else if (isAttach)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isAttach = false;
                    isHookActive = false;
                    isLineMax = false;
                    grabhook.GetComponent<GrabPlatform>().joint2D.enabled = false;
                    GrabHook.SetActive(false);
                    Debug.Log("후크 비활성화 너는 하니?");
                }
            }
        }

    }

}
