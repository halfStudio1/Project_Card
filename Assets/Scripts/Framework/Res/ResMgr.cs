using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

//可寻址资源 信息
public class AddressablesInfo
{
    //记录 异步操作句柄
    public AsyncOperationHandle handle;
    //记录 引用计数
    public uint count;

    public AddressablesInfo(AsyncOperationHandle handle)
    {
        this.handle = handle;
        count += 1;
    }
}

public class ResMgr : Singleton<ResMgr>
{

    //有一个容器 帮助我们存储 异步加载的返回值
    public Dictionary<string, AddressablesInfo> resDic = new Dictionary<string, AddressablesInfo>();

    //异步加载资源的方法
    public void LoadAssetAsync<T>(string name, Action<T> callBack)
    {
        //由于存在同名 不同类型资源的区分加载
        //所以我们通过名字和类型拼接作为 key
        string keyName = name + "_" + typeof(T).Name;
        AsyncOperationHandle<T> handle;
        //如果已经加载过该资源
        if (resDic.ContainsKey(keyName))
        {
            //获取异步加载返回的操作内容
            handle = resDic[keyName].handle.Convert<T>();
            //要使用资源了 那么引用计数+1
            resDic[keyName].count += 1;
            //判断 这个异步加载是否结束
            if (handle.IsDone)
            {
                //如果成功 就不需要异步了 直接相当于同步调用了 这个委托函数 传入对应的返回值
                callBack(handle.Result);
            }
            //还没有加载完成
            else
            {
                //如果这个时候 还没有异步加载完成 那么我们只需要 告诉它 完成时做什么就行了
                handle.Completed += (obj) =>
                {
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                        callBack(obj.Result);
                };
            }
            return;
        }

        //如果没有加载过该资源
        //直接进行异步加载 并且记录
        handle = Addressables.LoadAssetAsync<T>(name);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
                callBack(obj.Result);
            else
            {
                Debug.LogWarning(keyName + "资源加载失败");
                if (resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        };
        AddressablesInfo info = new AddressablesInfo(handle);
        resDic.Add(keyName, info);
    }



    //释放资源的方法 
    public void Release<T>(string name)
    {
        //由于存在同名 不同类型资源的区分加载
        //所以我们通过名字和类型拼接作为 key
        string keyName = name + "_" + typeof(T).Name;
        if (resDic.ContainsKey(keyName))
        {
            //释放时 引用计数-1
            resDic[keyName].count -= 1;
            //如果引用计数为0  才真正的释放
            if (resDic[keyName].count == 0)
            {
                //取出对象 移除资源 并且从字典里面移除
                AsyncOperationHandle<T> handle = resDic[keyName].handle.Convert<T>();
                Addressables.Release(handle);
                resDic.Remove(keyName);
            }
        }
    }

    #region 处理多个资源
    public void LoadAssetAsync<T>(Addressables.MergeMode mode, Action<T> callBack, params string[] keys)
    {
        //1.构建一个keyName  之后用于存入到字典中
        List<string> list = new List<string>(keys);
        string keyName = "";
        foreach (string key in list)
            keyName += key + "_";
        keyName += typeof(T).Name;
        //2.判断是否存在已经加载过的内容 
        //存在做什么
        AsyncOperationHandle<IList<T>> handle;
        if (resDic.ContainsKey(keyName))
        {
            handle = resDic[keyName].handle.Convert<IList<T>>();
            //要使用资源了 那么引用计数+1
            resDic[keyName].count += 1;
            //异步加载是否结束
            if (handle.IsDone)
            {
                foreach (T item in handle.Result)
                    callBack(item);
            }
            else
            {
                handle.Completed += (obj) =>
                {
                    //加载成功才调用外部传入的委托函数
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                    {
                        foreach (T item in handle.Result)
                            callBack(item);
                    }
                };
            }
            return;
        }
        //不存在做什么
        handle = Addressables.LoadAssetsAsync<T>(list, callBack, mode);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("资源加载失败" + keyName);
                if (resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        };
        AddressablesInfo info = new AddressablesInfo(handle);
        resDic.Add(keyName, info);
    }

    //卸载多个资源
    public void Release<T>(params string[] keys)
    {
        //1.构建一个keyName  之后用于存入到字典中
        List<string> list = new List<string>(keys);
        string keyName = "";
        foreach (string key in list)
            keyName += key + "_";
        keyName += typeof(T).Name;

        if (resDic.ContainsKey(keyName))
        {
            resDic[keyName].count -= 1;
            if (resDic[keyName].count == 0)
            {
                //取出字典里面的对象
                AsyncOperationHandle<IList<T>> handle = resDic[keyName].handle.Convert<IList<T>>();
                Addressables.Release(handle);
                resDic.Remove(keyName);
            }

        }
    }

    #endregion

    //异步加载多个资源 或者 加载指定资源

    #region Scene


    private Dictionary<string, AsyncOperationHandle<SceneInstance>> _cachedScenes = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();
    // 加载场景
    public async void LoadSceneAsync(string sceneKey, bool additive = true)
    {
        await ReallyLoadSceneAsync(sceneKey, additive);
    }

    private async Task<bool> ReallyLoadSceneAsync(string sceneKey, bool additive = true)
    {

        if (_cachedScenes.ContainsKey(sceneKey))
        {
            Debugger.LogWarning($"已经加载了场景了：{sceneKey}");
            return false;
        }
        else
        {
            var handle = Addressables.LoadSceneAsync(sceneKey, additive ? UnityEngine.SceneManagement.LoadSceneMode.Additive : UnityEngine.SceneManagement.LoadSceneMode.Single);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _cachedScenes.Add(sceneKey, handle);
                return true;
            }
            else
            {
                Debugger.LogError($"加载场景失败: {sceneKey}");
                return false;
            }
        }
    }

    // 卸载场景
    public async void UnloadSceneAsync(string sceneKey)
    {
        await ReallyUnloadSceneAsync(sceneKey);
    }
    private async Task<bool> ReallyUnloadSceneAsync(string sceneKey)
    {
        // 检查场景是否已加载
        if (_cachedScenes.TryGetValue(sceneKey, out AsyncOperationHandle<SceneInstance> handle))
        {
            var unloadHandle = Addressables.UnloadSceneAsync(handle, false);
            await unloadHandle.Task;

            if (unloadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _cachedScenes.Remove(sceneKey); // 从缓存中移除场景
                unloadHandle.Release();
                return true;
            }
            else
            {
                Debugger.LogError($"卸载场景失败: {sceneKey}");
                return false;
            }
        }
        else
        {
            Debugger.LogWarning($"场景 {sceneKey} 没有被加载.");
            return false;
        }
    }

    #endregion

    //清空资源
    public void Clear()
    {
        foreach (var item in resDic.Values)
        {
            Addressables.Release(item.handle);
        }
        resDic.Clear();
        AssetBundle.UnloadAllAssetBundles(true);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
