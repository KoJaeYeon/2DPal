using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Furniture
{
    [SerializeField] string _itemName;
    [SerializeField] int id;
    [SerializeField] Sprite _sprite;
    [SerializeField] int[,] _buildingItems;

    public Furniture(string itemName, int id, int[,] buildingItem)
    {
        this._itemName = itemName;
        this.id = id;
        this._buildingItems = buildingItem;
    }

    public Furniture(Furniture furniture)
    {
        this._itemName = furniture._itemName;
        this.id = furniture.id;
        _sprite = furniture._sprite;
        _buildingItems = furniture._buildingItems;
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

    public int[,] buildingItems
    {
        get => _buildingItems;
        set => _buildingItems = value;
    }
}