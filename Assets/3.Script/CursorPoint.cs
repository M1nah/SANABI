using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    public Texture2D cursorArrow;
    public Texture2D cursorPick;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        PauseCursor();
    }

    private void PauseCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorPick, Vector2.zero, CursorMode.ForceSoftware);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    //private void OnMouseEnter(Collider2D col)
    //{
    //    if (col.CompareTag("Button"))
    //    {
    //        Cursor.SetCursor(cursorPick, Vector2.zero, CursorMode.ForceSoftware);
    //    }
    //}
    //private void OnMouseExit(Collider2D col)
    //{
    //    if (col.CompareTag("Button"))
    //    {
    //        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    //    }
    //}
}
