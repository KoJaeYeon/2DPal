using System.Collections;
using System.Collections.Generic;
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
    }
}
