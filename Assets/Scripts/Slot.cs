using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot : MonoBehaviour
{
    public Item item;
    public Image image;
    public int key;
    public TextMeshProUGUI[] itemData = new TextMeshProUGUI[2]; //0 : Count, 1 : Weight

    bool dragOn;
    protected void Awake()
    {
        item = null;
        image = GetComponent<Image>();
        itemData = GetComponentsInChildren<TextMeshProUGUI>();
    }

    #region SwapPoint
    public void DragOn()
    {
        SetSlot();
    }
    public void PointerClick()
    {
        if (InventoryManager.Instance.tempSlot.gameObject.activeSelf) // 잡고있는게 있으면
        {
            if(InventoryManager.Instance.startSlotkey == key) // 원래 위치랑 같을 때
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // 절반으로 나누기
                {
                    if(InventoryManager.Instance.tempSlot.item.count > 1)
                    {
                        InventoryManager.Instance.tempSlot.item.count /= 2;
                        InventoryManager.Instance.tempSlot.UpdateSlot();
                    }

                }
                else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) // 1개씩 추가
                {
                    InventoryManager.Instance.tempSlot.item.count += InventoryManager.Instance.tempSlot.item.count < item.count ? 1 : 0;
                    InventoryManager.Instance.tempSlot.UpdateSlot();
                }
                else // 그자리에 놓기
                {
                    Debug.Log("else1");
                    InventoryManager.Instance.tempSlot.gameObject.SetActive(false);
                }
            }
            else // 다른곳에 놓았을 때
            {
                Debug.Log("else2");
                EndDrag();                
            }            
        }
        else
        {
            SetSlot();
        }

    }

    void SetSlot()
    {
        if (item == null){ Debug.Log("return"); return; } // 빈칸이 시작지점일 때
        InventoryManager.Instance.startSlotkey = this.key;
        InventoryManager.Instance.tempSlot.gameObject.SetActive(true);
        InventoryManager.Instance.tempSlot.item = new Item(item);
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) InventoryManager.Instance.tempSlot.item.count /= 2;
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) InventoryManager.Instance.tempSlot.item.count = 1;
        InventoryManager.Instance.tempSlot.UpdateSlot();
    }

    public void EndDrag()
    {
        if (!InventoryManager.Instance.tempSlot.gameObject.activeSelf) return; // Temp 슬롯이 비 활성화일 때(인벤토리 꺼졌을때 드래그 유지 중)
        InventoryManager.Instance.tempSlot.gameObject.SetActive(false);
        if (InventoryManager.Instance.endSlotkey == -1) return;// 빈 공간에 놓았을 때
        InventoryManager.Instance.SwapSlot();
    }
    public void EnterMouse()
    {
        InventoryManager.Instance.endSlotkey = key;
    }
    public void ExitMouse()
    {
        InventoryManager.Instance.endSlotkey = -1;
    }
    #endregion
    public void UpdateSlot(Item item)
    {        
        if (item == null || item.count == 0)
        {
            RemoveSlot();
        }
        else
        {
            this.item = item;
            image.sprite = item.sprite;
            image.gameObject.SetActive(true);
            itemData[0].text = item.count.ToString();
            itemData[1].text = (item.count * item.weight).ToString();
        }

    }

    public void UpdateSlot()
    {
        UpdateSlot(item);
    }

    public void RemoveSlot()
    {
        item = null;
        image.gameObject.SetActive(false);
        if (InventoryManager.Instance.inventory[0].ContainsKey(key)) InventoryManager.Instance.inventory[0].Remove(key);
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Slot other = obj as Slot;
        if (other.key == key) return true;
        else return false;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
