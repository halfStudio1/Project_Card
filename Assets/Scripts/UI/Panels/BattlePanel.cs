using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    //摸牌
    public Button Btn_DrawCard;

    //结束摸牌、出牌
    public Button Btn_NextPhase;

    public CardGroup cardGroup;
    private void Awake()
    {
        Btn_DrawCard.onClick.AddListener(OnBtn_DrawCardClick);
        Btn_NextPhase.onClick.AddListener(OnBtn_NextPhaseClick);
    }

    private void OnBtn_DrawCardClick()
    {
        if (BattleController.Instance.stateMachine.currentStateType != typeof(Phase_Draw))
            return;

        BattleController.Instance.DrawCard();
    }
    private void OnBtn_NextPhaseClick()
    {
        BattleController.Instance.NextPhase();
    }

    public void DrawCard(CardUI cardUI)
    {
        cardGroup.DrawCard(cardUI);
    }

    public override void HideMe()
    {
        gameObject.SetActive(false);
    }

    public override void ShowMe()
    {
        gameObject.SetActive(true);
    }
}
