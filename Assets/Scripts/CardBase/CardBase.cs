using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase
{
    public E_CradType e_CradType;
    //ÊýÖµ
    public float num;
    public bool isUpGrade = false;
    public void UseCard() 
    {
        BaseEffect();
        if (isUpGrade) 
            UpGradeEffect();
    }
    protected virtual void BaseEffect() { }
    protected virtual void UpGradeEffect() { }


}
