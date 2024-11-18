using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    public Button Btn_DrawCard;

    public CardGroup cardGroup;
    private void Awake()
    {
        Btn_DrawCard.onClick.AddListener(OnBtn_DrawCardClick);
    }

    private void OnBtn_DrawCardClick()
    {
        if(BattleController.Instance.stateMachine.currentStateType != typeof(Phase_Draw))
            return;

        BattleController.Instance.DrawCard();
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
