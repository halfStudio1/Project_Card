using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int id;

    public string name;

    public string info;

    public E_CardType cardType;

    public int point;

    public string spriteName;

    public Card(int id, string name, string info, E_CardType type, int point, string spriteName)
    {
        this.id = id;
        this.name = name;
        this.info = info;
        this.cardType = type;
        this.point = point;
        this.spriteName = spriteName;
    }
}
