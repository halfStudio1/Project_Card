using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    public BattleController battleController;

    //摸牌
    public Button Btn_DrawCard;

    //结束摸牌、出牌
    public Button Btn_NextPhase;

    public EnemyView enemyView;
    public CardGroup cardGroup;

    public override void HideMe()
    {
        gameObject.SetActive(false);
    }

    public override void ShowMe()
    {
        gameObject.SetActive(true);
    }
    private void Awake()
    {
        Btn_DrawCard.onClick.AddListener(OnBtn_DrawCardClick);
        Btn_NextPhase.onClick.AddListener(OnBtn_NextPhaseClick);
    }

    /// <summary>
    /// 点击摸牌按钮
    /// </summary>
    private void OnBtn_DrawCardClick()
    {
        Debugger.LogYellow(BattleController.Instance.stateMachine.currentStateType);
        //必须在摸牌阶段
        if (BattleController.Instance.stateMachine.currentStateType != typeof(Phase_Draw))
            return;

        //摸牌
        BattleController.Instance.DrawCard();
    }


    private void OnBtn_NextPhaseClick()
    {
        BattleController.Instance.NextPhase();
    }

    public void DrawCard(CardObj cardObj)
    {
        cardGroup.DrawCard(cardObj);
    }

    public void LoseCard(CardView cardView)
    {
        cardGroup.LoseCard(cardView);
    }
}
