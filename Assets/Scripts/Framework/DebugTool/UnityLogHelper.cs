using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using UnityEngine;

public class LogData
{
    public string log;
    public string trace;
    public LogType type;
}
public class UnityLogHelper : MonoBehaviour
{
    /// <summary>
    /// 文件写入流
    /// </summary>
    private StreamWriter _streamWriter;
    /// <summary>
    /// 日志数据队列
    /// </summary>
    private readonly ConcurrentQueue<LogData> _conCurrentQueue = new ConcurrentQueue<LogData>();
    /// <summary>
    /// 工作信号事件
    /// </summary>
    private readonly ManualResetEvent _manualRestEvent = new ManualResetEvent(false);
    private bool _threadRunning = true;
    /// <summary>
    /// 当前时间
    /// </summary>
    private string _nowTime { get { return DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss"); } }

    public void InitLogFileModule(string savePath, string logfileName)
    {
        string logFilePath = Path.Combine(savePath, logfileName);
        Debug.Log("logFilePath:" + logFilePath);
        _streamWriter = new StreamWriter(logFilePath);
        Application.logMessageReceivedThreaded += OnLogMessageReceivedThreaded;

        Thread fileThread = new Thread(FileLogThread);
        fileThread.Start();
    }

    public void FileLogThread()
    {
        while (_threadRunning)
        {
            _manualRestEvent.WaitOne();
            if(_streamWriter == null)
            {
                break;
            }
            LogData data;
            while(_conCurrentQueue.Count > 0 && _conCurrentQueue.TryDequeue(out data))
            {
                if(data.type == LogType.Log)
                {
                    _streamWriter.Write("Log >>> ");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.WriteLine(data.trace);
                }
                else if(data.type == LogType.Warning)
                {
                    _streamWriter.Write("Warning >>> ");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.WriteLine(data.trace);
                }
                else if(data.type == LogType.Error)
                {
                    _streamWriter.Write("Error >>> ");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.Write('\n');
                    _streamWriter.WriteLine(data.trace);
                }
                _streamWriter.Write("\r\n");
            }
            //保存文件内容，使其生效
            _streamWriter.Flush();
            _manualRestEvent.Reset();
            Thread.Sleep(1);
        }
    }

    public void OnApplicationQuit()
    {
        Application.logMessageReceivedThreaded -= OnLogMessageReceivedThreaded;
        _threadRunning = false;
        _manualRestEvent.Reset();
        _streamWriter.Close();
        _streamWriter = null;
    }
    private void OnLogMessageReceivedThreaded(string condition, string stackTrace, LogType type)
    {
        _conCurrentQueue.Enqueue(new LogData { log = _nowTime + " " + condition, trace = stackTrace, type = type });
        _manualRestEvent.Set();
    }
}
