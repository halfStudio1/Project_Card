using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Card card;

    public Image image;
    public Text cardName;
    public Text cardInfo;
    public Text point;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardGroup _cardGroup;

    private Vector2 _originPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _originPos = _rectTransform.anchoredPosition;
        _cardGroup = GetComponentInParent<CardGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (BattleController.Instance.stateMachine.currentStateType != typeof(Phase_Play))
        {
            _rectTransform.anchoredPosition = _originPos;
            return;
        }

        if ((_rectTransform.anchoredPosition.y - _originPos.y) > 200f)
        {
            Debugger.LogPink($"使用了{card.name}");
            BattleController.Instance.UseCard(card);
            _cardGroup.LoseCard(this);
        }
        else
        {
            _rectTransform.anchoredPosition = _originPos;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    //摸牌时，会启用
    public void OnLoad(Card cardData)
    {
        card = cardData;

        ResMgr.Instance.LoadAssetAsync<Sprite>(card.spriteName, (sprite) =>
        {
            image.sprite = sprite;
        });

        cardName.text = card.name;
        cardInfo.text = card.info;
        point.text = card.point.ToString();
    }


}
