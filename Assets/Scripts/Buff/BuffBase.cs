using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None,
    Bleed,
}

/// <summary>
/// Buff，如流血、穿透、创伤
/// </summary>
public class BuffBase
{
    public int value;
    public BuffType type = BuffType.None;
    public bool isCompelete = false;
}

//流血
public class Bleed : BuffBase
{
    public Bleed(int num)
    {
        //层数
        value = num;
        type = BuffType.Bleed;
    }
}

