using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : CardBase
{
    [Header("卡牌数值")]
    public float attckNum;
    [Header("作用目标")]
    public object target;

    protected override void BaseEffect()
    {
        base.BaseEffect();
    }
    protected override void UpGradeEffect()
    {
        base.UpGradeEffect();
    }
}
