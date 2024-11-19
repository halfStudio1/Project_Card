using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private bool isDraw;
    private void Update()
    {
        if (isDraw)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }
        else 
        {
            if (transform.position.x < 2 && transform.position.x > -2) 
            {
                transform.position = new Vector2(0, 0);
            }
        }
    }
    private void OnMouseDown()
    {
        isDraw = !isDraw;
    }
}
