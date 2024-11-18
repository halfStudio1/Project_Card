using cfg.card;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : SingletonMono<BattleController>
{
    public PhaseStateMachine stateMachine;

    public BattlePanel battlePanel;

    //卡组
    public List<Card> deck;

    //手牌
    public List<Card> handCards;

    //弃牌堆
    public List<Card> deadDeck;

    private void Start()
    {
        stateMachine.Init(this);
    }

    /// <summary>
    /// 初始化卡组
    /// </summary>
    public void InitDeck()
    {
        //读取配置表中的数据
        Dictionary<int, cfg.card.Cards> cardsDic = ConfigTableMgr.Instance.GetCardDic();

        //读取玩家组的卡
        int[] playerCard = DatasMgr.Instance.playerData.cardID;

        //卡组
        deck = new List<Card>();

        //初始化卡组内的卡
        for (int i = 0; i < playerCard.Length; i++)
        {
            //从配置表的数据中取出相应的卡
            cfg.card.Cards cfgCard = cardsDic[playerCard[i]];

            //填入相应数据
            Card card = new Card(cfgCard.Id, cfgCard.Info, cfgCard.Info, cfgCard.CardType, cfgCard.Point, cfgCard.ImgName);
            deck.Add(card);
        }

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
    private void Shuffle(List<Card> deck)
    {
        // 获取随机数生成器
        System.Random random = new System.Random();

        // 随机排序
        for (int i = deck.Count - 1; i > 0; i--)
        {
            // 生成一个随机索引
            int j = random.Next(i + 1);

            // 交换元素
            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    //摸牌
    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            Debugger.LogPink("牌库摸完了");
            return;
        }

        Debugger.LogPink("摸牌");
        //从牌顶抽一张
        Card topCard = deck[deck.Count - 1];
        deck.Remove(deck[deck.Count - 1]);
        //创建一个CardUI对象
        //把Card传给CardUI对象
        ResMgr.Instance.LoadAssetAsync<GameObject>("Card", (obj) =>
        {
            obj = Instantiate(obj, battlePanel.cardGroup.transform);
            CardUI cardUI = obj.GetComponent<CardUI>();
            cardUI.card = topCard;
            battlePanel.DrawCard(cardUI);
        });
    }

    //失去手牌
    public void LoseCard()
    {

    }

    //使用卡牌
    public void UseCard()
    {

    }
}
