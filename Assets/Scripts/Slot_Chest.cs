using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot_Chest : MonoBehaviour
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
        if (InventoryManager.Instance.tempSlot2.gameObject.activeSelf) // ����ִ°� ������
        {
            if(InventoryManager.Instance.startSlotkey == key) // ���� ��ġ�� ���� ��
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // �������� ������
                {
                    if(InventoryManager.Instance.tempSlot2.item.count > 1)
                    {
                        InventoryManager.Instance.tempSlot2.item.count /= 2;
                        InventoryManager.Instance.tempSlot2.UpdateSlot();
                    }

                }
                else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) // 1���� �߰�
                {
                    InventoryManager.Instance.tempSlot2.item.count += InventoryManager.Instance.tempSlot2.item.count < item.count ? 1 : 0;
                    InventoryManager.Instance.tempSlot2.UpdateSlot();
                }
                else // ���ڸ��� ����
                {
                    Debug.Log("else1");
                    InventoryManager.Instance.tempSlot2.gameObject.SetActive(false);
                }
            }
            else // �ٸ����� ������ ��
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
        if (item == null){ Debug.Log("return"); return; } // ��ĭ�� ���������� ��
        InventoryManager.Instance.startSlotkey = this.key;
        InventoryManager.Instance.tempSlot2.gameObject.SetActive(true);
        InventoryManager.Instance.tempSlot2.item = new Item(item);
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) InventoryManager.Instance.tempSlot2.item.count /= 2;
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) InventoryManager.Instance.tempSlot2.item.count = 1;
        InventoryManager.Instance.tempSlot2.UpdateSlot();
    }

    public void EndDrag()
    {
        if (!InventoryManager.Instance.tempSlot2.gameObject.activeSelf) return; // Temp ������ �� Ȱ��ȭ�� ��(�κ��丮 �������� �巡�� ���� ��)
        InventoryManager.Instance.tempSlot2.gameObject.SetActive(false);
        if (InventoryManager.Instance.endSlotkey == -1) return;// �� ������ ������ ��
        InventoryManager.Instance.SwapSlot2();
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
        if (InventoryManager.Instance.inventory.ContainsKey(key)) InventoryManager.Instance.inventory.Remove(key);
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
