using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture
{
    [SerializeField] string itemName;
    [SerializeField] int id;
    [SerializeField] Sprite _sprite;
    [SerializeField] int[,] _buildingItems;

    public Furniture(string itemName, int id, int[,] buildingItem)
    {
        this.itemName = itemName;
        this.id = id;
    }

    public Furniture(Furniture furniture)
    {
        this.itemName = furniture.itemName;
        this.id = furniture.id;
        _sprite = furniture._sprite;
        _buildingItems = furniture._buildingItems;
    }

    public Sprite sprite
    {
        get => _sprite;
        set => _sprite = value;
    }

    public int[,] buildingItems
    {
        get => _buildingItems;
        set => _buildingItems = value;
    }
}