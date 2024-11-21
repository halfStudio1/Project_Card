using System.Collections.Generic;
using UnityEngine.Events;


public partial class BattleController : SingletonMono<BattleController>
{
    //阶段状态机
    public PhaseStateMachine stateMachine;
    //战斗界面
    public BattlePanel battlePanel;
    //数据
    public BattleData battleData;
    private EnemyBattleData _enemyBattleData;
    private PlayerBattleData _playerBattleData;

    //下一个阶段
    public UnityAction nextPhaseAction;

    private void Start()
    {
        stateMachine.Init(this);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitDeck()
    {
        battleData = new BattleData();
        battleData.playerBattleData.InitDeck();

        _enemyBattleData = battleData.enemyBattleData;
        _playerBattleData = battleData.playerBattleData;
    }

    /// <summary>
    /// 获取手牌
    /// </summary>
    /// <returns></returns>
    public List<Card> GetHandCard()
    {
        return _playerBattleData.handCards;
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        _playerBattleData.Shuffle();
    }

    /// <summary>
    /// 摸牌
    /// </summary>
    public void DrawCard()
    {
        _playerBattleData.DrawCard(battlePanel);
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
    public void LoseCard(Card card)
    {
        _playerBattleData.LoseCard(card);
    }

    /// <summary>
    /// 丢弃手牌
    /// </summary>
    /// <param name="id">手牌中第几号位置</param>
    public void FoldCard(int id)
    {
        _playerBattleData.LoseCard(_playerBattleData.handCards[id]);
        battlePanel.LoseCard(id);
    }

    /// <summary>
    /// 结算
    /// </summary>
    public void Settle()
    {
        battleData.Settle();
    }


}
