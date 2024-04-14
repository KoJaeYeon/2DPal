using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject UIInventorySlot;
    
    public TMPro.TextMeshProUGUI textMeshProUGUI;
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    private int maxInvetory = 20;
    public float playerWeight;

    public static Dictionary<int,int> inventorySum = new Dictionary<int,int>();
    public static int sphereCount = 0;
    public int Debugint = 0;

    public Item DebugItem;

    public int startSlotkey;
    public int endSlotkey;

    public Slot tempSlot;

    private void Awake()
    {
        inventory.Clear();
        // ����� �����Ϳ��� �κ��丮 �޾ƿ���

        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
    }

    private void Start()
    {
        //DropItem(new Item("sdf",1001,5,0));
    }

    void UpdateSlot(int i,Item item)
    {
        Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
        DebugItem = item;
        slot.UpdateSlot(item);
    }
    void UpdateSlot(int i)
    {
        Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
        slot.UpdateSlot();
    }




    public void SwapSlot()
    {
        Debug.Log("SwapSlot");
        if (!inventory.ContainsKey(endSlotkey)) // �� �����̸�
        {
            inventory[startSlotkey].Substarct(tempSlot.item);
            inventory.Add(endSlotkey,tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[endSlotkey]);
            Debug.Log("case1");
        }
        else if (tempSlot.item.Equals(inventory[endSlotkey])) // ���� ������ �������̸�
        {
            inventory[startSlotkey].Substarct(tempSlot.item);
            inventory[endSlotkey].Add(tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[endSlotkey]);
            Debug.Log("case2");
        }
        else // �ٸ������� �������̸�
        {
            if (inventory[startSlotkey].count != tempSlot.item.count) // �ٲٱⰡ �ƴ϶� ������ �޶������� ���
            {
                Debug.Log("case3");
                return;
            }
            else // �ڸ� �ٲٱ�
            {
                inventory[startSlotkey] = inventory[endSlotkey];
                inventory[endSlotkey] = tempSlot.item;
                UpdateSlot(startSlotkey, inventory[startSlotkey]);
                UpdateSlot(endSlotkey, inventory[endSlotkey]);
                Debug.Log("case4");
            }

        }
    }

    public void DropItem(Item item)
    {
        if (item.id == 1001) sphereCount += item.count;
        Debugint = sphereCount;
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
        if (inventory.ContainsValue(item)) //�κ��丮�� �������� �̹� �����ϴ� ���
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
        if (item.id == 1001) sphereCount -= item.count;
        Debugint = sphereCount;
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
        while (item.count > 0)
        {
            if (inventory.ContainsValue(item))
            {
                int[] keysArray = new int[inventory.Count];
                int keyIndex = 0;
                foreach (int i in inventory.Keys)
                {
                    keysArray[keyIndex++] = i;
                }
                for (int i = 0; i < keysArray.Length; i++)
                {
                    if (inventory[keysArray[i]].Equals(item))
                    {
                        if (inventory[keysArray[i]].count >= item.count) // �κ��丮 ������ ������
                        {
                            inventory[keysArray[i]].Substarct(item);
                            inventorySum[item.id] -= item.count;
                            item.count = 0;
                            UpdateSlot(keysArray[i], inventory[keysArray[i]]);
                        }
                        else // �κ��丮 ������ ������
                        {
                            item.Substarct(inventory[keysArray[i]]);
                            inventorySum[item.id] -= inventory[keysArray[i]].count;
                            inventory[keysArray[i]].count = 0;
                            UpdateSlot(keysArray[i], null);
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
    public void UseItem(int id)
    {
        Item item = new Item("", id, 1, 0);
        UseItem(item);
    }
}
