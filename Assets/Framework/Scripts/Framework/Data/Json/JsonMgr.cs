using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Json数据管理类
/// Json序列化存储到硬盘
/// 反序列化从硬盘中读取到内存中
/// </summary>
public class JsonMgr : Singleton<JsonMgr>
{

    /// <summary>
    /// 存储 序列化
    /// </summary>
    /// <param name="data"></param>
    /// <param name="fileName">文件名称</param>
    public void Save(object data, string fileName)
    {
        //确定存储路径
        string path = Application.persistentDataPath + "/Data/" + fileName + ".json";
        //序列化 得到Json字符串
        string jsonStr = JsonMapper.ToJson(data);
        //把序列化的Json字符串 存储到指定路径的文件中
        File.WriteAllText(path, jsonStr);
        //Debug.Log("已经存储到" + path);
    }

    /// <summary>
    /// 读取 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public T Load<T>(string fileName) where T : new()
    {
        //确定从哪个路径读取
        //首先先判断 默认数据文件夹中是否有我们想要的数据 如果有 就从中获取
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //先判断 是否存在这个文件
        //如果不存在默认文件 就从 读写文件夹中去寻找
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //如果读写文件夹中都还没有 那就返回一个默认对象
        if (!File.Exists(path))
            return new T();

        //进行反序列化
        string jsonStr = File.ReadAllText(path);
        //数据对象
        T data = JsonMapper.ToObject<T>(jsonStr);

        //把对象返回出去
        return data;
    }

    /// <summary>
    /// 是否存在数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public bool HasData(string fileName)
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //如果不存在默认文件 就从 读写文件夹中去寻找
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //如果读写文件夹中都还没有 那就返回一个默认对象
        if (!File.Exists(path))
            return false;

        return true;
    }
}
