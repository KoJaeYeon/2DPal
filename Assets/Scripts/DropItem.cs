using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public List<Item> items;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = sprite;
    }

    public void DropItems()
    {
        int length = items.Count;
        for (int i = 0; i < length; i++)
        {
            InventoryManager.Instance.DropItem(items[i]);
        }
    }
}
