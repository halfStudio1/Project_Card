using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoldCard : MonoBehaviour, IPointerClickHandler
{
    public FoldCardPanel foldCardPanel;

    public CardView cardView;

    public Image image;
    public Text cardName;
    public Text cardInfo;
    public Text point;

    private Vector3 _originPos;

    private void Awake()
    {
        foldCardPanel = GetComponentInParent<FoldCardPanel>();
    }

    //卡片被加载时
    public void Init(CardView cardView)
    {
        this.cardView = cardView;
        image.sprite = cardView.image.sprite;
        cardName.text = cardView.cardName.text;
        cardInfo.text = cardView.cardInfo.text;
        point.text = cardView.point.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect();
    }

    //被选择
    public void OnSelect()
    {
        Debugger.LogPink($"选择了卡片：{cardName.text}");
        foldCardPanel.SelectFold(this);
    }
    //被取消选择
    public void OnCancel()
    {
        Debugger.LogPink($"取消选择了卡片：{cardName.text}");
        foldCardPanel.CancelFold(this);
    }
}
