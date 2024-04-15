using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveWorkbench : Building
{
    public GameObject RecipePanel;
    public Product production;

    
    private new void Awake()
    {
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
                foreach(GameObject recipe in FurnitureDatabase.Instance.workbenchRecipeSlots)
                {
                    recipe.SetActive(true);
                }
                ProductManager.Instance.primitiveWorkbench = this;
                GameManager.Instance.activePanel = RecipePanel;
                break;
            case BuildingStatement.Done:
                InventoryManager.Instance.DropItem(production);
                buildingStatement = BuildingStatement.Built;
                break;
        }
    }
    public override void Work(float work)
    {        
        base.Work(work);
    }
    public void ConfirmProduct(Product product)
    {
        GameManager.Instance.EscapeMenu(true);
        production = product;
        buildingStatement = BuildingStatement.Working;
        todoList.Add(this);
        MaxWorkTime = production.lavor * production.count;
        nowWorkTime = 0;
    }
}
