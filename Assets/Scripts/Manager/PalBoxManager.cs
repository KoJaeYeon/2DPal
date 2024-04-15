using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalBoxManager : Singleton<PalBoxManager>
{
    public GameObject inventoryPalSlot;
    public GameObject palboxPalSlot;

    public TMPro.TextMeshProUGUI textMeshProUGUI;
    public Dictionary<int, Pal>[] palBox = new Dictionary<int, Pal>[3];
    private int maxPalBox = 100;

    public int Debugint = 0;

    public Pal DebugPal;

    public int startSlotkey;
    public int endSlotkey;
    public Slot_Pal tempSlot;
    public Slot_Pal tempSlot2;

    public GameObject dataPanel;
    public Image dataPortrait;
    public TMPro.TextMeshProUGUI palLevel;
    public TMPro.TextMeshProUGUI palName;
    public TMPro.TextMeshProUGUI palHealth;
    public Slider slider;

    public void Awake()
    {
        for(int i = 0; i < 3; i++)
        {
            palBox[i] = new Dictionary<int, Pal>();
        }
    }
    void UpdateSlot(int i, Pal pal)
    {
        if (i < 5)
        {
            Slot_Pal slot = inventoryPalSlot.transform.GetChild(i).GetChild(0).GetComponent<Slot_Pal>();
            DebugPal = pal;
            slot.UpdateSlot(pal);
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot(pal);
        }
        else if (i < 90)
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(1).GetChild(0).GetChild(i-5).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot(pal);
        }
        else
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(2).GetChild(0).GetChild(i - 90).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot(pal);
        }

    }
    void UpdateSlot(int i)
    {
        if(i < 5)
        {
            Slot_Pal slot = inventoryPalSlot.transform.GetChild(i).GetChild(0).GetComponent<Slot_Pal>();
            slot.UpdateSlot();
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot();
        }
        else if(i < 90)
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(1).GetChild(0).GetChild(i-5).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot();
        }
        else
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(2).GetChild(0).GetChild(i - 90).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot();
        }

    }
    public void CatchPal(int id)
    {
        for(int i = 0; i < 5; i++) // 지니고 있는 팰 채우기
        {
            if (!palBox[0].ContainsKey(i))
            {
                palBox[0].Add(i, PalDatabase.Instance.GetPal(id));
                UpdateSlot(i, palBox[0][i]);
                return;
            }
        }
        for (int i = 5; i < maxPalBox - 10; i++) // 지니고 있는 팰이 다 찼으면 박스로 보내기
        {
            if (!palBox[1].ContainsKey(i))
            {
                palBox[1].Add(i, PalDatabase.Instance.GetPal(id));
                DebugPal = PalDatabase.Instance.GetPal(id);
                UpdateSlot(i, palBox[1][i]);
                return;
            }
        }
        Debug.Log("Box is Full!");
    }

    public void SwapSlot()
    {
        if (endSlotkey < 5 && !palBox[0].ContainsKey(endSlotkey))
        {
            Dictionary<int, Pal> startPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 90)
            {
                startPalBox = palBox[1];
            }
            else
            {
                startPalBox = palBox[2];
            }
            startPalBox.Remove(startSlotkey);
            palBox[0].Add(endSlotkey, tempSlot.pal);
            UpdateSlot(startSlotkey, null);
            UpdateSlot(endSlotkey, palBox[0][endSlotkey]);
        }
        else if (endSlotkey >=5 &&endSlotkey < 90 && !palBox[1].ContainsKey(endSlotkey)) // 빈 공간이면
        {
            Dictionary<int, Pal> startPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 90)
            {
                startPalBox = palBox[1];
            }
            else
            {
                startPalBox = palBox[2];
            }
            startPalBox.Remove(startSlotkey);
            palBox[1].Add(endSlotkey, tempSlot.pal);
            UpdateSlot(startSlotkey, null);
            UpdateSlot(endSlotkey, palBox[1][endSlotkey]);
        }
        else if (endSlotkey >= 90 && endSlotkey < 100 && !palBox[2].ContainsKey(endSlotkey)) // 빈 공간이면
        {
            Dictionary<int, Pal> startPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 90)
            {
                startPalBox = palBox[1];
            }
            else
            {
                startPalBox = palBox[2];
            }
            startPalBox.Remove(startSlotkey);
            palBox[2].Add(endSlotkey, tempSlot.pal);
            UpdateSlot(startSlotkey, null);
            UpdateSlot(endSlotkey, palBox[2][endSlotkey]);
        }

        else
        {
            Dictionary<int, Pal> startPalBox;
            Dictionary<int, Pal> endPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 90)
            {
                startPalBox = palBox[1];
            }
            else
            {
                startPalBox = palBox[2];
            }

            if (endSlotkey < 5)
            {
                endPalBox = palBox[0];
            }
            else if (endSlotkey < 90)
            {
                endPalBox = palBox[1];
            }
            else
            {
                endPalBox = palBox[2];
            }

            startPalBox[startSlotkey] = endPalBox[endSlotkey];
            endPalBox[endSlotkey] = tempSlot.pal;
            UpdateSlot(startSlotkey, startPalBox[startSlotkey]);
            UpdateSlot(endSlotkey, endPalBox[endSlotkey]);
        }
        
    }

    public void ShowData(Pal pal)
    {
        dataPanel.SetActive(true);
        dataPortrait.sprite = pal.portrait;
        palLevel.text = pal.lv.ToString();
        palName.text = pal.palName;
        palHealth.text = pal.health + "/" + pal.maxHealth;
        slider.value = pal.health / pal.maxHealth;
    }

    public void DeleteData()
    {
        dataPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            foreach(int i in palBox[0].Keys)
            {
                Debug.Log("box : 0, " + palBox[0][i].ToString());
            }
            foreach (int i in palBox[1].Keys)
            {
                Debug.Log("box : 1, "  + palBox[1][i].ToString());
            }
        }
    }
}
