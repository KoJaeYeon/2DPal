using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image image;
    public TextMeshProUGUI[] itemData = new TextMeshProUGUI[2]; //0 : Count, 1 : Weight

    private void Awake()
    {
        image = GetComponent<Image>();
        itemData = GetComponentsInChildren<TextMeshProUGUI>();
        image.gameObject.SetActive(false);
    }
    public void UpdateSlot(Item item)
    {
        if (item == null)
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

    public void RemoveSlot()
    {
        item = null;
        image.gameObject.SetActive(false);
    }
}
