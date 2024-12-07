using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface ICardEffect
{
    public abstract bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target);
}

public class SwordEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        AttackObj attackObj = new AttackObj();
        attackObj.damage = cardObj.value;
        attackObj.attackType = E_AttackType.Normal;
        attackObj.enemyObj = target;
        //造成伤害
        user.Attack(attackObj);
        return true;
    }
}

/// <summary>
/// 小刀
/// </summary>
public class KnifeEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        Bleed bleedBuff = new Bleed();
        bleedBuff.stack = cardObj.value;

        BuffMgr.Instance.GiveBuff(bleedBuff, target);
        return true;
    }
}

public class MaskEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

public class ScissorsEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        MonoMgr.Instance.StartCoroutine(ActivelyFold(cardObj));
        return true;
    }
    private IEnumerator ActivelyFold(CardObj cardObj)
    {
        yield return CoroutineTool.WaitForFrame();
        BattleController.Instance.ActivelyFold(cardObj.value);
    }
}

public class EyesEffect : ICardEffect
{
    //查看牌库的3张牌，以任意顺序置于牌库顶
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

/// <summary>
/// 长毛，立即获得“穿透”
/// </summary>
public class SpearEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        Penetrate penetrate = new Penetrate();

        penetrate.stack = 1;
        //penetrate.stack = cardObj.value;

        Debugger.LogMagenta("获得穿透Buff");
        user.AddBuff(penetrate);
        return true;
    }
}

public class AppleEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

public class WolfToothEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        Trauma trauma = new Trauma();
        trauma.stack = cardObj.value;
        BuffMgr.Instance.GiveBuff(trauma, target);
        return true;
    }
}

public class GobletEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

public class PoisonEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

public class PenEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

public class RivetEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        return false;
    }
}

public class ScrollEffect : ICardEffect
{
    public bool ExecuteEffect(CardObj cardObj, PlayerObj user, EnemyObj target)
    {
        BattleController.Instance.DrawCard();
        Debugger.LogMagenta("使用了卷轴");
        return true;
    }
}
