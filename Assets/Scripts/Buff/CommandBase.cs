using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CommandType
{
    None,
    Attack,
    Fold,
}

/// <summary>
/// 命令，如伤害、弃牌、
/// </summary>
public class CommandBase
{
    public int value;
    public E_CommandType type;
    public bool isCompelete = false;
}

//攻击
public class Attack : CommandBase
{
    public Attack(int damage)
    {
        value = damage;
        type = E_CommandType.Attack;
    }
}

//弃牌
public class Fold : CommandBase
{
    public Fold(int foldNum)
    {
        value = foldNum;
        type = E_CommandType.Fold;
    }
}
