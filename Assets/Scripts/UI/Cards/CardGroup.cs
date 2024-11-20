using cfg.card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    public float maxAngle = 20f; // 最大角度
    public float defaultInterval = 10f; // 默认间隔

    public List<CardUI> cards;

    //摸牌
    public void DrawCard(CardUI cardUI)
    {
        cards.Add(cardUI);
        Layout();
    }

    public void LoseCard(CardUI cardUI)
    {
        cardUI.gameObject.SetActive(false);
        cards.Remove(cardUI);
        Layout();
    }

    //布局
    public void Layout()
    {
        DistributeAngles(cards.Count);
    }
    void DistributeAngles(int count)
    {
        if (count == 1)
        {
            // 如果只有一个物体，直接设置为 0 度
            SetObjectAngle(cards[0].transform, 0);
            return;
        }

        // 计算实际间隔（缩减间隔以保持总角度范围）
        float interval = Mathf.Min(defaultInterval, 2 * maxAngle / (count - 1));

        // 计算第一个物体的角度起点
        float startAngle = -interval * (count - 1) / 2;

        for (int i = 0; i < count; i++)
        {
            // 当前物体的角度
            float angle = startAngle + interval * i;
            SetObjectAngle(cards[i].transform, angle);
        }
    }
    void SetObjectAngle(Transform obj, float angle)
    {
        // 假设物体围绕某个点旋转，这里仅演示沿 z 轴旋转
        obj.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
