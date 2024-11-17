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
    /// ����˺�
    /// </summary>
    /// <param name="num">�˺�����ֵ</param>
    /// <param name="target">������Ŀ��</param>
    public static void DealDamage(float num,object target) 
    {
        Debugger.LogYellow("����˺�");
    }
    /// <summary>
    /// �ض��������
    /// </summary>
    /// <param name="card">��Ҫ���в����Ŀ���</param>
    /// <param name="target">������Ҫ���õ�λ��</param>
    public static void GetCard(CardBase card,object target) 
    {
        Debugger.LogYellow("��ȡ����");
    }
    /// <summary>
    /// ���ݻ�������ʵ��Ч��
    /// </summary>
    /// <param name="cradType">��������</param>
    /// <param name="num">��ֵ</param>
    /// <param name="card">����</param>
    /// <param name="target">��������Ŀ��/�����ƶ�λ��</param>
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
