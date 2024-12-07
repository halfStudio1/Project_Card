using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class BattleController : SingletonMono<BattleController>
{
    //阶段状态机
    public PhaseStateMachine stateMachine;
    //战斗界面
    public BattlePanel battlePanel;
    //数据
    public EnemyObj enemyobj;
    public PlayerObj playerobj;

    //回合数
    public int round = 0;

    //下一个阶段
    public UnityAction nextPhaseAction;

    private void Start()
    {
        stateMachine.Init(this);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData()
    {
        enemyobj = new EnemyObj();
        playerobj = new PlayerObj();
        playerobj.InitDeck();
    }

    /// <summary>
    /// 获取手牌
    /// </summary>
    /// <returns></returns>
    public List<CardObj> GetHandCard()
    {
        return playerobj.handCards;
    }

    /// <summary>
    /// 摸牌
    /// </summary>
    public void DrawCard()
    {
        playerobj.DrawCard();
    }

    /// <summary>
    /// 结束摸牌/出牌阶段
    /// </summary>
    public void NextPhase()
    {
        nextPhaseAction?.Invoke();
    }

    /// <summary>
    /// 失去一张牌
    /// </summary>
    /// <param name="card"></param>
    public void LoseCard(CardObj card)
    {
        playerobj.LoseCard(card);
    }

    /// <summary>
    /// 主动弃牌
    /// </summary>
    public void ActivelyFold(int num)
    {
        UIMgr.Instance.ShowPanel<FoldCardPanel>(E_CanvasType.Top, (panel) =>
        {
            panel.Init(battlePanel, num);
        });
    }

    /// <summary>
    /// 丢弃手牌
    /// </summary>
    public void FoldCard(CardView cardView)
    {
        battlePanel.LoseCard(cardView);
        playerobj.LoseCard(cardView.cardObj);
    }

    /// <summary>
    /// 结算
    /// </summary>
    public void Settle()
    {
        enemyobj.Settle();
        playerobj.Settle();
    }

    /// <summary>
    /// 使用卡牌
    /// </summary>
    /// <param name="card"></param>
    public void UseCard(CardView cardView, EnemyView target)
    {
        if (CardMgr.Instance.UseCard(cardView.cardObj, playerobj, target.enemyObj))
        {
            battlePanel.LoseCard(cardView);
        }
    }


    #region 怪物

    /// <summary>
    /// 怪物行动
    /// </summary>
    public void MonsterAction()
    {
        enemyobj.MonsterAction();
    }

    #endregion
}
