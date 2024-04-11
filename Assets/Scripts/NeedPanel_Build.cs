using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NeedPanel_Build : MonoBehaviour
{
    public Item item;
    public Image image;
    public TextMeshProUGUI itemData; //Need Count


    private void OnDisable()
    {
        RemoveDataPanel();
    }
    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        itemData = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }
    public bool UpdateDataPanel(Item item, int needNum)
    {
        bool canBuild = true;
        this.item = item;
        image.sprite = item.sprite;
        gameObject.SetActive(true);
        bool nowNumIn = InventoryManager.inventorySum.ContainsKey(item.id);
        int nowNum = nowNumIn ? InventoryManager.inventorySum[item.id] : 0;
        itemData.text = nowNum+ "/" + needNum;
        if(nowNum < needNum)
        {
            itemData.color = Color.red;
            canBuild = false;
        }
        else
        {
            itemData.color = Color.white;
        }
        return canBuild;
    }

    public void RemoveDataPanel()
    {
        item = null;
        gameObject.SetActive(false);
    }
}
