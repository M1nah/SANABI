using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    public Texture2D cursorArrow;
    public Vector2 cursorHotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        cursorHotSpot = new Vector2(cursorArrow.width * 0.5f, cursorArrow.height * 0.5f);

        Cursor.SetCursor(cursorArrow, cursorHotSpot, CursorMode.ForceSoftware);

    }
}
