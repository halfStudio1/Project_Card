using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据
/// </summary>
public class PlayerData
{
    public int[] cardID;
}

public class DatasMgr : Singleton<DatasMgr>
{
    public PlayerData playerData;


    public void LoadData()
    {
        playerData = JsonMgr.Instance.Load<PlayerData>("Player");

        playerData.cardID = new int[] { 10001,10001, 10002, 10002, 10003, 10003, 10004, 10004, 10004, 10008, 10008 };
    }

    public void SaveData()
    {

    }
}
