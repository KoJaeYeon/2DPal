using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NeedPanel : MonoBehaviour
{
    public Item item;
    public Image image;
    public TextMeshProUGUI[] itemData = new TextMeshProUGUI[2]; //0 : Item Name, 1 : Need Count

    private void Start()
    {
        image = transform.GetChild(1).GetComponent<Image>();
        itemData = GetComponentsInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }
    public bool UpdateDataPanel(Item item, int needNum)
    {
        bool canBuild = true;
        this.item = item;
        image.sprite = item.sprite;
        gameObject.SetActive(true);
        itemData[0].text = item.itemName;
        bool nowNumIn = InventoryManager.inventorySum.ContainsKey(item.id);
        int nowNum = nowNumIn ? InventoryManager.inventorySum[item.id] : 0;
        itemData[1].text = nowNum+ "/" + needNum;
        if(nowNum < needNum)
        {
            itemData[1].color = Color.red;
            canBuild = false;
        }
        else
        {
            itemData[1].color = Color.white;
        }
        return canBuild;
    }

    public void RemoveDataPanel()
    {
        item = null;
        gameObject.SetActive(false);
    }
}
