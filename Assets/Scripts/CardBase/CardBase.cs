using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    [Header("����")]
    public int Points;
    [Header("�Ƿ�����")]
    public bool isUpGrade = false;
    [Header("����ͼƬ")]
    public Sprite cardIcon;
    [Header("���ƽ���")]
    public string cardText;

    /// <summary>
    /// ����Ч��ʵ�ֺ���
    /// </summary>
    public void UseCard() 
    {
        BaseEffect();
        if (isUpGrade) 
            UpGradeEffect();
    }
    /// <summary>
    /// ����Ϊǿ��ǰ��Ч��
    /// </summary>
    protected virtual void BaseEffect() { }
    /// <summary>
    /// ����ǿ�����Ч��
    /// </summary>
    protected virtual void UpGradeEffect() { }


}
