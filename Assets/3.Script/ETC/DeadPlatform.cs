using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlatform : MonoBehaviour
{
    [SerializeField] private AudioSource deadZoneAudio;
    [SerializeField] private AudioClip deadZoneSFX;

    //¹âÀ¸¸é Ä«¸Þ¶ó Èçµé±â

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("Level") != 0)
        {
            StartCoroutine(DeadZoneAudio());
            GameManager.instance.HP();
        }
    }

    private IEnumerator DeadZoneAudio()
    {
        deadZoneAudio.PlayOneShot(deadZoneSFX);
        yield return new WaitForSeconds(1f);
    }
}
