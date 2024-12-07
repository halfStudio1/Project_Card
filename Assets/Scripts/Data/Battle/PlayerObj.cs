using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//战斗中的玩家数据
public class PlayerObj
{
    //卡组
    public List<int> deck = new List<int>();

    //手牌
    public List<CardObj> handCards = new List<CardObj>();

    //弃牌堆
    public List<int> deadDeck = new List<int>();

    public int health = 3;

    public int maxPoint = 21;


    public UnityAction<AttackObj> onAttack;
    public UnityAction<AttackObj> onHurt;
    public UnityAction onDeath;


    public void Attack(AttackObj attackObj)
    {
        onAttack?.Invoke(attackObj);
        attackObj.TakeDamage();
    }
    public void Hurt(AttackObj attackObj)
    {
        onHurt?.Invoke(attackObj);
    }


    public List<PlayerBuffObj> playerBuffObjs = new List<PlayerBuffObj>();
    public void AddBuff(PlayerBuffObj buffObj)
    {
        buffObj.OnAdd(this);
    }
    public void RemoveBuff(PlayerBuffObj buffObj)
    {
        buffObj.OnRemove(this);
    }



    #region 行动：洗牌、摸牌、出牌等

    /// <summary>
    /// 初始化卡组
    /// </summary>
    public void InitDeck()
    {
        //读取配置表中的数据
        Dictionary<int, cfg.card.Cards> cardsDic = ConfigTableMgr.Instance.GetCardDic();

        //读取玩家数据
        int[] playerCard = DatasMgr.Instance.playerData.cardID;

        //卡组

        //初始化卡组内的卡
        for (int i = 0; i < playerCard.Length; i++)
        {
            deck.Add(playerCard[i]);
        }

        Shuffle();
        Debugger.LogPink("初始化卡组完成");
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        Shuffle(deck);
        Debugger.LogPink("洗牌");
    }
    private void Shuffle(List<int> deck)
    {
        System.Random random = new System.Random();

        // 随机排序
        for (int i = deck.Count - 1; i > 0; i--)
        {
            // 生成一个随机索引
            int j = random.Next(i + 1);

            // 交换元素
            int temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    /// <summary>
    /// 摸牌
    /// </summary>
    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            Debugger.LogPink("牌库摸完了");
            return;
        }

        Debugger.LogPink("摸牌");
        //从牌顶抽一张
        CardObj topCard = CardMgr.Instance.GetCard(deck[deck.Count - 1]);
        deck.Remove(deck[deck.Count - 1]);
        //放入手牌
        handCards.Add(topCard);

        //告诉BattlePanel，我摸了一张牌，并且给他牌的数据
        BattleController.Instance.battlePanel.DrawCard(topCard);
    }

    /// <summary>
    /// 从手牌移除卡牌
    /// </summary>
    /// <param name="card"></param>
    public void LoseCard(CardObj card)
    {
        //从手牌移除
        handCards.Remove(card);
        Debugger.LogPink($"失去卡牌{card.name}");
        //放入弃牌堆
        deadDeck.Add(card.id);
    }

    /// <summary>
    /// 结算
    /// </summary>
    public void Settle()
    {

    }

    #endregion



}
