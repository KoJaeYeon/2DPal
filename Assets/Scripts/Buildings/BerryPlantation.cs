using System.Collections;
using UnityEngine;

public class BerryPlantation : Building
{
    public Food food;
    public int sequence = 0;
    Coroutine coroutine;
    private new void Awake()
    {
        buildingName = "¿­¸Å ³óÀå";
        base.Awake();
        todoList = PalManager.Instance.seeding;
        buildingType = BuildingType.Plant;
    }
    public override void Build(float work)
    {
        base.Build(work);
        if (nowConstructTime > MaxConstructTime)
        {
            buildingStatement = BuildingStatement.Built;
            coroutine = StartCoroutine(BuildTerm());
        }
    }

    public override void Action()
    {
        buildingStatement = BuildingStatement.Built;
        coroutine = StartCoroutine(BuildTerm());
    }

    IEnumerator BuildTerm()
    {
        yield return new WaitForSeconds(1.5f);
        buildingStatement = BuildingStatement.Working;
        todoList.Add(this);
        sequence = 0;
        food = ItemDatabase.Instance.GetFood(5);
        yield break;
    }
    public override void Work(float work)
    {
        nowWorkTime += work;
        if (nowWorkTime > MaxWorkTime && sequence == 0)
        {
            workingPal.palState = PalStates.Idle;
            workingPal = null;
            todoList.Remove(this);
            todoList = PalManager.Instance.watering;
            todoList.Add(this);
            sequence++;
            nowWorkTime = 0;
        }
        else if (nowWorkTime > MaxWorkTime && sequence == 1)
        {
            workingPal.palState = PalStates.Idle;
            workingPal = null;
            todoList.Remove(this);
            todoList = PalManager.Instance.harvesting;
            todoList.Add(this);
            sequence++;
            nowWorkTime = 0;
        }
        else if (nowWorkTime > MaxWorkTime && sequence == 2)
        {
            workingPal.palState = PalStates.Idle;
            workingPal = null;
            todoList.Remove(this);
            todoList = PalManager.Instance.seeding;
            todoList.Add(this);
            nowWorkTime = 0;
            InventoryManager.Instance.DropItem(food);
            sequence = 0;
        }
    }
}
