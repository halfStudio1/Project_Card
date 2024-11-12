using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private float _deltaTime = 0.0f;

    private GUIStyle _style;

    private void Awake()
    {
        _style = new GUIStyle();
        _style.alignment = TextAnchor.UpperLeft;
        _style.normal.background = null;
        _style.fontSize = 35;
        _style.normal.textColor = Color.red;
    }
    private void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
    }
    private void OnGUI()
    {
        Rect rect = new Rect(0, 0, 500, 300);
        float fps = 1.0f / _deltaTime;
        string text = string.Format(" FPS:{0:N0} ", fps);
        GUI.Label(rect, text, _style);
    }

}
