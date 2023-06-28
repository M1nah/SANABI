using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] GameObject GrabHook;
    public LineRenderer line; //������ �׸��� ���� ���η�����
    public Transform grabhook; //��ũ ��ġ

    Vector2 mouseDirection; //���콺 Ŀ���� ��ġ 

    private bool isHookActive;
    private bool isLineMax;
    public bool isAttach = false; //�� ������ ���̸� GrabPlatform�� ���� islinemax �Լ��� �۵� x


    private void Start()
    {
        line.positionCount = 2; // ������ �׸��� ������ (�� ���� player�� ������, �� ���� grabhook�� ������)
        line.endWidth = line.startWidth = 0.04f;
        line.SetPosition(0, transform.position); //player�� ������
        line.SetPosition(1, grabhook.position); //grabhook�� ������
        line.useWorldSpace = true; //���η������� �߰��Ǿ��ִ� ������Ʈ�� ��ġ�� ������� ���� ��ǥ�� �������� ȭ�鿡 ������ �׷����� ��
    }

    private void Update()
    {
        //player�� grabhook�� ��ġ�� �ٲ�� ������ �����ǰ��� �ٲ�� update�Լ��� �־��ش�
        line.SetPosition(0, transform.position); //player�� ������
        line.SetPosition(1, grabhook.position); //grabhook�� ������

       //grabhoook �߻��ϱ�
       if (Input.GetMouseButtonDown(0)&& !isHookActive) //���콺 ��Ŭ���� ��ũ �߻� 
       {
           //grabhook�� �÷��̾��� ��ġ���� �߻�Ǿ�� �ϴϱ� grabhook�� �ʱ� ��ġ���� �÷��̾��� ��ġ������ ����
           grabhook.position = transform.position;
       
           //���콺 �������� ��ũ�� �������� ���� ��ȯ�ϴϱ� ���� ��ǥ�� �ٲ��� ��
           //player�� ��ġ���� ���ָ� ���� ���ư��� ������ ���Ͱ��� �˼� �ִ�
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

            ///////youtu.be/jBw3wUDvQ8Y?list=LL&t=210 �����Ѱ�...���Ҹ��� �ٽ� ����///////
            if (Vector2.Distance(transform.position, grabhook.position) > 7)
            {
                isLineMax = true;
            }

            //���鿩�⼭���� �۵��� ���ϳ��ס���//
            else if (isHookActive && isLineMax && !isAttach)
            { 
                grabhook.position = Vector2.MoveTowards(grabhook.position, transform.position, Time.deltaTime * 5);

                //��ũ�� player�� ������ 0.1���� �۾����� ��ũ ��Ȱ��ȭ
                if (Vector2.Distance(transform.position, grabhook.position) < 0.1f)
                {
                    isHookActive = false;
                    isLineMax = false;
                    GrabHook.SetActive(false);
                    Debug.Log("��ũ �������? �۵��� �ƿ� ���ϳ�.. ");
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
                    Debug.Log("��ũ ��Ȱ��ȭ �ʴ� �ϴ�?");
                }
            }
        }

    }

}
