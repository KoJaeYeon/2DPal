using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum itemName
{
    Stone,
    Tree,
    Iron
}
public class Item
{
    [SerializeField] itemName name;
    [SerializeField] int id;
    [SerializeField] int _count;
    [SerializeField] float weight;

    public Item(itemName name, int id, int count, float weight)
    {
        this.name = name;
        this.id = id;
        this._count = count;
        this.weight = weight;
    }

    public Item(Item item)
    {
        this.name = item.name;
        this.id = item.id;
        this._count = item._count;
        this.weight = item.weight;
    }

    public int count
    {
        get => _count;
        set => _count = value;
    }

    public void Add(Item item)
    {
        _count += item._count;
    }

    public override string ToString()
    {
        return "¿Ã∏ß" + name.ToString() + ",id" + id.ToString() + ",count" + _count.ToString();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if(obj == null) return false;
        Item other = obj as Item;
        if (other.id == id) return true;
        else return false;
    }
}
