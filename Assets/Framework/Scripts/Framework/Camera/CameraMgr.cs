using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraMgr : SingletonMono<CameraMgr>
{
    public UniversalAdditionalCameraData _uacd;
    public void AddCamera(Camera camera)
    {
        _uacd.cameraStack.Add(camera);
    }
}
