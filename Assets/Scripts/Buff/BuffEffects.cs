using System;

/// <summary>
/// 流血
/// </summary>
public class Bleed : BuffObj
{
    public Bleed()
    {
        buff = BuffMgr.Instance.GetBuff(cfg.E_BuffType.Bleed);
    }

    public override void OnAddStack(int stack)
    {
        base.OnAddStack(stack);
        Debugger.LogRed($"当前流血层数{this.stack}");
    }

    public override void OnAdd(EnemyObj enemyObj)
    {
        base.OnAdd(enemyObj);
        base.enemyObj.onReadyHurt += ExDamage;
        Debugger.LogRed($"施加流血Buff，当前流血层数{stack}");
    }
    public override void OnRemove(EnemyObj enemyObj)
    {
        base.enemyObj.onReadyHurt -= ExDamage;
        base.OnRemove(enemyObj);
    }
    public void ExDamage(AttackObj attackObj)
    {
        enemyObj.readyDamage += stack--;
        Debugger.LogOrange($"追加伤害{stack + 1},当前总伤害{enemyObj.readyDamage}");
        Debugger.LogRed($"当前流血层数{stack}");

        if(stack <= 0)
        {
            enemyObj.RemoveBuff(this);
            Debugger.LogPink("流血层数用完了");
        }
    }
}

/// <summary>
/// 创伤
/// </summary>
public class Trauma : BuffObj
{
    public Trauma()
    {
        buff = BuffMgr.Instance.GetBuff(cfg.E_BuffType.Trauma);
    }

    public override void OnAddStack(int stack)
    {
        base.OnAddStack(stack);
        Debugger.LogRed($"当前创伤层数{this.stack}");
    }

    public override void OnAdd(EnemyObj enemyObj)
    {
        base.OnAdd(enemyObj);
        enemyObj.onActionEnd += TraumaDamage;
        Debugger.LogRed($"施加创伤Buff，当前创伤层数{stack}");
    }
    public override void OnRemove(EnemyObj enemyObj)
    {
        enemyObj.onActionEnd -= TraumaDamage;
        base.OnRemove(enemyObj);
    }
    private void TraumaDamage(EnemyObj enemyObj)
    {
        enemyObj.Hurt(stack);
        stack--;
        Debugger.LogRed($"触发创伤Buff，造成伤害{stack + 1}，剩余层数{stack}");
        if(stack <= 0)
        {
            enemyObj.RemoveBuff(this);
            Debugger.LogPink("创伤层数用完了");
        }
    }
}

/// <summary>
/// 乐不思蜀
/// </summary>
public class SkipAction : BuffObj
{
    public SkipAction()
    {
        buff = BuffMgr.Instance.GetBuff(cfg.E_BuffType.SkipAction);
    }

    public override void OnAddStack(int stack)
    {
        base.OnAddStack(stack);
    }

    public override void OnAdd(EnemyObj enemyObj)
    {
        base.OnAdd(enemyObj);
    }

    public override void OnRemove(EnemyObj enemyObj)
    {
        base.OnRemove(enemyObj);
    }
}

public class Poison : BuffObj
{
    public Poison()
    {
        buff = BuffMgr.Instance.GetBuff(cfg.E_BuffType.Poison);
    }

    public override void OnAddStack(int stack)
    {
        base.OnAddStack(stack);
        Debugger.LogRed($"当前中毒层数{this.stack}");
    }

    public override void OnAdd(EnemyObj enemyObj)
    {
        base.OnAdd(enemyObj);
        enemyObj.onActionEnd += PoisonDamage;
        Debugger.LogRed($"施加中毒Buff，当前中毒层数{stack}");
    }
    public override void OnRemove(EnemyObj enemyObj)
    {
        enemyObj.onActionEnd -= PoisonDamage;
        base.OnRemove(enemyObj);
    }
    private void PoisonDamage(EnemyObj enemyObj)
    {
        enemyObj.Hurt(stack);
        stack--;
        Debugger.LogRed($"触发中毒Buff，造成伤害{stack + 1}，剩余层数{stack}");
        if (stack <= 0)
        {
            enemyObj.RemoveBuff(this);
            Debugger.LogPink("中毒层数用完了");
        }
    }
}