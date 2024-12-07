using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// 游戏中的所有场景
/// </summary>
public enum E_ScenesType
{
    GameEntry,
    TestScene_1,
}

public class SceneMgr : Singleton<SceneMgr>
{
    public string curScene = string.Empty;

    private Dictionary<string, AsyncOperationHandle<SceneInstance>> _cachedScenes = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();

    /// <summary>
    /// 卸载当前场景后加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="action"></param>
    public async void LoadSceneWhenUnloadCurScene(string sceneName, UnityAction action = null)
    {
        if (!string.IsNullOrEmpty(curScene))
            await ReallyUnloadSceneAsync(curScene);
        await ReallyLoadSceneAsync(sceneName, action, true);
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="additive"></param>
    public async void LoadSceneAsync(string sceneName, UnityAction action = null, bool additive = true)
    {
        await ReallyLoadSceneAsync(sceneName, action, additive);
    }
    private async UniTask<bool> ReallyLoadSceneAsync(string sceneName, UnityAction action = null, bool additive = true)
    {
        if (_cachedScenes.ContainsKey(sceneName))
        {
            Debugger.LogWarning($"已经加载了场景了：{sceneName}");
            return false;
        }
        else
        {
            var handle = Addressables.LoadSceneAsync(sceneName, additive ? UnityEngine.SceneManagement.LoadSceneMode.Additive : UnityEngine.SceneManagement.LoadSceneMode.Single);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _cachedScenes.Add(sceneName, handle);
                curScene = sceneName;
                action?.Invoke();
                Debugger.LogError($"加载场景成功: {sceneName}");
                return true;
            }
            else
            {
                Debugger.LogError($"加载场景失败: {sceneName}");
                return false;
            }
        }
    }

    /// <summary>
    /// 卸载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public async void UnloadSceneAsync(string sceneName, UnityAction action = null)
    {
        await ReallyUnloadSceneAsync(sceneName, action);
    }
    private async UniTask<bool> ReallyUnloadSceneAsync(string sceneName, UnityAction action = null)
    {
        // 检查场景是否已加载
        if (_cachedScenes.TryGetValue(sceneName, out AsyncOperationHandle<SceneInstance> handle))
        {
            var unloadHandle = Addressables.UnloadSceneAsync(handle, false);
            await unloadHandle.Task;

            if (unloadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _cachedScenes.Remove(sceneName); // 从缓存中移除场景
                unloadHandle.Release();
                curScene = string.Empty;
                action?.Invoke();
                Debugger.LogError($"卸载场景成功: {sceneName}");
                return true;
            }
            else
            {
                Debugger.LogError($"卸载场景失败: {sceneName}");
                return false;
            }
        }
        else
        {
            Debugger.LogWarning($"场景 {sceneName} 没有被加载.");
            return false;
        }
    }
}
