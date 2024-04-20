using UnityEngine;

public class Campfire : Building
{
    public GameObject RecipePanel;
    public Product production;

    private new void Awake()
    {
        buildingName = "¸ð´ÚºÒ";
        base.Awake();
        RecipePanel = GameManager.Instance.RecipePanel;
        todoList = todoList = PalManager.Instance.cooking;
        buildingType = BuildingType.Recipe;
    }
    public override void Action()
    {
        switch (buildingStatement)
        {
            case BuildingStatement.Built:
                RecipePanel.SetActive(true);
                ProductManager.Instance.ResetPanel();
                ProductManager.Instance.buildingName.text = buildingName;
                foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[2])
                {
                    recipe.SetActive(true);
                }
                ProductManager.Instance.nowBuilding = this;
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
        foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[2])
        {
            recipe.SetActive(false);
        }
    }
}
