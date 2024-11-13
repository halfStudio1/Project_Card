using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池数据
/// </summary>
public class PoolData
{
    //抽屉根对象 用来进行布局管理的对象
    private GameObject rootObj;

    //用来存储对象池中的对象 记录的是没有使用的对象
    private Stack<GameObject> _unusedDataStack = new Stack<GameObject>();
    //用来记录使用中的对象的
    private List<GameObject> _usedList = new List<GameObject>();

    //场景上同时存在的对象的上限个数
    //如果不初始化的话，就是无限个
    private int _maxNum = -1;

    //获取容器中没有使用的对象数量
    public int UnusedCount => _unusedDataStack.Count;
    //获取容器中使用中的对象数量
    public int UsedCount => _usedList.Count;

    /// <summary>
    /// 是否需要实例化一个新的对象，当使用中的对象没有达到上限时，就实例化新的对象
    /// </summary>
    public bool NeedCreate => (_usedList.Count < _maxNum) || (_maxNum == -1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root">对象池的根节点</param>
    /// <param name="name">对象池名字</param>
    /// <param name="usedObj"></param>
    public PoolData(GameObject root, string name, GameObject usedObj)
    {
        if (GameObjectPoolMgr.isOpenLayout)
        {
            rootObj = new GameObject(name + "Root");
            rootObj.transform.SetParent(root.transform);
        }

        if (usedObj != null)
            //将外部创建的obj放到容器中
            PushUsedList(usedObj);
    }

    //取出对象
    public GameObject Pop()
    {
        GameObject obj;

        //如果有空闲对象
        if (UnusedCount > 0)
        {
            //从存空闲对象的容器中取出
            obj = _unusedDataStack.Pop();
            //放到存使用中的对象的容器中
            _usedList.Add(obj);
        }
        //如果没有空闲对象
        else
        {
            //把最早使用的对象取出来，重新使用
            obj = _usedList[0];
            _usedList.RemoveAt(0);
            _usedList.Add(obj);
        }

        //激活
        obj.SetActive(true);

        if (GameObjectPoolMgr.isOpenLayout)
            obj.transform.SetParent(null);

        return obj;
    }

    //存入对象
    public void Push(GameObject obj)
    {
        //先失活
        obj.SetActive(false);

        if (GameObjectPoolMgr.isOpenLayout)
            obj.transform.SetParent(rootObj.transform);

        //存入到记录空闲对象的容器中
        _unusedDataStack.Push(obj);
        //从记录使用对象的容器中移除
        _usedList.Remove(obj);
    }

    //存放到记录使用对象的容器中
    public void PushUsedList(GameObject obj)
    {
        _usedList.Add(obj);
    }

    public void Init(int maxNum)
    {
        _maxNum = maxNum;
    }
}

public class GameObjectPoolMgr : Singleton<GameObjectPoolMgr>
{
    //是否开启布局功能，开启后，取出存入物体时，会放到相应的根节点上
    //在最终打包时建议关闭，以提高性能
    public static bool isOpenLayout = true;

    //对象池根节点
    private GameObject poolObjRoot;

    //存储所有的对象池
    private Dictionary<string, PoolData> _poolDic = new Dictionary<string, PoolData>();

    /// <summary>
    /// 初始化对象池，如果不初始化直接使用的话，对象池上限就是无限
    /// </summary>
    /// <param name="name"></param>
    /// <param name="maxNum">存储对象上限</param>
    /// <param name="preCreateNum">预创建几个对象</param>
    public void InitPool(string name, int maxNum, int preCreateNum)
    {
        //如果根物体为空 就创建
        if (poolObjRoot == null && isOpenLayout)
            poolObjRoot = new GameObject("PoolRoot");

        PoolData poolData = new PoolData(poolObjRoot, name, null);
        poolData.Init(maxNum);
        _poolDic.Add(name, poolData);

        for (int i = 0; i < preCreateNum; i++)
        {
            GetObject(name, Push);
        }

        void Push(GameObject obj)
        {
            PushObj(obj);
        }
    }
    public void GetObject(string name, Action<GameObject> callBack)
    {
        //如果根物体为空 就创建
        if (poolObjRoot == null && isOpenLayout)
            poolObjRoot = new GameObject("PoolRoot");

        //如果没有该对象池，或者对象池中没有空闲的对象，并且对象数量没有达到上限
        if (!_poolDic.ContainsKey(name) ||
            (_poolDic[name].UnusedCount == 0 && _poolDic[name].NeedCreate))
        {
            //加载一个对象
            ResMgr.Instance.LoadAssetAsync<GameObject>(name, OnLoad);
        }
        //如果已经有该对象池，并且对象池中有空闲的对象，或者对象池数量达到上限了，就直接取一个来用
        else
        {
            //传出去
            callBack?.Invoke(_poolDic[name].Pop());
        }

        void OnLoad(GameObject obj)
        {
            obj = GameObject.Instantiate(obj);
            obj.name = name;

            //如果没有对象池就创建对象池，并且存入对象
            if (!_poolDic.ContainsKey(name))
                _poolDic.Add(name, new PoolData(poolObjRoot, name, obj));
            //如果有对象池，就存到记录使用对象的容器
            else
                _poolDic[name].PushUsedList(obj);

            //加载完过后，传出去
            callBack?.Invoke(obj);
        }
    }

    public void PushObj(GameObject obj)
    {
        _poolDic[obj.name].Push(obj);
    }

    public void ClearPool()
    {
        _poolDic.Clear();
        poolObjRoot = null;
    }
}
