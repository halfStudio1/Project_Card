using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//战斗中的敌人数据
public class EnemyObj
{
    public string name = "怪物";
    public int health = 100;

    public int readyDamage = 0;

    //怪物被攻击时
    public UnityAction<AttackObj> onReadyHurt;
    public UnityAction onHurt;

    //怪物行动时
    public UnityAction onAction;
    //怪物行动结束时
    public UnityAction<EnemyObj> onActionEnd;

    /// <summary>
    /// 准备受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public virtual void ReadyHurt(AttackObj attackObj)
    {
        if(attackObj.attackType == E_AttackType.Penetrate)
        {
            Debugger.LogOrange("受到了穿透伤害");
        }
        else if(attackObj.attackType == E_AttackType.Bleed)
        {
            Debugger.LogOrange("受到了流血伤害");
        }

        readyDamage += attackObj.damage;
        Debugger.LogOrange($"{name}准备受到{attackObj.damage}点伤害，总共伤害：{readyDamage}");
        onReadyHurt?.Invoke(attackObj);
    }

    public virtual void Hurt(int damage)
    {
        health -= readyDamage;
        Debugger.LogOrange($"收到了{readyDamage}点伤害，剩余生命{health}");
        onHurt?.Invoke();
    }

    //buff字典
    public Dictionary<cfg.E_BuffType, BuffObj> buffDic = new Dictionary<cfg.E_BuffType, BuffObj>();
    //获得buff
    public virtual void AddBuff(BuffObj buffObj)
    {
        //如果有buff就增加层数
        if (buffDic.ContainsKey(buffObj.buff.BuffType))
        {
            buffDic[buffObj.buff.BuffType].OnAddStack(buffObj.stack);
        }
        else
        {
            buffDic.Add(buffObj.buff.BuffType, buffObj);
            buffObj.OnAdd(this);
        }
    }
    public virtual void RemoveBuff(BuffObj buffObj)
    {
        buffDic.Remove(buffObj.buff.BuffType);
        buffObj.OnRemove(this);
    }

    public virtual void Settle()
    {
        Hurt(readyDamage);
        readyDamage = 0;
    }

    public virtual void MonsterAction()
    {
        Debugger.LogPink("怪物行动了");
        onActionEnd?.Invoke(this);
    }
}

public class Drinker : EnemyObj
{
    public Drinker()
    {
        name = "酒鬼酱";
        health = 80;
    }

    public override void MonsterAction()
    {
        base.MonsterAction();
    }

    //喝点b酒干杯
    public void Skill_DrinkBWine()
    {
        //往玩家牌库随机位置塞2张“空酒杯”

        //Card card = CardMgr.Instance.GetCard(10030);
        //BattleController.Instance.GiveCard(card);
        //Card card2 = CardMgr.Instance.GetCard(10030);
        //BattleController.Instance.GiveCard(card2);

        //空酒杯：5点，无法打出，无法弃置，没有效果，回合结束消耗
        //一回合一动 判定优先级4

    }

}
