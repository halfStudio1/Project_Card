using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 玩家和怪物的基类
/// </summary>
public class ActorObj
{
    //名字
    public string name = "";

    public int health = 100;

    //攻击
    public UnityAction onAttack;
    //受伤
    public UnityAction onHurt;
    //死亡
    public UnityAction onDeath;
}
