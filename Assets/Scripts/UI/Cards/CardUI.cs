using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public Card card;

    public Image image;
    public Text cardName;
    public Text cardInfo;
    public Text point;

    public void OnDrag(PointerEventData eventData)
    {
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
