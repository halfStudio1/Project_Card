using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : CardBase
{
    public float attckNum;

    public object target;

    protected override void BaseEffect()
    {
        CardEffect.BaseEffectReal(e_CradType,attckNum,null,target);
    }
    protected override void UpGradeEffect()
    {
        base.UpGradeEffect();
    }
}
