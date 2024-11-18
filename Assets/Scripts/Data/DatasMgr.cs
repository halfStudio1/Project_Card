using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleData
{
    


}
public class EnemyData
{
    public float health = 100f;

    public HashSet<BuffBase> buffs = new HashSet<BuffBase>();

}
public class PlayerData
{

    public int[] cardID;

}
public class BattlePlayerData
{
    //血量
    public int health = 3;
    //buff
    public HashSet<BuffBase> buffs = new HashSet<BuffBase>();
    //牌组
    public HashSet<CardBase> cards = new HashSet<CardBase>();
}

public class DatasMgr : Singleton<DatasMgr>
{
    public PlayerData playerData;


    public void LoadData()
    {
        playerData = JsonMgr.Instance.Load<PlayerData>("Player");

        playerData.cardID = new int[] { 10001,10002 };
    }

    public void SaveData()
    {

    }
}
