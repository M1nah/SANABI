using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlatform : MonoBehaviour
{
    [SerializeField] private AudioSource deadZoneAudio;
    [SerializeField] private AudioClip deadZoneSFX;

    public GameObject virtualPlayerFollowCam;
    Vector3 cameraPos;

    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange =0.05f;
    [SerializeField] [Range(0.1f, 1f)] float duration =0.05f;

    //¹âÀ¸¸é Ä«¸Þ¶ó Èçµé±â

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("Level") != 0)
        {
            StartCoroutine(DeadZoneAudio_Co());
            //StartCoroutine(DeadShakeCam_Co());
            GameManager.instance.HP();
        }
    }

    private IEnumerator DeadZoneAudio_Co()
    {
        deadZoneAudio.PlayOneShot(deadZoneSFX);
        yield return new WaitForSeconds(1f);
    }


    //´êÀ¸¸é Ä«¸Þ¶ó°¡ Èçµé¸².... 
    public void Shake()
    {
        cameraPos = virtualPlayerFollowCam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
        Debug.Log("Shake");
    }

    public void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;

        Vector3 cameraPos = virtualPlayerFollowCam.transform.position;

        cameraPos.x = cameraPosX;
        cameraPos.y = cameraPosY;
        virtualPlayerFollowCam.transform.position = cameraPos;
        Debug.Log("StartShake");
    }

    public void StopShake()
    {
        CancelInvoke("StartShake");
        virtualPlayerFollowCam.transform.position = cameraPos;
        Debug.Log("StopShake");
    }
}
