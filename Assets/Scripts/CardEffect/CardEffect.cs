using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// ��ȡ����
    /// </summary>
    /// <param name="card">����</param>
    /// <param name="target">�ƶѻ�������</param>
    public static void GetCard(object card,object target) 
    {
        Debugger.LogYellow("��ȡ����");
    }
    /// <summary>
    /// ʧȥ����
    /// </summary>
    /// <param name="card">����</param>
    /// <param name="target">���ƶѻ�����</param>
    public static void LoseCard(object card,object target) 
    {
        Debugger.LogYellow("ʧȥ����");
    }
}
