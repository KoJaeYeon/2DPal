using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Pal : MonoBehaviour
{
    public Pal pal;
    public Image image; // portrait
    public int key;
    public TextMeshProUGUI[] palData = new TextMeshProUGUI[4]; //0 : Level, 1 : Num, 2 : palName, 3 : health
    public Slider slider;

    bool dragOn;
    protected void Awake()
    {
        pal = null;
        try
        {
            image = transform.GetChild(4).GetComponent<Image>();
        }
        catch
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }
        palData = GetComponentsInChildren<TextMeshProUGUI>();
    }

    #region SwapPoint
    public void DragOn()
    {
        Debug.Log("DragOn");
        SetSlot();
    }
    public void PointerClick()
    {
        Debug.Log("Click");
    }

    void SetSlot()
    {
        if (pal == null) { Debug.Log("return"); return; } // 빈칸이 시작지점일 때
        Vector3 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        PalBoxManager.Instance.startSlotkey = this.key;
        PalBoxManager.Instance.tempSlot.gameObject.SetActive(true);
        PalBoxManager.Instance.tempSlot.pal = new Pal(pal);
        PalBoxManager.Instance.tempSlot.UpdateSlot();
        PalBoxManager.Instance.tempSlot.transform.position = point;
        PalBoxManager.Instance.tempSlot2.gameObject.SetActive(true);
        PalBoxManager.Instance.tempSlot2.pal = new Pal(pal);
        PalBoxManager.Instance.tempSlot2.UpdateSlot();
        PalBoxManager.Instance.tempSlot2.transform.position = point;
    }

    public void EndDrag()
    {
        Debug.Log("DragEnd");
        if (!PalBoxManager.Instance.tempSlot.gameObject.activeSelf) return; // Temp 슬롯이 비 활성화일 때(인벤토리 꺼졌을때 드래그 유지 중)
        PalBoxManager.Instance.tempSlot.gameObject.SetActive(false);
        PalBoxManager.Instance.tempSlot2.gameObject.SetActive(false);
        if (PalBoxManager.Instance.endSlotkey == -1) return;// 빈 공간에 놓았을 때
        PalBoxManager.Instance.SwapSlot();
    }
    public void EnterMouse()
    {
        //Debug.Log("Enter");
        PalBoxManager.Instance.endSlotkey = key;
        if(pal != null) PalBoxManager.Instance.ShowData(pal);
    }
    public void ExitMouse()
    {
        //Debug.Log("Exit");
        PalBoxManager.Instance.endSlotkey = -1;
        PalBoxManager.Instance.DeleteData();
    }
    #endregion
    public void UpdateSlot(Pal pal)
    {
        if (pal == null)
        {
            RemoveSlot();
        }
        else
        {

            this.pal = pal;
            image.sprite = pal.portrait;
            gameObject.SetActive(true);
            if (key < 5)
            {
                palData[1].text = pal.lv.ToString();
                palData[2].text = pal.palName.ToString();
                palData[3].text = (pal.health + "/" + pal.maxHealth);
            }
        }

    }

    public void UpdateSlot()
    {
        UpdateSlot(pal);
    }

    public void RemoveSlot() // 보류
    {
        Debug.Log("remove");
        pal = null;
        gameObject.SetActive(false);
        if( key < 5 )
        {
            Debug.Log(PalBoxManager.Instance.palBox[0]);
            if (PalBoxManager.Instance.palBox[0].ContainsKey(key)) PalBoxManager.Instance.palBox[0].Remove(key);
        }
        //조건추가
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Slot other = obj as Slot;
        if (other.key == key) return true;
        else return false;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
