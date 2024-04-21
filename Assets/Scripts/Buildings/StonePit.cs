using System.Collections;
using UnityEngine;

public class StonePit : Building
{
    public Item item;
    Coroutine coroutine;
    private new void Awake()
    {
        buildingName = "Ã¤¼®Àå";
        base.Awake();
        todoList = PalManager.Instance.mining;
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
        yield return new WaitForSeconds(1.1f);
        buildingStatement = BuildingStatement.Working;
        if(!todoList.Contains(this)) todoList.Add(this);
        nowWorkTime = 0;
        yield break;
    }
    public override void Work(float work)
    {
        nowWorkTime += work;
        if (nowWorkTime > MaxWorkTime)
        {
            workingPal.palState = PalStates.Idle;
            workingPal = null;
            todoList.Remove(this);
            item = ItemDatabase.Instance.GetItem(2);
            buildingStatement = BuildingStatement.Built;
            coroutine = StartCoroutine(BuildTerm());
        }
    }
}
