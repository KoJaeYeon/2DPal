using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalBoxManager : Singleton<PalBoxManager>
{
    public GameObject inventoryPalSlot;
    public GameObject palboxPalSlot;

    public List<PalBox> palBoxBuilding;

    public TMPro.TextMeshProUGUI textMeshProUGUI;
    public Dictionary<int, Pal>[] palBox = new Dictionary<int, Pal>[3];
    public Dictionary<int, GameObject> spawnPal = new Dictionary<int, GameObject>();
    private int maxPalBox = 110;

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
        else if (i < 95)
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(1).GetChild(0).GetChild(i-5).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot(pal);
        }
        else
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(2).GetChild(0).GetChild(i - 95).GetChild(0).GetComponent<Slot_Pal>();
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
        else if(i < 95)
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(1).GetChild(0).GetChild(i-5).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot();
        }
        else
        {
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(2).GetChild(0).GetChild(i - 95).GetChild(0).GetComponent<Slot_Pal>();
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
        for (int i = 5; i < maxPalBox - 5; i++) // 지니고 있는 팰이 다 찼으면 박스로 보내기
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

    public void Dispawn()
    {
        for(int i = 95; i < maxPalBox; i++)
        {
            if(spawnPal.ContainsKey(i))
            {
                spawnPal[i].SetActive(false);
                spawnPal.Remove(i);
                for (int j = 5; j < 95; j++)
                {
                    if (!palBox[1].ContainsKey(j))
                    {
                        Debug.Log(palBox[2][i]);
                        palBox[1].Add(j, palBox[2][i]);
                        palBox[2].Remove(i);
                        UpdateSlot(i, null);
                        UpdateSlot(j, palBox[1][j]);
                        break;
                    }
                }
            }
        }
    }

    public void SwapSlot()
    {
        if (endSlotkey < 5 && !palBox[0].ContainsKey(endSlotkey)) // 보유 팰이 빈 공간이면
        {
            Dictionary<int, Pal> startPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 95)
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
            if(startSlotkey >= 95) // 소환된 팰 삭제
            {
                spawnPal[startSlotkey].SetActive(false);
                spawnPal.Remove(startSlotkey);
            }
        }
        else if (endSlotkey >=5 &&endSlotkey < 95 && !palBox[1].ContainsKey(endSlotkey)) // 창고가 빈 공간이면
        {
            Dictionary<int, Pal> startPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 95)
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
            if (startSlotkey >= 95) // 소환된 팰 삭제
            {
                spawnPal[startSlotkey].SetActive(false);
                spawnPal.Remove(startSlotkey);
            }
        }
        else if (endSlotkey >= 95 && endSlotkey < maxPalBox && !palBox[2].ContainsKey(endSlotkey))// 거점 팰이 빈 공간이면
        {
            Dictionary<int, Pal> startPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 95)
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
            if (startSlotkey >= 95) //스폰된 팰을 옮길 때
            {
                spawnPal.Add(endSlotkey, spawnPal[startSlotkey]);
                spawnPal.Remove(startSlotkey);
            }
            else // 새로 스폰할 때
            {
                spawnPal.Add(endSlotkey, PalDatabase.Instance.GivePal(palBox[2][endSlotkey].id));
                spawnPal[endSlotkey].transform.position = palBoxBuilding[0].transform.position + Vector3.down * 2;                
            }
            
        }
        else //Swap
        {
            Dictionary<int, Pal> startPalBox;
            Dictionary<int, Pal> endPalBox;
            if (startSlotkey < 5)
            {
                startPalBox = palBox[0];
            }
            else if (startSlotkey < 95)
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
            else if (endSlotkey < 95)
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

            if (startSlotkey < 95 && endSlotkey>= 95) // 교체해서 새로 소환할 때
            {
                spawnPal[endSlotkey].SetActive(false); //기존 팰 삭제
                spawnPal.Remove(endSlotkey);

                spawnPal.Add(endSlotkey, PalDatabase.Instance.GivePal(palBox[2][endSlotkey].id)); // 새로 팰 추가
                spawnPal[endSlotkey].transform.position = palBoxBuilding[0].transform.position + Vector3.down * 2;
            }
            else if(startSlotkey >= 95 && endSlotkey < 95)
            {
                spawnPal[startSlotkey].SetActive(false); //기존 팰 삭제
                spawnPal.Remove(startSlotkey);

                spawnPal.Add(startSlotkey, PalDatabase.Instance.GivePal(palBox[2][endSlotkey].id)); // 새로 팰 추가
                spawnPal[startSlotkey].transform.position = palBoxBuilding[0].transform.position + Vector3.down * 2;
            }
            else if(startSlotkey >= 95 && endSlotkey >= 95) // Swap
            {
                GameObject temp = spawnPal[startSlotkey];
                spawnPal[startSlotkey] = spawnPal[endSlotkey];
                spawnPal[endSlotkey] = temp;
            }
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

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.V))
    //    {
    //        Debug.Log("v");
    //        foreach(int i in palBox[0].Keys)
    //        {
    //            Debug.Log("box : 0, " + palBox[0][i].ToString());
    //        }
    //        foreach (int i in palBox[1].Keys)
    //        {
    //            Debug.Log("box : 1, "  + palBox[1][i].ToString());
    //        }
    //        foreach (int i in palBox[2].Keys)
    //        {
    //            Debug.Log("box : 2, " + palBox[2][i].ToString());
    //        }
    //    }
    //}
}
