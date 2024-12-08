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

        playerData.cardID = new int[] { 10001, 10001, 10002, 10002, 10003, 10003, 10004, 10004, 10004,
                                        10007, 10008, 10008, 10011, 10012, 10012, 10015, 10016, 10018,
                                        10018, 10020, 10023, 10023, 10027, 10027};
    }

    public void SaveData()
    {

    }
}
