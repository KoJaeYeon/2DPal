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
    Vector3 inititalVec;

    bool dragOn;

    private void Awake()
    {
        image = GetComponent<Image>();
        itemData = GetComponentsInChildren<TextMeshProUGUI>();
        image.gameObject.SetActive(false);
        inititalVec = transform.position;
    }

    private void Update()
    {
        if(dragOn)
        {
            Vector3 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.position = point;
        }

    }

    public void DragOn(bool dragtrue)
    {
        if (dragtrue)
        {
            dragOn = true;
            InventoryManager.Instance.startSlot = this;
        }
        else
        {
            dragOn = false;
            transform.localPosition = Vector3.zero;
            InventoryManager.Instance.SwapSlot();
        }

    }
    public void EnterMouse()
    {
        Debug.Log(this);
        InventoryManager.Instance.EndSlot(this);
    }
    public void ExitMouse()
    {
        Debug.Log(null);
        InventoryManager.Instance.EndSlot(null);
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
