using cfg.card;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//战斗中的敌人数据
public class EnemyBattleData
{
    public string name = "怪物";
    public float health = 100f;

    public HashSet<BuffBase> buffs = new HashSet<BuffBase>();

    //受到伤害
    public void GetHurt(int damage)
    {
        Debugger.LogOrange($"{name}受到{damage}点伤害");
    }
}

//战斗中的玩家数据
public class PlayerBattleData
{
    //卡组
    public List<Card> deck = new List<Card>();

    //手牌
    public List<Card> handCards = new List<Card>();

    //弃牌堆
    public List<Card> deadDeck = new List<Card>();

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
        deck = new List<Card>();

        //初始化卡组内的卡
        for (int i = 0; i < playerCard.Length; i++)
        {
            //从配置表的数据中取出相应的卡
            cfg.card.Cards cfgCard = cardsDic[playerCard[i]];

            //填入相应数据
            Card card = new Card(cfgCard.Id, cfgCard.Name, cfgCard.ImgName, cfgCard.Info, cfgCard.CardType, cfgCard.CardFace, cfgCard.Point, cfgCard.Value);
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
    public void DrawCard(BattlePanel battlePanel)
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
        //放入手牌
        handCards.Add(topCard);
        //创建一个CardUI对象
        //把Card传给CardUI对象
        ResMgr.Instance.LoadAssetAsync<GameObject>("Card", (obj) =>
        {
            obj = GameObject.Instantiate(obj, battlePanel.cardGroup.transform);
            CardUI cardUI = obj.GetComponent<CardUI>();
            cardUI.OnLoad(topCard);
            battlePanel.DrawCard(cardUI);
        });
    }

    public void LoseCard(Card card)
    {
        //从手牌移除
        handCards.Remove(card);
        //放入弃牌堆
        deadDeck.Add(card);
    }
}

//战斗数据
public class BattleData
{
    public PlayerBattleData playerBattleData = new PlayerBattleData();

    public EnemyBattleData enemyBattleData = new EnemyBattleData();

    public BattleData()
    {

    }
}

public class BattleController : SingletonMono<BattleController>
{
    //阶段状态机
    public PhaseStateMachine stateMachine;
    //战斗界面
    public BattlePanel battlePanel;
    //数据
    public BattleData battleData;

    public UnityAction nextPhaseAction;

    private void Start()
    {
        stateMachine.Init(this);
    }

    /// <summary>
    /// 初始化卡组
    /// </summary>
    public void InitDeck()
    {
        battleData = new BattleData();

        battleData.playerBattleData.InitDeck();
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        battleData.playerBattleData.Shuffle();
    }

    /// <summary>
    /// 摸牌
    /// </summary>
    public void DrawCard()
    {
        battleData.playerBattleData.DrawCard(battlePanel);
    }

    //结束摸牌/出牌阶段
    public void NextPhase()
    {
        nextPhaseAction?.Invoke();
    }

    //失去手牌
    public void LoseCard()
    {

    }

    //使用卡牌
    public void UseCard(Card card)
    {
        battleData.playerBattleData.LoseCard(card);

        //使用效果
        switch (card.cardFace)
        {
            case cfg.E_CardFace.LongSword:
                {
                    TakeDamage(card.value);
                    break;
                }
            case cfg.E_CardFace.Knife:
                {
                    break;
                }
            case cfg.E_CardFace.Mask:
                {
                    break;
                }
            case cfg.E_CardFace.Scissors:
                {
                    break;
                }
            case cfg.E_CardFace.Eye:
                {
                    break;
                }
        }
    }


    public void TakeDamage(int damage)
    {
        battleData.enemyBattleData.GetHurt(damage);
    }
}
