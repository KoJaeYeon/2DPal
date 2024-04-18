using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotButton : MonoBehaviour, IPointerClickHandler
{
    Slot slot;
    private void Awake()
    {
        slot = transform.GetComponentInChildren<Slot>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            slot.PointerClick();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            slot.PointerRightClick();
        }
    }
}
