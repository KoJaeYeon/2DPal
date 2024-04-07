using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    private int maxInvetory = 20;
    private void Awake()
    {
        inventory.Clear();
        // ����� �����Ϳ��� �κ��丮 �޾ƿ���
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
                    break;
                }
            }
        }
    }
}
