using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isInteraction { get; private set; }
    
    public bool isMoveLeft { get; private set; }
    public bool isMoveRight { get; private set; }
    public bool isMoveUp { get; private set; }
    public bool isMoveDown { get; private set; }
    public bool isJump { get; private set; }

    // Update is called once per frame
    void Update()
    {
        isInteraction = Input.GetKeyDown(KeyCode.E);

        isMoveLeft = Input.GetKey(KeyCode.A);
        isMoveRight = Input.GetKey(KeyCode.D);
        isMoveUp = Input.GetKey(KeyCode.W);
        isMoveDown = Input.GetKey(KeyCode.S);
        isJump = Input.GetKeyDown(KeyCode.Space);

    }
}
