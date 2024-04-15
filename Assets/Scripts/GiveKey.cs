using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveKey : MonoBehaviour
{
    public Slot[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].key = i;
            slots[i].gameObject.SetActive(false);
        }
    }
}
