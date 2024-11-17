using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    [Header("点数")]
    public int Points;
    [Header("是否升级")]
    public bool isUpGrade = false;
    [Header("卡面图片")]
    public Sprite cardIcon;
    [Header("卡牌介绍")]
    public string cardText;

    /// <summary>
    /// 卡牌效果实现函数
    /// </summary>
    public void UseCard() 
    {
        BaseEffect();
        if (isUpGrade) 
            UpGradeEffect();
    }
    /// <summary>
    /// 卡牌为强化前的效果
    /// </summary>
    protected virtual void BaseEffect() { }
    /// <summary>
    /// 卡牌强化后的效果
    /// </summary>
    protected virtual void UpGradeEffect() { }


}
