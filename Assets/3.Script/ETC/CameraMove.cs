using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject virtualCam;





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }


    //줌인

    private void ZoomIn()
    {
        //player가 죽었을 때
        // etc. 특정 카메라 포인트로 들어왓을 ㄸㅐ
        //줌사이즈 ==> 1.8에서 1로
    }

    private void ZoomOUt()
    {
        //01씬이 처음 시작할 때 Intro 
    }


}
