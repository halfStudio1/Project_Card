using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardObj cardObj;

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
        //必须在出牌阶段
        if (BattleController.Instance.stateMachine.currentStateType != typeof(Phase_Play))
        {
            _rectTransform.anchoredPosition = _originPos;
            return;
        }

        if ((_rectTransform.anchoredPosition.y - _originPos.y) > 200f)
        {
            //发射射线，获取到Enemy
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                EnemyView enemyView = hit.collider.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    // 执行相关逻辑
                    BattleController.Instance.UseCard(this, enemyView);
                }
            }
        }

        _rectTransform.anchoredPosition = _originPos;
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

    //摸牌时，初始化卡牌数据
    public void Init(CardObj cardData)
    {
        cardObj = cardData;

        //加载图片
        ResMgr.Instance.LoadAssetAsync<Sprite>(cardObj.spriteName, (sprite) =>
        {
            image.sprite = sprite;
        });
        //加载数据
        cardName.text = cardObj.name;
        cardInfo.text = cardObj.info;
        point.text = cardObj.point.ToString();
    }
}
