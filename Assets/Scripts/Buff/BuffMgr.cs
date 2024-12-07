using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMgr : Singleton<BuffMgr>
{
    public Dictionary<int, cfg.buff.Buff> buffDic = new Dictionary<int, cfg.buff.Buff>();

    public void Init()
    {
        buffDic = ConfigTableMgr.Instance.GetBuffDic();
    }


    public cfg.buff.Buff GetBuff(cfg.E_BuffType e_BuffType)
    {
        return buffDic[(int)e_BuffType];
    }


    public void GiveBuff(BuffObj buffObj, EnemyObj enemyObj)
    {
        enemyObj.AddBuff(buffObj);
    }
}
