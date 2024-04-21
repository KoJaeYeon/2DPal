using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PalBoxManager : Singleton<PalBoxManager>
{
    public GameObject inventoryPalSlot;
    public GameObject palboxPalSlot;

    public List<PalBox> palBoxBuilding;

    public TMPro.TextMeshProUGUI textMeshProUGUI;
    public Dictionary<int, Pal> palBox = new Dictionary<int, Pal>();
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
            palBox = new Dictionary<int, Pal>();
        }
    }

    public void LoadAllSlot(List<PalData> pals)
    {
        palBox.Clear();

        for (int i = 95; i < maxPalBox; i++) //�� ����
        {
            if (spawnPal.ContainsKey(i))
            {
                spawnPal[i].SetActive(false);
                spawnPal[i].transform.SetParent(ItemDatabase.Instance.poolParent.transform);
                spawnPal.Remove(i);
            }
        }
        spawnPal.Clear();

        for(int i =0; i < maxPalBox; i++)
        {
            UpdateSlot(i, null);
        }
        for(int i = 0;i < pals.Count; i++)
        {
            Pal pal = PalDatabase.Instance.GetPal(pals[i].id);
            pal.lv = pals[i].lv;
            pal.LevelUp(pal.lv);
            palBox.Add(pals[i].key, pal);
            UpdateSlot(pals[i].key, pal);
        }

        for (int i = 95; i < maxPalBox; i++) //�� ����
        {
            if (palBox.ContainsKey(i))
            {
                spawnPal.Add(i, PalDatabase.Instance.GivePal(palBox[i].id));
                spawnPal[i].transform.position = palBoxBuilding[0].transform.position + Vector3.down * 2;
                spawnPal[i].transform.SetParent(PalDatabase.Instance.parent_Base.transform);
            }
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
            if (i >= 35) return; // �ڽ� �߰� �� �ӽ���ġ
            Slot_Pal boxSlot = palboxPalSlot.transform.GetChild(1).GetChild(0).GetChild(i-5).GetChild(0).GetComponent<Slot_Pal>();
            boxSlot.UpdateSlot(pal);
        }
        else
        {
            if (i >= 107) return; // ���� �߰� �� �ӽ���ġ
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
        for(int i = 0; i < maxPalBox - 15; i++) // �� ä���
        {
            if (!palBox.ContainsKey(i))
            {
                palBox.Add(i, PalDatabase.Instance.GetPal(id));
                UpdateSlot(i, palBox[i]);
                return;
            }
        }
    }

    public void Dispawn()
    {
        for(int i = 95; i < maxPalBox; i++)
        {
            if(spawnPal.ContainsKey(i))
            {
                spawnPal[i].SetActive(false);
                spawnPal[i].transform.SetParent(ItemDatabase.Instance.poolParent.transform);
                spawnPal.Remove(i);
                for (int j = 5; j < 95; j++)
                {
                    if (!palBox.ContainsKey(j))
                    {
                        palBox.Add(j, palBox[i]);
                        palBox.Remove(i);
                        UpdateSlot(i, null);
                        UpdateSlot(j, palBox[j]);
                        break;
                    }
                }
            }
        }
    }

    public void SwapSlot()
    {
        if (endSlotkey < 5 && !palBox.ContainsKey(endSlotkey)) // ���� ���� �� �����̸�
        {
            palBox.Remove(startSlotkey);
            palBox.Add(endSlotkey, tempSlot.pal);
            UpdateSlot(startSlotkey, null);
            UpdateSlot(endSlotkey, palBox[endSlotkey]);
            if(startSlotkey >= 95) // ��ȯ�� �� ����
            {
                spawnPal[startSlotkey].SetActive(false);
                spawnPal.Remove(startSlotkey);
            }
        }
        else if (endSlotkey >=5 &&endSlotkey < 95 && !palBox.ContainsKey(endSlotkey)) // â�� �� �����̸�
        {
            palBox.Remove(startSlotkey);
            palBox.Add(endSlotkey, tempSlot.pal);
            UpdateSlot(startSlotkey, null);
            UpdateSlot(endSlotkey, palBox[endSlotkey]);
            if (startSlotkey >= 95) // ��ȯ�� �� ����
            {
                spawnPal[startSlotkey].SetActive(false);
                spawnPal.Remove(startSlotkey);
            }
        }
        else if (endSlotkey >= 95 && endSlotkey < maxPalBox && !palBox.ContainsKey(endSlotkey))// ���� ���� �� �����̸�
        {
            palBox.Remove(startSlotkey);
            palBox.Add(endSlotkey, tempSlot.pal);
            UpdateSlot(startSlotkey, null);
            UpdateSlot(endSlotkey, palBox[endSlotkey]);
            if (startSlotkey >= 95) //������ ���� �ű� ��
            {
                spawnPal.Add(endSlotkey, spawnPal[startSlotkey]);
                spawnPal.Remove(startSlotkey);
            }
            else // ���� ������ ��
            {
                spawnPal.Add(endSlotkey, PalDatabase.Instance.GivePal(palBox[endSlotkey].id));
                spawnPal[endSlotkey].transform.position = palBoxBuilding[0].transform.position + Vector3.down * 2;     
                spawnPal[endSlotkey].transform.SetParent(PalDatabase.Instance.parent_Base.transform);

            }

        }
        else //Swap
        {
            palBox[startSlotkey] = palBox[endSlotkey];
            palBox[endSlotkey] = tempSlot.pal;
            UpdateSlot(startSlotkey, palBox[startSlotkey]);
            UpdateSlot(endSlotkey, palBox[endSlotkey]);

            if (startSlotkey < 95 && endSlotkey>= 95) // ��ü�ؼ� ���� ��ȯ�� ��
            {
                spawnPal[endSlotkey].SetActive(false); //���� �� ����
                spawnPal.Remove(endSlotkey);

                spawnPal.Add(endSlotkey, PalDatabase.Instance.GivePal(palBox[endSlotkey].id)); // ���� �� �߰�
                spawnPal[endSlotkey].transform.position = palBoxBuilding[0].transform.position + Vector3.down * 2;
                spawnPal[endSlotkey].transform.SetParent(PalDatabase.Instance.parent_Base.transform);
            }
            else if(startSlotkey >= 95 && endSlotkey < 95)
            {
                spawnPal[startSlotkey].SetActive(false); //���� �� ����
                spawnPal.Remove(startSlotkey);

                spawnPal.Add(startSlotkey, PalDatabase.Instance.GivePal(palBox[endSlotkey].id)); // ���� �� �߰�
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("v");
            foreach (int i in palBox.Keys)
            {
                Debug.Log("box : "+ i +", " + palBox[i].ToString());
            }
        }
    }
}
