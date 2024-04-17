using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : Product
{
    public Equip(string itemName, int _id, int count, float weight, float lavor, string description = "", int[,] ingredients = null) : base(itemName, _id, count, weight, lavor, description, ingredients)
    {

    }

    public Equip(Equip equip) : base(equip)
    {

    }

}
