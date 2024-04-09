using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject UIInventorySlot;
    
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    private int maxInvetory = 20;
    public float playerWeight;

    public static Dictionary<int,int> inventorySum = new Dictionary<int,int>();


    public Item DebugItem;
    private void Awake()
    {
        inventory.Clear();
        // ����� �����Ϳ��� �κ��丮 �޾ƿ���
    }

    void UpdateSlot(int i,Item item)
    {
        Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
        DebugItem = item;
        slot.UpdateSlot(item);
    }

    public void DropItem(Item item)
    {
        if(inventory.ContainsValue(item)) //�κ��丮�� �������� �̹� �����ϴ� ���
        {
            foreach (int i in inventory.Keys)
            {
                if (inventory[i].Equals(item))
                {
                    inventory[i].Add(item);
                    inventorySum[item.id] += item.count;
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, inventory[i]);
                }
            }
        }    
        else // �κ��丮�� �������� ���� ���
        {
            for (int i = 0;i < maxInvetory; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    inventory.Add(i, item);
                    if(!inventorySum.TryAdd(item.id,item.count)) inventorySum[item.id] += item.count;
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, item);
                    break;
                }
            }
        }
    }

    public void UseItem(Item item)
    {
        while (item.count > 0)
        {
            if (inventory.ContainsValue(item))
            {
                foreach (int i in inventory.Keys)
                {
                    if (inventory[i].Equals(item))
                    {
                        if (inventory[i].count >= item.count) // �κ��丮 ������ ������
                        {
                            inventory[i].Substarct(item);
                            inventorySum[item.id] -= item.count;
                            item.count = 0;
                            UpdateSlot(i, inventory[i]);
                        }
                        else // �κ��丮 ������ ������
                        {
                            item.Substarct(inventory[i]);
                            inventorySum[item.id] -= inventory[i].count;
                            inventory[i].count = 0;
                            UpdateSlot(i, null);
                        }
                    }
                }
            }
            else if( false) //â�� Ȯ��
            {
                //for(int i = 0; i < Chect.Count; i++)
                //{

                //}
            }
        }

    }
}
