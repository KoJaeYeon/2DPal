using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject UIInventorySlot;
    
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    private int maxInvetory = 20;
    public float playerWeight;

    public Item DebugItem;
    private void Awake()
    {
        inventory.Clear();
        // 저장된 데이터에서 인벤토리 받아오기
    }

    void UpdateSlot(int i,Item item)
    {
        Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
        DebugItem = item;
        slot.UpdateSlot(item);
    }

    public void DropItem(Item item)
    {
        if(inventory.ContainsValue(item))
        {
            foreach (int i in inventory.Keys)
            {
                if (inventory[i].Equals(item))
                {
                    inventory[i].Add(item);
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, inventory[i]);
                }
            }
        }
    
        else
        {
            for (int i = 0;i < maxInvetory; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    inventory.Add(i, item);
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, item);
                    break;
                }
            }
        }
    }
}
