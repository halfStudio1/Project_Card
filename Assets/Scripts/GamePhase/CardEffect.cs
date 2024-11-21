using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleController
{
    /// <summary>
    /// 使用卡牌
    /// </summary>
    /// <param name="card"></param>
    public void UseCard(Card card)
    {
        _playerBattleData.LoseCard(card);

        //使用效果
        switch (card.cardFace)
        {
            case cfg.E_CardFace.LongSword:
                {
                    ReadyDamage(card.value);
                    break;
                }
            case cfg.E_CardFace.Knife:
                {
                    Bleed bleed = new Bleed(card.value);
                    InflictBuff(bleed);
                    break;
                }
            case cfg.E_CardFace.Mask:
                {
                    break;
                }
            case cfg.E_CardFace.Scissors:
                {
                    Fold fold = new Fold(card.value);
                    battleData.commandQueue.Enqueue(fold);
                    break;
                }
            case cfg.E_CardFace.Eye:
                {
                    break;
                }
        }
    }


    #region 卡牌效果

    /// <summary>
    /// 施加Buff
    /// </summary>
    /// <param name="buff"></param>
    public void InflictBuff(BuffBase buff)
    {
        _enemyBattleData.GetBuff(buff);

        Debugger.LogPink($"增加流血层数：{buff.value}层，当前流血层数{_enemyBattleData.buffDic[BuffType.Bleed].value}层");
    }

    /// <summary>
    /// 准备伤害
    /// </summary>
    /// <param name="damage"></param>
    public void ReadyDamage(int damage)
    {
        Attack attack = new Attack(damage);

        //先准备伤害
        battleData.commandQueue.Enqueue(attack);
        Debugger.LogPink($"准备伤害：{damage}");

        //如果有流血debuff，就追加伤害
        if (_enemyBattleData.buffDic.ContainsKey(BuffType.Bleed))
        {
            //追加流血层数的伤害
            //_enemyBattleData.ReadyHurt(_enemyBattleData.buffDic[BuffType.Bleed].value);
            Attack attackAddition = new Attack(_enemyBattleData.buffDic[BuffType.Bleed].value);
            battleData.commandQueue.Enqueue(attackAddition);
            Debugger.LogPink($"追加伤害：{_enemyBattleData.buffDic[BuffType.Bleed].value}");

            //流血层数减少1
            _enemyBattleData.buffDic[BuffType.Bleed].value -= 1;
            Debugger.LogPink($"减少1层流血层数，剩余：{_enemyBattleData.buffDic[BuffType.Bleed].value}层");

            //如果没有层数了，就移除这个buff
            if (_enemyBattleData.buffDic[BuffType.Bleed].value <= 0)
            {
                _enemyBattleData.buffDic.Remove(BuffType.Bleed);

                Debugger.LogPink("没有流血buff了");
            }
        }
    }

    /// <summary>
    /// 选择弃牌
    /// </summary>
    /// <param name="command"></param>
    public void SelectFold(CommandBase command)
    {
        Debugger.LogPink($"选择弃牌，弃置{command.value}张");
        UIMgr.Instance.ShowPanel<FoldCardPanel>(E_CanvasType.Top, (panel) =>
        {
            //告诉弃牌panel，当前的手牌数据，需要弃多少牌，弃牌完成过后做什么
            panel.Init(_playerBattleData.handCards, command.value, () => { command.isCompelete = true; });
        });
    }

    #endregion
}
