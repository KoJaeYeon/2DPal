using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tilemap;
    public Item item;

    [SerializeField] int id;

    public float maxDuration = 250;
    public float duration = 250;

    public int count;
    public int givedItem = 0;
    public int leftItem;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        item = ItemDatabase.Instance.items[id];
        count = item.count;
        leftItem = count;
    }

    public Item Attack(float damage)
    {
        duration -= damage;
        Item giveItem = new Item(item);
        if (duration < 0) giveItem.count = leftItem; //ÆÄ±«µÇ¸é ³²Àº °¹¼ö ¸®ÅÏ
        else
        {
            float togiveNum = (((maxDuration - duration) / maxDuration) * count);// ÃÑ Áà¾ß ÇÒ °¹¼ö
            if ( togiveNum > givedItem) 
            {
                giveItem.count = (int)(togiveNum - givedItem);
                leftItem -= (int)(togiveNum - givedItem);
                givedItem += (int)(togiveNum - givedItem);
            }
            else
            {
                giveItem.count = 0;
            }
        }
        return giveItem;
    }

    public bool DestroyTile(Vector3 pos)
    {
        Vector3Int cellPostion = tilemap.WorldToCell(pos);
        if (tilemap.GetTile(cellPostion) == null) return false;
        tilemap.SetTile(cellPostion,null);
        duration = maxDuration;
        leftItem = count;
        givedItem = 0;
        return true;
    }

}
