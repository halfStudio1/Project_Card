using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEditor.Experimental.GraphView;
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
        Debugger.LogYellow("造成伤害" + num + target.ToString());
    }
    /// <summary>
    /// 可以获取到卡牌的相关操作
    /// </summary>
    /// <param name="card">需要进行操作的卡牌</param>
    /// <param name="target">卡牌需要放置的位置</param>
    public static void HandleCard(CardBase card,object target) 
    {
        Debugger.LogYellow("获取卡牌" + card.ToString() + target.ToString());
    }
    /// <summary>
    /// 随机从一个容器中筛选任意数量到任意一个容器
    /// </summary>
    /// <param name="start">需要筛选卡牌的容器</param>
    /// <param name="num">需要的数量</param>
    /// <param name="end">需要存放的容器</param>
    public static void RandomCard(object start, float num, object end) 
    {
        Debugger.LogYellow("随机获取卡牌" + start.ToString() + num.ToString() + end.ToString());
    }
    /// <summary>
    /// 修改value的值
    /// </summary>
    /// <param name="value">需要修改的值</param>
    /// <param name="num">需要增加的值</param>
    public static void ChangeValue(ref int value,int num) 
    {
        value += num;
    }

}
