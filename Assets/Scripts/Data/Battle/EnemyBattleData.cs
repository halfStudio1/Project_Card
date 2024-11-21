using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗中的敌人数据
public class EnemyBattleData
{
    public string name = "怪物";
    public float health = 100f;

    //buff字典
    public Dictionary<BuffType, BuffBase> buffDic = new Dictionary<BuffType, BuffBase>();

    //受到伤害
    public void GetHurt(CommandBase command)
    {
        health -= command.value;
        Debugger.LogOrange($"{name}受到{command.value}点伤害");
        command.isCompelete = true;
    }

    //获得buff
    public void GetBuff(BuffBase buff)
    {
        if (buffDic.ContainsKey(buff.type))
        {
            buffDic[buff.type].value += buff.value;
        }
        else
        {
            buffDic.Add(buff.type, buff);
        }
    }
}
