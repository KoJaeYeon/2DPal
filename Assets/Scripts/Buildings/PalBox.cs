using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private new void OnEnable()
    {
        base.OnEnable();
        PalBoxManager.Instance.palBoxBuilding.Add(this);
    }
    private new void OnDisable()
    {
        base.OnDisable();
        PalBoxManager.Instance.palBoxBuilding.Remove(this);
        PalBoxManager.Instance.Dispawn();
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
