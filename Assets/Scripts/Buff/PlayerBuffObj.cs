using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBuffObj
{
    public int stack;

    public UnityAction onAdd;
    public UnityAction onRemove;

    public virtual void OnAdd(PlayerObj playerObj)
    {
        playerObj.playerBuffObjs.Add(this);
    }

    public virtual void OnRemove(PlayerObj playerObj)
    {
        playerObj.playerBuffObjs.Remove(this);
    }
}

/// <summary>
/// 穿透
/// </summary>
public class Penetrate : PlayerBuffObj
{
    public PlayerObj playerObj;

    public override void OnAdd(PlayerObj playerObj)
    {
        base.OnAdd(playerObj);
        this.playerObj = playerObj;
        playerObj.onAttack += PenetrateDamage;
    }
    public override void OnRemove(PlayerObj playerObj)
    {
        playerObj.onAttack -= PenetrateDamage;
        base.OnRemove(playerObj);
    }

    public void PenetrateDamage(AttackObj attackObj)
    {
        attackObj.attackType = E_AttackType.Penetrate;

        stack--;
        if(stack <= 0)
        {
            playerObj.RemoveBuff(this);
        }
    }
}

/// <summary>
/// 护盾
/// </summary>
public class Shield : PlayerBuffObj
{

    public override void OnAdd(PlayerObj playerObj)
    {
        base.OnAdd(playerObj);
    }
    public override void OnRemove(PlayerObj playerObj)
    {


        base.OnRemove(playerObj);
    }

    public void ShieldDamage(AttackObj attackObj)
    {
    }
}

public class Mask : PlayerBuffObj
{
    public int defenseTime = 1;
    public override void OnAdd(PlayerObj playerObj)
    {
        base.OnAdd(playerObj);
        playerObj.onGetCurse += DefenseCurse;
        BattleController.Instance.roundStart += ResetDefenseTime;
    }

    private void ResetDefenseTime()
    {
        defenseTime = 1;
    }

    public override void OnRemove(PlayerObj playerObj)
    {
        playerObj.onGetCurse -= DefenseCurse;
        BattleController.Instance.roundStart -= ResetDefenseTime;
        base.OnRemove(playerObj);
    }

    public void DefenseCurse(CardObj cardObj)
    {
        if(defenseTime > 0)
        {
            defenseTime--;
            cardObj.cardType = cfg.E_CardType.None;
        }
    }
}
