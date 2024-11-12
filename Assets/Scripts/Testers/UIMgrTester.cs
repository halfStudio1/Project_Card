using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgrTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //UIMgr.Instance.ShowPanel<TestUIPanel>();

            UIMgr.Instance.ShowPanel<TestUIPanel>(E_CanvasType.Bottom, OnShow);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UIMgr.Instance.HidePanel<TestUIPanel>(true);
        }
    }

    private void OnShow(TestUIPanel panel)
    {
        Debug.Log(panel.name);
    }
}
