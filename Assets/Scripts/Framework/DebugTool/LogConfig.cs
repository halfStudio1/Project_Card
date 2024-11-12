using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogConfig
{
    /// <summary>
    /// 日志是否打开
    /// </summary>
    public bool openLog = true;
    /// <summary>
    /// 日志前缀
    /// </summary>
    public string logHeadFix = "###";
    /// <summary>
    /// 是否显示时间
    /// </summary>
    public bool openTime = true;
    /// <summary>
    /// 显示线程ID
    /// </summary>
    public bool showThreadID = true;
    /// <summary>
    /// 是否存储日志文件
    /// </summary>
    public bool logSave = true;
    /// <summary>
    /// 是否显示FPS
    /// </summary>
    public bool showFPS = true;
    /// <summary>
    /// 显示颜色名称
    /// </summary>
    public bool showColorName = true;
    /// <summary>
    /// 文件存储路径
    /// </summary>
    public string logFileSavePath { get { return Application.persistentDataPath + "/"; } }
    /// <summary>
    /// 日志文件名称
    /// </summary>
    public string logFileName { get { return Application.productName + " " + DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".log"; } }

}
