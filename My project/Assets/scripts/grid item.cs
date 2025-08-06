using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class griditem : MonoBehaviour
{
   /* private int _x;

     public int x
    {         get { return _x; }
        
    } */

    public int x
    {
        get;
        private set;
    }
    public int y
    {
        get;
        private set;
    }
    public void OnItemPositionChanged (int newX, int newY)
    {
        x = newX;
        y = newY;
        gameObject.name = string.Format ("sprite ({0},{1})", x, y);
    }

    void OnMouseDown()
    {
        if (OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }

    public delegate void OnMouseOverItem (griditem item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
