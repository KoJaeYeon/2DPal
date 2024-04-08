using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture
{
    public List<Item> buildingItems = new List<Item>();

    [SerializeField] string itemName;
    [SerializeField] int id;
    [SerializeField] Sprite _sprite;
    [SerializeField] GameObject GameObject;

    public Furniture(string itemName, int id, int[,] buildingItem)
    {
        this.itemName = itemName;
        this.id = id;
        for(int i = 0; i < buildingItem.GetLength(0); i++)
        {
            Item item = new Item(ItemDatabase.Instance.items[buildingItem[i,0]]);
            item.count = buildingItem[i, 1];
            buildingItems.Add(item);
        }
    }

    public Furniture(Furniture furniture)
    {
        this.itemName = furniture.itemName;
        this.id = furniture.id;
        _sprite = furniture._sprite;
        buildingItems = furniture.buildingItems;
    }

    public Sprite sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
}