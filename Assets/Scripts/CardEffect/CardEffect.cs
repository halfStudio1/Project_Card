using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_CradType 
{
    None = 0,
    Attack = 1,
    Defence = 2,
    Resource = 3,
    Buff = 4,
    Heal = 5,
    Num = 6,
}
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
    /// 特定卡牌相关
    /// </summary>
    /// <param name="card">需要进行操作的卡牌</param>
    /// <param name="target">卡牌需要放置的位置</param>
    public static void GetCard(CardBase card,object target) 
    {
        Debugger.LogYellow("获取卡牌");
    }
    /// <summary>
    /// 根据基础类型实现效果
    /// </summary>
    /// <param name="cradType">卡牌类型</param>
    /// <param name="num">数值</param>
    /// <param name="card">卡牌</param>
    /// <param name="target">卡牌作用目标/卡牌移动位置</param>
    public static void BaseEffectReal(E_CradType cradType = E_CradType.None,float num = 0,CardBase card = null,object target = null) 
    {
        switch (cradType) 
        {
            case E_CradType.Attack:
                if (num != 0 && target != null)
                    DealDamage(num,target);
                break;
            case E_CradType.Defence:
                break;
            case E_CradType.Resource:
                break;
            case E_CradType.Buff:
                break;
            case E_CradType.Heal:
                break;
            case E_CradType.Num:
                break;

        }
    }
}
