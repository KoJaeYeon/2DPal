using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : Product
{
    public int personalId;
    static int personalIndex = 0;
    public Equip(string itemName, int _id, int count, float weight, float lavor, string description = "", int[,] ingredients = null) : base(itemName, _id, count, weight, lavor, description, ingredients)
    {
        personalId = personalIndex++;
    }

    public Equip(Equip equip) : base(equip)
    {

    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if(!base.Equals(obj)) return false;
        Equip other = obj as Equip;
        if (other.personalId == personalId) return true;
        else return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
