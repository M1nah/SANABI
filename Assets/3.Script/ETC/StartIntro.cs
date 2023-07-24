using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIntro : MonoBehaviour
{

    [SerializeField] Animator introAni;

    [SerializeField] GameObject player;
    [SerializeField] GameObject playerArm;
    [SerializeField] GameObject HPUI;

    [SerializeField] GameObject StartCam;
    [SerializeField] GameObject FollowCam;

    //die audio
    [SerializeField] AudioSource standUp;
    [SerializeField] AudioSource armEquipping;

    private void Start()
    {
        StartCoroutine(StartIntro_Co());
    }
    public IEnumerator StartIntro_Co()
    {
        introAni.SetBool("isStart", true);
        StartCoroutine(StartIntroAudio_Co());
        yield return new WaitForSeconds(3f);
        StartCam.SetActive(false);
        FollowCam.SetActive(true);
        yield return new WaitForSeconds(3.7f);
        introAni.SetBool("isStart", false);
        playerArm.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        HPUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<StartIntro>().enabled = false;
    }

    public IEnumerator StartIntroAudio_Co()
    {
        yield return new WaitForSeconds(3.1f);
        standUp.Play();
        yield return new WaitForSeconds(2f);
        armEquipping.Play();
    }

}
