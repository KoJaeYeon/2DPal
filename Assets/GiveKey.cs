using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveKey : MonoBehaviour
{
    Slot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].key = i;
        }
    }
}
