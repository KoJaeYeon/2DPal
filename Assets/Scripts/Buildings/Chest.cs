using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Building
{
    public GameObject chestPanel;
    public int volume = 10;
    public GiveKey_Chest chestKey;
    private new void Awake()
    {
        base.Awake();
        chestPanel = GameManager.Instance.chestPanel;
        todoList = todoList = PalManager.Instance.producing;
        buildingType = BuildingType.Chest;
        chestKey = InventoryManager.Instance.chestBoxSlot.GetComponent<GiveKey_Chest>();
    }

    public override void Action()
    {
        switch (buildingStatement)
        {
            case BuildingStatement.Built:
                chestPanel.SetActive(true);
                GameManager.Instance.activePanel = chestPanel;
                chestKey.SetSlotVolume(volume, index * 50);
                break;
        }
    }
}
