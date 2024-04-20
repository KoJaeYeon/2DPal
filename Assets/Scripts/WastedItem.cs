using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastedItem : MonoBehaviour
{
    public int id;

    private void Start()
    {
        GameObject item = ItemDatabase.Instance.giveitem(id);
        item.transform.position = transform.position;
        item.SetActive(false);
        item.SetActive(true);
        gameObject.SetActive(false);
    }
}
