using UnityEngine;

public class GiveKey_Chest : MonoBehaviour
{
    public Slot_Chest[] slots;
    public int startKey = 0;

    private void Start()
    {
        slots = GetComponentsInChildren<Slot_Chest>();
        for(int i = startKey; i < slots.Length + startKey; i++)
        {
            slots[i - startKey].key = i;
            slots[i- startKey].gameObject.SetActive(false);
        }

        if(startKey == 21)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void SetSlotVolume(int volume, int key)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.parent.gameObject.SetActive(false);
            slots[i].gameObject.SetActive(false);
        }
        for(int i = key; i < volume + key; i++)
        {
            slots[i - key].transform.parent.gameObject.SetActive(true);
            slots[i - key].key = i;            
            if (InventoryManager.Instance.inventory.ContainsKey(i))
            {
                slots[i - key].gameObject.SetActive(true);
                slots[i - key].UpdateSlot(InventoryManager.Instance.inventory[i]);
            }
        }
    }
}
