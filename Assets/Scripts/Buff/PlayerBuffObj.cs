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
