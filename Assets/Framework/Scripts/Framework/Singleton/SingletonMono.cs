using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 自动挂载式的 继承Mono的单例模式基类
/// 无需手动挂载 无需动态添加 无需关心切场景带来的问题
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _isApplicationQuitting = false;

    public static T Instance
    {
        get
        {
            if (_isApplicationQuitting)
            {
                //Debug.LogWarning($"[单例模式] 实例 '{typeof(T)}' 已被销毁，返回空对象。");
                return null;
            }

            if (_instance == null)
            {
                var instances = FindObjectsOfType<T>();

                if (instances.Length > 1)
                {
                    //Debug.LogError($"[单例模式] 场景中单例脚本的数量超过1: {instances.Length}。请检查场景。");
                    return _instance;
                }

                if (instances.Length == 1)
                {
                    _instance = instances[0];
                }

                if (_instance == null)
                {
                    string instanceName = typeof(T).Name;
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                    {
                        instanceGO = new GameObject(instanceName);
                        _instance = instanceGO.AddComponent<T>();
                        //DontDestroyOnLoad(instanceGO);
                        //Debug.Log($"[单例模式] 创建新的游戏对象 '{instanceGO.name}' 并添加单例脚本。");
                    }
                    else
                    {
                        //Debug.LogWarning($"[单例模式] 场景中已经存在名为 '{instanceGO.name}' 的游戏对象，但没有找到单例脚本，已自动添加。");
                        _instance = instanceGO.GetComponent<T>() ?? instanceGO.AddComponent<T>();
                        //DontDestroyOnLoad(instanceGO);
                    }
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            //Debug.LogWarning($"[单例模式] 检测到重复的 {typeof(T)} 实例 '{gameObject.name}'，该对象将被销毁。");
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

}
