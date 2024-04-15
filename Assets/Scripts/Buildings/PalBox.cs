using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalBox : Building
{
    public GameObject palBoxPanel;
    private new void Awake()
    {
        base.Awake();
        palBoxPanel = GameManager.Instance.palBoxPanel;
        todoList = todoList = PalManager.Instance.producing;
        buildingType = BuildingType.Pal;
    }
    public override void Action()
    {
        switch (buildingStatement)
        {
            case BuildingStatement.Built:
                palBoxPanel.SetActive(true);
                GameManager.Instance.activePanel = palBoxPanel;
                break;
        }
    }
}
