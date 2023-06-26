using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPlatform : MonoBehaviour
{
    PlayerGrab grab;
    public DistanceJoint2D joint2D;

    private void Start()
    {
        grab = GameObject.Find("Player").GetComponent<PlayerGrab>();
        joint2D = GetComponent<DistanceJoint2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPlatform"))
        {
            joint2D.enabled = true;
            grab.isAttach = true;
        }
        
    }
}
