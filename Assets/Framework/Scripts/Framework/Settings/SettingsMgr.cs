using UnityEngine;

public enum E_FrameRateType
{
    Low = 30,
    Middle = 60,
    High = 90
}
public class SettingsMgr : Singleton<SettingsMgr>
{
    /// <summary>
    /// 设置帧率
    /// </summary>
    /// <param name="frameRate"></param>
    public void SetFrameRate(E_FrameRateType frameRate)
    {
        Application.targetFrameRate = (int)frameRate;
    }
    public int GetFrameRate()
    {
        return Application.targetFrameRate;
    }

    /// <summary>
    /// 设置分辨率
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="isFullscreen"></param>
    public void SetResolution(int width, int height, bool isFullscreen = false)
    {
        Screen.SetResolution(width, height, isFullscreen);
    }

    /// <summary>
    /// 设置垂直同步
    /// </summary>
    /// <param name="enableVSync"></param>
    public void SetVSync(bool enableVSync)
    {
        if (enableVSync)
        {
            // 开启垂直同步
            QualitySettings.vSyncCount = 1; // 每个垂直刷新间隔同步
        }
        else
        {
            // 关闭垂直同步
            QualitySettings.vSyncCount = 0; // 不进行垂直同步
        }
    }
}
