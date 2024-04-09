using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] string _itemName;
    [SerializeField] int _id;
    [SerializeField] int _count;
    [SerializeField] float _weight;
    [SerializeField] Sprite _sprite;

    public Item(string itemName, int _id, int count, float weight)
    {
        this._itemName = itemName;
        this._id = _id;
        this._count = count;
        this._weight = weight;
    }

    public Item(Item item)
    {
        this._itemName = item._itemName;
        this._id = item._id;
        this._count = item._count;
        this._weight = item._weight;
        _sprite = item._sprite;
    }

    public Sprite sprite
    {
        get => _sprite;
        set => _sprite = value;
    }

    public string itemName
    {
        get => _itemName;
        set => _itemName = value;
    }
    public int id
    {
        get => _id;
        set => _id = value;
    }
    public int count
    {
        get => _count;
        set => _count = value;
    }

    public float weight
    {
        get => _weight;
        set => _weight = value;
    }

    public void Add(Item item)
    {
        _count += item._count;
    }
    public void Substarct(Item item)
    {
        _count -= item._count;
    }

    public override string ToString()
    {
        return "¿Ã∏ß" + _itemName.ToString() + ",_id" + _id.ToString() + ",count" + _count.ToString();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if(obj == null) return false;
        Item other = obj as Item;
        if (other._id == _id) return true;
        else return false;
    }
}
