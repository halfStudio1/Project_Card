using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardEffect
{
    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="num">伤害的数值</param>
    /// <param name="target">攻击的目标</param>
    public static void DealDamage(float num,object target) 
    {
        Debugger.LogYellow("造成伤害");
    }
    /// <summary>
    /// 获取卡牌
    /// </summary>
    /// <param name="card">卡牌</param>
    /// <param name="target">牌堆或者手牌</param>
    public static void GetCard(object card,object target) 
    {
        Debugger.LogYellow("获取卡牌");
    }
    /// <summary>
    /// 失去卡牌
    /// </summary>
    /// <param name="card">卡牌</param>
    /// <param name="target">弃牌堆或消耗</param>
    public static void LoseCard(object card,object target) 
    {
        Debugger.LogYellow("失去卡牌");
    }
}
