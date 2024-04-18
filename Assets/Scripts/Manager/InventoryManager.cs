using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject UIInventorySlot;
    public GameObject chestInventorySlot;
    public GameObject chestBoxSlot;
    public GameObject UIEquipmentSlot;
    public GameObject FoodPanel;


    public TMPro.TextMeshProUGUI textMeshProUGUI; //�ӽ��Ǿ� ���� ǥ�� UI
    public Dictionary<int,Item> inventory = new Dictionary<int,Item>();
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
        inventory = new Dictionary<int, Item>();
        // ����� �����Ϳ��� �κ��丮 �޾ƿ���
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(inventory[2] is Equip);
        }
    }

    private void Start()
    {
        DropItem(new Item("sdf",1000,5,0));
        Equip equip = new Equip("���� ���", 501, 1, 10, 30, "���� ������ ���� ���.\n�Ӱ� �ο�⿣ �� �Ҿ��ϴ�.", new int[,] { { 1, 5 } }); equip.sprite = ItemDatabase.Instance.sprites_equip[0];
        DropItem(equip);
    }

    public int CheckWeapon()
    {
        for(int i = 21; i <= 25; i++)
        {
            if (!inventory.ContainsKey(i)) return i;
        }
        return -1;
    }

    public int CheckNull()
    {
        for (int i = 0; i < 21; i++)
        {
            if (!inventory.ContainsKey(i)) return i;
        }
        return -1;
    }


    void UpdateSlot(int i,Item item)
    {
        if(i < 21)
        {
            Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
            DebugItem = item;
            slot.UpdateSlot(item);
            Slot_Chest slot2 = chestInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot_Chest>();
            slot2.UpdateSlot(item);
        }
        else if(i < 25)
        {
            Slot slot = UIEquipmentSlot.transform.GetChild(i - 21).GetChild(0).GetComponent<Slot>();
            slot.UpdateSlot(item);
            //���� ��ü
            if(slot.item == null) WeaponManager.Instance.Equip(500, i);
            else WeaponManager.Instance.Equip(slot.item.id, i);
        }
        else if (i >= 50)
        {
            Slot_Chest slot2 = chestBoxSlot.transform.GetChild(i%50).GetChild(0).GetComponent<Slot_Chest>();
            slot2.UpdateSlot(item);
        }

    }
    void UpdateSlot(int i)
    {
        if(i < 21)
        {
            Slot slot = UIInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot>();
            slot.UpdateSlot();
            Slot_Chest slot2 = chestInventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Slot_Chest>();
            slot2.UpdateSlot();
        }
        else if (i < 25)
        {
            Slot slot = UIEquipmentSlot.transform.GetChild(i - 21).GetChild(0).GetComponent<Slot>();
            slot.UpdateSlot();
            //���� ��ü
            if (slot.item == null) WeaponManager.Instance.Equip(500, i);
            else WeaponManager.Instance.Equip(slot.item.id, i);
        }
        else if (i >= 50)
        {
            Slot_Chest slot2 = chestBoxSlot.transform.GetChild(i%50).GetChild(0).GetComponent<Slot_Chest>();
            slot2.UpdateSlot();
        }

    }
    public void SwapSlot()
    {
        if(21 <= endSlotkey && endSlotkey < 25)
        {
            if (!(tempSlot.item is Equip)) return;
        }
        if (!inventory.ContainsKey(endSlotkey)) // �� �����̸�
        {
            Debug.Log("case1");
            inventory[startSlotkey].Substarct(tempSlot.item);
            inventory.Add(endSlotkey,tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[endSlotkey]);
        }
        else if (tempSlot.item.Equals(inventory[endSlotkey]) && !(tempSlot.item is Equip)) // ���� ������ �������̰� ��� �ƴϸ�
        {
            Debug.Log("case2");
            inventory[startSlotkey].Substarct(tempSlot.item);
            inventory[endSlotkey].Add(tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[endSlotkey]);
        }
        else // �ٸ������� �������̰ų� ����̸�
        {
            if (inventory[startSlotkey].count != tempSlot.item.count) // �ٲٱⰡ �ƴ϶� ������ �޶������� ���
            {
                Debug.Log("case3");
                return;
            }
            else // �ڸ� �ٲٱ�
            {
                Debug.Log("case4");
                inventory[startSlotkey] = inventory[endSlotkey];
                inventory[endSlotkey] = tempSlot.item;
                UpdateSlot(startSlotkey, inventory[startSlotkey]);
                UpdateSlot(endSlotkey, inventory[endSlotkey]);
            }

        }
    }

    public void SwapSlot2()
    {
        if (!inventory.ContainsKey(endSlotkey)) // �� �����̸�
        {
            Debug.Log("case1");
            inventory[startSlotkey].Substarct(tempSlot2.item);
            inventory.Add(endSlotkey, tempSlot2.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[endSlotkey]);
        }
        else if (tempSlot2.item.Equals(inventory[endSlotkey])) // ���� ������ �������̸�
        {
            Debug.Log("case2");
            inventory[startSlotkey].Substarct(tempSlot2.item);
            inventory[endSlotkey].Add(tempSlot2.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[endSlotkey]);
        }
        else // �ٸ������� �������̸�
        {
            if (inventory[startSlotkey].count != tempSlot2.item.count) // �ٲٱⰡ �ƴ϶� ������ �޶������� ���
            {
                Debug.Log("case3");
                return;
            }
            else // �ڸ� �ٲٱ�
            {
                Debug.Log("case4");
                inventory[startSlotkey] = inventory[endSlotkey];
                inventory[endSlotkey] = tempSlot2.item;
                UpdateSlot(startSlotkey, inventory[startSlotkey]);
                UpdateSlot(endSlotkey, inventory[endSlotkey]);
            }

        }
    }

    public void DropItem(Item item)
    {
        if (item.id == 1000) sphereCount += item.count;
        Debugint = sphereCount;
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
        bool alreadyHas = false;
        int index = 0;
        for (int i = 0; i < maxInvetory; i++)
        {
            if(inventory.ContainsKey(i) && inventory[i].Equals(item)) {  alreadyHas = true; index = i; break; }
        }
        if (item is Equip) alreadyHas = false;
        if (alreadyHas) //�κ��丮�� �������� �̹� �����ϴ� ���
        {
            inventory[index].Add(item);
            inventorySum[item.id] += item.count;
            playerWeight += item.count * item.weight;
            weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
            UpdateSlot(index, inventory[index]);
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
                    weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
                    UpdateSlot(i, item);
                    break;
                }
            }
        }
    }

    public void UseItem(Item item)
    {
        if (item.id == 1000) sphereCount -= item.count;
        Debugint = sphereCount;
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
        while (item.count > 0)
        {
            if (inventory.ContainsValue(item))
            {
                //int[] keysArray = new int[inventory.Count];
                //int keyIndex = 0;
                //foreach (int i in inventory.Keys)
                //{
                //    keysArray[keyIndex++] = i;
                //}
                for (int i = 0; i < maxInvetory; i++)
                {
                    if (!inventory.ContainsKey(i)) continue;
                    if (inventory[i].Equals(item))
                    {
                        if (inventory[i].count >= item.count) // �κ��丮 ������ ������
                        {
                            playerWeight -= item.count * item.weight;
                            weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
                            inventory[i].Substarct(item);
                            inventorySum[item.id] -= item.count;
                            item.count = 0;
                            UpdateSlot(i, inventory[i]);
                            return;
                        }
                        else // �κ��丮 ������ ������
                        {
                            playerWeight -= item.count * item.weight;
                            weihgtPanel.UpdateWeight(playerWeight, maxPlayerWeight);
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
    public void UseItem(int id)
    {
        Item item = new Item("", id, 1, 0);
        UseItem(item);
    }
}
