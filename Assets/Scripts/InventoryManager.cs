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
        // ����� �����Ϳ��� �κ��丮 �޾ƿ���
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
            Debug.Log("������");
            for(int i = 0; i < maxInvetory; i++)
            {
                if (inventory.ContainsKey(i) && inventory[i].Equals(item))
                {
                    Debug.Log("�κ��丮" + i + "��°ĭ��" + item + "��");
                    inventory[i].Add(item);
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i,inventory[i]);
                }
            }
        }
        else
        {
            Debug.Log("�ٸ���");
            for (int i = 0;i < maxInvetory; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    Debug.Log("�κ��丮"+i + "��°ĭ��" + item +"��");
                    inventory.Add(i, item);
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, item);
                    break;
                }
            }
        }
    }
}
