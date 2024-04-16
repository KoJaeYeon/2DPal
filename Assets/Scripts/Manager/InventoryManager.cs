using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject UIInventorySlot;
    public GameObject chestInventorySlot;


    public TMPro.TextMeshProUGUI textMeshProUGUI; //�ӽ��Ǿ� ���� ǥ�� UI
    public List<Dictionary<int,Item>> inventory = new List<Dictionary<int,Item>>();
    private int maxInvetory = 21;
    public float playerWeight = 0;
    public float maxPlayerWeight = 300;
    public WeihgtPanel weihgtPanel;

    public static Dictionary<int,int> inventorySum = new Dictionary<int,int>();
    public static int sphereCount = 0;
    public int Debugint = 0;

    public Item DebugItem;

    public int startSlotkey;
    public int endSlotkey;

    public Slot tempSlot;
    public Slot_Chest tempSlot2;

    private void Awake()
    {
        inventory.Add(new Dictionary<int, Item>());
        // ����� �����Ϳ��� �κ��丮 �޾ƿ���
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
    }

    private void Start()
    {
        DropItem(new Item("sdf",1001,5,0));
    }

    void UpdateSlot(int i,Item item)
    {
        if(i < 20)
        {
            Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
            DebugItem = item;
            slot.UpdateSlot(item);
            Slot_Chest slot2 = chestInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot_Chest>();
            slot2.UpdateSlot(item);
        }

    }
    void UpdateSlot(int i)
    {
        if(i < 20)
        {
            Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
            slot.UpdateSlot();
            Slot_Chest slot2 = chestInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot_Chest>();
            slot2.UpdateSlot();
        }

    }
    public void SwapSlot()
    {
        if (!inventory[0].ContainsKey(endSlotkey)) // �� �����̸�
        {
            Debug.Log("case1");
            inventory[0][startSlotkey].Substarct(tempSlot.item);
            inventory[0].Add(endSlotkey,tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
        }
        else if (tempSlot.item.Equals(inventory[0][endSlotkey])) // ���� ������ �������̸�
        {
            Debug.Log("case2");
            inventory[0][startSlotkey].Substarct(tempSlot.item);
            inventory[0][endSlotkey].Add(tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
        }
        else // �ٸ������� �������̸�
        {
            if (inventory[0][startSlotkey].count != tempSlot.item.count) // �ٲٱⰡ �ƴ϶� ������ �޶������� ���
            {
                Debug.Log("case3");
                return;
            }
            else // �ڸ� �ٲٱ�
            {
                Debug.Log("case4");
                inventory[0][startSlotkey] = inventory[0][endSlotkey];
                inventory[0][endSlotkey] = tempSlot.item;
                UpdateSlot(startSlotkey, inventory[0][startSlotkey]);
                UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
            }

        }
    }

    public void SwapSlot2()
    {
        if (!inventory[0].ContainsKey(endSlotkey)) // �� �����̸�
        {
            Debug.Log("case1");
            inventory[0][startSlotkey].Substarct(tempSlot2.item);
            inventory[0].Add(endSlotkey, tempSlot2.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
        }
        else if (tempSlot2.item.Equals(inventory[0][endSlotkey])) // ���� ������ �������̸�
        {
            Debug.Log("case2");
            inventory[0][startSlotkey].Substarct(tempSlot2.item);
            inventory[0][endSlotkey].Add(tempSlot2.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
        }
        else // �ٸ������� �������̸�
        {
            if (inventory[0][startSlotkey].count != tempSlot2.item.count) // �ٲٱⰡ �ƴ϶� ������ �޶������� ���
            {
                Debug.Log("case3");
                return;
            }
            else // �ڸ� �ٲٱ�
            {
                Debug.Log("case4");
                inventory[0][startSlotkey] = inventory[0][endSlotkey];
                inventory[0][endSlotkey] = tempSlot2.item;
                UpdateSlot(startSlotkey, inventory[0][startSlotkey]);
                UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
            }

        }
    }

    public void DropItem(Item item)
    {
        if (item.id == 1001) sphereCount += item.count;
        Debugint = sphereCount;
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
        if (inventory[0].ContainsValue(item)) //�κ��丮�� �������� �̹� �����ϴ� ���
        {
            foreach (int i in inventory[0].Keys)
            {
                if (inventory[0][i].Equals(item))
                {
                    inventory[0][i].Add(item);
                    inventorySum[item.id] += item.count;
                    playerWeight += item.count * item.weight;
                    weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
                    UpdateSlot(i, inventory[0][i]);
                }
            }
        }    
        else // �κ��丮�� �������� ���� ���
        {
            for (int i = 0;i < maxInvetory; i++)
            {
                if (!inventory[0].ContainsKey(i))
                {
                    inventory[0].Add(i, item);
                    if(!inventorySum.TryAdd(item.id,item.count)) inventorySum[item.id] += item.count;
                    playerWeight += item.count * item.weight;
                    weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
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
            if (inventory[0].ContainsValue(item))
            {
                int[] keysArray = new int[inventory[0].Count];
                int keyIndex = 0;
                foreach (int i in inventory[0].Keys)
                {
                    keysArray[keyIndex++] = i;
                }
                for (int i = 0; i < keysArray.Length; i++)
                {
                    if (inventory[0][keysArray[i]].Equals(item))
                    {
                        if (inventory[0][keysArray[i]].count >= item.count) // �κ��丮 ������ ������
                        {
                            playerWeight -= item.count * item.weight;
                            weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
                            inventory[0][keysArray[i]].Substarct(item);
                            inventorySum[item.id] -= item.count;
                            item.count = 0;
                            UpdateSlot(keysArray[i], inventory[0][keysArray[i]]);
                        }
                        else // �κ��丮 ������ ������
                        {
                            playerWeight -= item.count * item.weight;
                            weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
                            item.Substarct(inventory[0][keysArray[i]]);
                            inventorySum[item.id] -= inventory[0][keysArray[i]].count;
                            inventory[0][keysArray[i]].count = 0;
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
