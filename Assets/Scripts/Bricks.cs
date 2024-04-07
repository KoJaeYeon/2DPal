using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tilemap;
    public Item item;

    [SerializeField] itemName itemName;
    [SerializeField] int id;
    [SerializeField] int count = 40;
    [SerializeField] float weight;

    public float maxDuration = 250;
    public float duration = 250;

    public int givedItem = 0;
    public int leftItem;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        item = new Item(itemName, id, count, weight);
        leftItem = count;
    }

    public Item Attack(float damage)
    {
        duration -= damage;
        Item giveItem = new Item(item);
        if(duration)
        return item;
    }

    public bool DestroyTile(Vector3 pos)
    {
        Vector3Int cellPostion = tilemap.WorldToCell(pos);
        if (tilemap.GetTile(cellPostion) == null) return false;
        tilemap.SetTile(cellPostion,null);
        duration = maxDuration;
        return true;
    }

}
