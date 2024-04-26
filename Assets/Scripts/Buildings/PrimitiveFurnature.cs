using UnityEngine;

public class PrimitiveFurnature : Building
{
    public GameObject recipePanel;
    public Product production;


    private new void Awake()
    {
        buildingName = "원시적인 화로";
        base.Awake();
        recipePanel = GameManager.Instance.recipePanel;
        todoList = PalManager.Instance.cooking;
        buildingType = BuildingType.Recipe;
    }
    public override void Action()
    {
        switch (buildingStatement)
        {
            case BuildingStatement.Built:
                recipePanel.GetComponent<QuitPanelUI>().PanelActive();
                ProductManager.Instance.ResetPanel();
                ProductManager.Instance.buildingName.text = buildingName;
                foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[1])
                {
                    recipe.SetActive(true);
                }
                ProductManager.Instance.nowBuilding = this;
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
    public override void ConfirmProduct(Product product)
    {
        GameManager.Instance.ExitMenu();
        production = product;
        buildingStatement = BuildingStatement.Working;
        todoList.Add(this);
        MaxWorkTime = production.lavor * production.count;
        nowWorkTime = 0;
    }

    public override void ResetPanel()
    {
        foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[1])
        {
            recipe.SetActive(false);
        }
    }
}
