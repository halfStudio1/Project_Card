using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Camera mainCamera;
    private void Awake()
    {
        CameraMgr.Instance.AddCamera(mainCamera);
    }
}
