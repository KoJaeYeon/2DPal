using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveKey_Pal : MonoBehaviour
{
    public Slot_Pal[] slots;
    public int startKey = 0;

    private void Start()
    {
        Debug.Log("sdf");
        slots = GetComponentsInChildren<Slot_Pal>();
        for(int i = startKey; i < slots.Length + startKey; i++)
        {
            slots[i - startKey].key = i;
            slots[i- startKey].gameObject.SetActive(false);
        }
    }
}
