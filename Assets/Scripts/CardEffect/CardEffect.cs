using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEditor.Experimental.GraphView;
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
        Debugger.LogYellow("����˺�" + num + target.ToString());
    }
    /// <summary>
    /// ���Ի�ȡ�����Ƶ���ز���
    /// </summary>
    /// <param name="card">��Ҫ���в����Ŀ���</param>
    /// <param name="target">������Ҫ���õ�λ��</param>
    public static void HandleCard(CardBase card,object target) 
    {
        Debugger.LogYellow("��ȡ����" + card.ToString() + target.ToString());
    }
    /// <summary>
    /// �����һ��������ɸѡ��������������һ������
    /// </summary>
    /// <param name="start">��Ҫɸѡ���Ƶ�����</param>
    /// <param name="num">��Ҫ������</param>
    /// <param name="end">��Ҫ��ŵ�����</param>
    public static void RandomCard(object start, float num, object end) 
    {
        Debugger.LogYellow("�����ȡ����" + start.ToString() + num.ToString() + end.ToString());
    }
    /// <summary>
    /// �޸�value��ֵ
    /// </summary>
    /// <param name="value">��Ҫ�޸ĵ�ֵ</param>
    /// <param name="num">��Ҫ���ӵ�ֵ</param>
    public static void ChangeValue(ref int value,int num) 
    {
        value += num;
    }

}
