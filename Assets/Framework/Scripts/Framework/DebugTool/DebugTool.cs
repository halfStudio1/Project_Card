using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTool : MonoBehaviour
{
    private void Awake()
    {
#if OPEN_LOG
        Debugger.InitLog(new LogConfig
        {
            openLog = true,
            openTime = true,
            showThreadID = true,
            showColorName = true,
            logSave = true,
            showFPS = true,
        });
        //Debugger.Log("Log");
        //Debugger.LogWarning("LogWarning");
        //Debugger.LogError("LogError");
        //Debugger.ColorLog(LogColor.Yellow, "ColorLog");
        //Debugger.LogGreen("LogGreen");
        //Debugger.LogPink("可爱小男娘");
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }
}
