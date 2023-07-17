using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    //[HideInInspector] public Texture2D cursorPick;
    public Texture2D cursorArrow;

    public Vector2 cursorHotSpot = Vector2.zero;

    //public CursorMode cursorMode = CursorMode.Auto;


    // Start is called before the first frame update
    void Start()
    {
        cursorHotSpot = new Vector2(cursorArrow.width, cursorArrow.height);

        Cursor.SetCursor(cursorArrow, cursorHotSpot, CursorMode.Auto);
    }
}
