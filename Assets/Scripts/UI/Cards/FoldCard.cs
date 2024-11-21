using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoldCard : MonoBehaviour, IPointerClickHandler
{
    public FoldCardPanel foldCardPanel;

    public Image image;
    public Text cardName;
    public Text cardInfo;
    public Text point;

    //表示手牌中第几号牌
    public int id;

    private void Awake()
    {
        foldCardPanel = GetComponentInParent<FoldCardPanel>();
    }

    //卡片被加载时
    public void OnLoad(int id ,Card cardData)
    {
        this.id = id;

        ResMgr.Instance.LoadAssetAsync<Sprite>(cardData.spriteName, (sprite) =>
        {
            image.sprite = sprite;
        });

        cardName.text = cardData.name;
        cardInfo.text = cardData.info;
        point.text = cardData.point.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect();
    }

    //被选择
    public void OnSelect()
    {
        Debugger.LogPink($"选择了卡片{cardName.text}");
        foldCardPanel.SelectFold(this);
    }
    //被取消选择
    public void OnCancel()
    {
        foldCardPanel.CancelFold(this);
    }
}
