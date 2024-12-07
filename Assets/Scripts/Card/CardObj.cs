using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObj
{
    public int id;

    public string name;

    public string info;

    public E_CardType cardType;

    public E_CardFace cardFace;

    public int point;

    public string spriteName;

    public int value;

    public CardObj(int id, string name, string spriteName, string info, E_CardType type, E_CardFace cardFace, int point, int value)
    {
        this.id = id;
        this.name = name;
        this.info = info;
        this.cardType = type;
        this.cardFace = cardFace;
        this.point = point;
        this.spriteName = spriteName;
        this.value = value;
    }
}
