using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D click;
    
    void Start()
    {
        UnityEngine.Cursor.SetCursor(cursor,Vector2.zero, CursorMode.ForceSoftware);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Cursor.SetCursor(click,Vector2.zero, CursorMode.ForceSoftware);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UnityEngine.Cursor.SetCursor(cursor,Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
