using cfg;
using System.Collections.Generic;

public class CardMgr : Singleton<CardMgr>
{
    public Dictionary<int, cfg.card.Cards> cardsDic = new Dictionary<int, cfg.card.Cards>();
    public Dictionary<E_CardFace, ICardEffect> effectDic = new Dictionary<E_CardFace, ICardEffect>();

    public void Init()
    {
        //初始化，从配置表中读取出所有的卡
        cardsDic = ConfigTableMgr.Instance.GetCardDic();

        effectDic.Add(E_CardFace.Sword, new SwordEffect());
        effectDic.Add(E_CardFace.Knife, new KnifeEffect());
        effectDic.Add(E_CardFace.Mask, new MaskEffect());
        effectDic.Add(E_CardFace.Scissors, new ScissorsEffect());
        effectDic.Add(E_CardFace.Eyes, new EyesEffect());
        effectDic.Add(E_CardFace.Spear, new SpearEffect());
        effectDic.Add(E_CardFace.Apple, new AppleEffect());
        effectDic.Add(E_CardFace.WolfTooth, new WolfToothEffect());
        effectDic.Add(E_CardFace.Scroll, new ScrollEffect());
        effectDic.Add(E_CardFace.Goblet, new GobletEffect());
        effectDic.Add(E_CardFace.Poison, new PoisonEffect());
        effectDic.Add(E_CardFace.Rivet, new RivetEffect());


    }

    /// <summary>
    /// 获取一张卡
    /// </summary>
    /// <param name="id">卡的id</param>
    /// <returns></returns>
    public CardObj GetCard(int id)
    {
        if (cardsDic.ContainsKey(id))
        {
            cfg.card.Cards cfgCard = cardsDic[id];
            CardObj cardObj = new CardObj(cfgCard.Id, cfgCard.Name, cfgCard.ImgName, cfgCard.Info, cfgCard.CardType, cfgCard.CardFace, cfgCard.Point, cfgCard.Value);
            return cardObj;
        }
        else
        {
            Debugger.LogError($"不存在id为：{id}的牌");
            return null;
        }
    }

    /// <summary>
    /// 使用卡牌
    /// </summary>
    /// <param name="cardObj"></param>
    /// <param name="target"></param>
    public bool UseCard(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return effectDic[cardObj.cardFace].ExecuteEffect(cardObj, user, target);
    }
}
