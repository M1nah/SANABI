using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] PlayerHookShot hookShot;
    [SerializeField] public DistanceJoint2D joint2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform"))
        {
            joint2D.enabled = true;
            hookShot.isAttach = true;
        }
    }


}
