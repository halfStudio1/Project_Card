using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCardView : MonoBehaviour,IDragHandler,IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardGroup _cardGroup;

    private Vector2 _originPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _originPos = _rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
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
                
            }
        }
        else
        {
            _rectTransform.anchoredPosition = _originPos;
        }

    }
}
