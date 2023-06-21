using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string MoveAxisName = "Vertical";
    [SerializeField] private string RotateAxisName = "Horizontal";
    public float Move_Value { get; private set; }
    public float Rotate_Value { get; private set; }
    public bool isInteraction { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
     
        

    }

    // Update is called once per frame
    void Update()
    {
        Move_Value = Input.GetAxis(MoveAxisName);
        Rotate_Value = Input.GetAxis(RotateAxisName);

        isInteraction = Input.GetKey(KeyCode.E);
    }
}
