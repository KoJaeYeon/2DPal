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
        Debug.Log(slot);
        DebugItem = item;
        slot.UpdateSlot(item);
    }

    public void DropItem(Item item)
    {
        if(inventory.ContainsValue(item))
        {
            Debug.Log("같은거");
            for(int i = 0; i < maxInvetory; i++)
            {
                if (inventory.ContainsKey(i) && inventory[i].Equals(item))
                {
                    Debug.Log("인벤토리" + i + "번째칸에" + item + "들어감");
                    inventory[i].Add(item);
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i,inventory[i]);
                }
            }
        }
        else
        {
            Debug.Log("다른거");
            for (int i = 0;i < maxInvetory; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    Debug.Log("인벤토리"+i + "번째칸에" + item +"들어감");
                    inventory.Add(i, item);
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, item);
                    break;
                }
            }
        }
    }
}
