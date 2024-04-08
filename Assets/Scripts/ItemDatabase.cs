using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public Dictionary<int,Item> items = new Dictionary<int, Item>();

    public Sprite[] sprites;
    private void Awake()
    {
        int index = 0;
        int id = 1; Item item = new Item("Tree", id, 40, 3); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("Stone", id, 40, 3); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("Iron", id, 40, 8); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("Paldium Fragment", id, 40, 1); item.sprite = sprites[index++]; items.Add(id, item);
    }
}
