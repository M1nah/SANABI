using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    Rigidbody2D rgd;

    [SerializeField]float maxSpeed;

    bool isUp=false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GrabPlatform"))
        {
            isUp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("GrabPlatform"))
        {
            isUp = false;
        }
    }

    private void FixedUpdate()
    {
        GrabHill();
    }


    private void GrabHill()
    {

        if (isUp)
        {
            float ver = Input.GetAxisRaw("Vertical");
            rgd.gravityScale = 0;
            rgd.velocity = new Vector2(rgd.velocity.x, ver * maxSpeed);
        }
        else
        {
            rgd.gravityScale = 0;
        }

    }

}
