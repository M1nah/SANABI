using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isInteraction { get; private set; }
    public bool isJump { get; private set; }

    // Update is called once per frame
    void Update()
    {

        isInteraction = Input.GetKeyDown(KeyCode.E);
        isJump = Input.GetKeyDown(KeyCode.Space);

    }
}
