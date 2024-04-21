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
        todoList = PalManager.Instance.sleeping;
        buildingType = BuildingType.None;

    }

    private new void OnEnable()
    {
        base.OnEnable();
        PalManager.Instance.palBedBuilding.Add(this);
    }
    private new void OnDisable()
    {
        base.OnDisable();
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

    public override void Build(float work)
    {
        base.Build(work);
        if (nowConstructTime > MaxConstructTime)
        {
            if (GameManager.Instance.night.IsMidnight())
            {
                Sleep();
            }
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
        if(!PalManager.Instance.sleeping.Contains(this)) todoList.Add(this);
        MaxWorkTime = 100000;
        nowWorkTime = 0;
    }
}
