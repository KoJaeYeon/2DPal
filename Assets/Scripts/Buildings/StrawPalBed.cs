using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrawPalBed : Building
{    
    private new void Awake()
    {
        buildingName = "원시적인 작업대";
        base.Awake();
        todoList = PalManager.Instance.producing;
        buildingType = BuildingType.None;

    }

    private new void OnEnable()
    {
        base.OnEnable();
        PalManager.Instance.palBedBuilding.Add(this);
    }
    private void OnDisable()
    {
        PalManager.Instance.palBedBuilding.Remove(this);
    }

    public override void Action()
    {
        switch(buildingStatement)
        {
            case BuildingStatement.Built:
                break;
            case BuildingStatement.Done:
                break;
        }
    }
    public override void Work(float work)
    {        
        base.Work(work);
    }

    public void Done()
    {
        workingPal = null;
        buildingStatement = BuildingStatement.Built;
        todoList.Remove(this);
        MaxConstructTime = 0;
        nowConstructTime = 1;
    }
    public void Sleep()
    {
        buildingStatement = BuildingStatement.Working;
        todoList.Add(this);
        MaxWorkTime = 100000;
        nowWorkTime = 0;
    }
    public override void ResetPanel()
    {
        foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[0])
        {
            recipe.SetActive(false);
        }
    }
}
