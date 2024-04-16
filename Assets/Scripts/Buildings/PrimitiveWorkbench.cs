using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveWorkbench : Building
{
    public GameObject RecipePanel;
    public Product production;

    
    private new void Awake()
    {
        buildingName = "원시적인 작업대";
        base.Awake();
        RecipePanel = GameManager.Instance.RecipePanel;
        todoList = todoList = PalManager.Instance.producing;
        buildingType = BuildingType.Recipe;

    }
    public override void Action()
    {
        switch(buildingStatement)
        {
            case BuildingStatement.Built:
                RecipePanel.SetActive(true);
                ProductManager.Instance.ResetPanel();
                ProductManager.Instance.buildingName.text = buildingName;
                foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[0])
                {
                    recipe.SetActive(true);
                }
                ProductManager.Instance.nowBuilding = this;
                GameManager.Instance.activePanel = RecipePanel;
                break;
            case BuildingStatement.Done:
                InventoryManager.Instance.DropItem(production);
                GameManager.Instance.GetExp(production.count * 2);
                buildingStatement = BuildingStatement.Built;
                break;
        }
    }
    public override void Work(float work)
    {        
        base.Work(work);
    }
    public override void ConfirmProduct(Product product)
    {
        GameManager.Instance.EscapeMenu(true);
        production = product;
        buildingStatement = BuildingStatement.Working;
        todoList.Add(this);
        MaxWorkTime = production.lavor * production.count;
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
