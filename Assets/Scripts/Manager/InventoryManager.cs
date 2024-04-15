using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject UIInventorySlot;
    
    public TMPro.TextMeshProUGUI textMeshProUGUI; //팰스피어 갯수 표시 UI
    public List<Dictionary<int,Item>> inventory = new List<Dictionary<int,Item>>();
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
        inventory.Add(new Dictionary<int, Item>());
        // 저장된 데이터에서 인벤토리 받아오기
        textMeshProUGUI.text = GameManager.Instance.CountString(sphereCount);
    }

    private void Start()
    {
        DropItem(new Item("sdf",1001,5,0));
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
        if (!inventory[0].ContainsKey(endSlotkey)) // 빈 공간이면
        {
            inventory[0][startSlotkey].Substarct(tempSlot.item);
            inventory[0].Add(endSlotkey,tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
        }
        else if (tempSlot.item.Equals(inventory[0][endSlotkey])) // 같은 종류의 아이템이면
        {
            inventory[0][startSlotkey].Substarct(tempSlot.item);
            inventory[0][endSlotkey].Add(tempSlot.item);
            UpdateSlot(startSlotkey);
            UpdateSlot(endSlotkey, inventory[0][endSlotkey]);
        }
        else // 다른종류의 아이템이면
        {
            if (inventory[0][startSlotkey].count != tempSlot.item.count) // 바꾸기가 아니라 갯수가 달라졌으면 취소
            {
                return;
            }
            else // 자리 바꾸기
            {
                inventory[0][startSlotkey] = inventory[0][endSlotkey];
                inventory[0][endSlotkey] = tempSlot.item;
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
        if (inventory[0].ContainsValue(item)) //인벤토리에 아이템이 이미 존재하는 경우
        {
            foreach (int i in inventory[0].Keys)
            {
                if (inventory[0][i].Equals(item))
                {
                    inventory[0][i].Add(item);
                    inventorySum[item.id] += item.count;
                    playerWeight += item.count * item.weight;
                    UpdateSlot(i, inventory[0][i]);
                }
            }
        }    
        else // 인벤토리에 아이템이 없는 경우
        {
            for (int i = 0;i < maxInvetory; i++)
            {
                if (!inventory[0].ContainsKey(i))
                {
                    inventory[0].Add(i, item);
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
                        if (inventory[0][keysArray[i]].count >= item.count) // 인벤토리 총합이 많을때
                        {
                            inventory[0][keysArray[i]].Substarct(item);
                            inventorySum[item.id] -= item.count;
                            item.count = 0;
                            UpdateSlot(keysArray[i], inventory[0][keysArray[i]]);
                        }
                        else // 인벤토리 총합이 적을때
                        {
                            item.Substarct(inventory[0][keysArray[i]]);
                            inventorySum[item.id] -= inventory[0][keysArray[i]].count;
                            inventory[0][keysArray[i]].count = 0;
                            UpdateSlot(keysArray[i], null);
                        }
                    }
                }
            }
            else if( false) //창고 확인
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
