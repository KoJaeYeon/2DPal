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
        int id = 1; Item item = new Item("나무", id, 40, 3, "건축물이나 아이템의 재료로 쓴다.\r\n주로 나무를 벌목하여 획득한다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("돌", id, 40, 3, "건축물이나 아이템의 재료로 쓴다.\r\n돌을 채굴하여 획득한다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("팰지움 파편", id, 40, 1); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("금속 광석", id, 40, 8); item.sprite = sprites[index++]; items.Add(id, item);
    }

    public Item GetItem(int id)
    {
        Item item = new Item(items[id]);
        return item;
    }
}
