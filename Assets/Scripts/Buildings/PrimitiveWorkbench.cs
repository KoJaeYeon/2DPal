using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrimitiveWorkbench : Building
{
    public GameObject recipePanel;
    public Product production;

    
    private new void Awake()
    {
        buildingName = "원시적인 작업대";
        base.Awake();
        recipePanel = GameManager.Instance.recipePanel;
        todoList = PalManager.Instance.producing;
        buildingType = BuildingType.Recipe;

    }
    public override void Action()
    {
        switch(buildingStatement)
        {
            case BuildingStatement.Built: // 제작 대기 상호작용
                recipePanel.GetComponent<QuitPanelUI>().PanelActive(); // 레시피 패널 활성화
                ProductManager.Instance.ResetPanel(); // 레시피 패널 초기화
                ProductManager.Instance.buildingName.text = buildingName; // 레시피 패널과 활성화한 건물 이름 갱신
                foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[0]) // 건물능력에 따른 제작 가능한 레시피
                {
                    recipe.SetActive(true); //레시피 활성화
                }
                ProductManager.Instance.nowBuilding = this; //제작 명령시 목표아이템을 전달해야 하는 건물 설정
                break;
            case BuildingStatement.Done: // 제작 완료 상호작용
                InventoryManager.Instance.DropItem(production); // 제작 완료된 아이템 획득
                GameManager.Instance.GetExp(production.count * 2); // 생산 시간에 비례한 경험치 획득
                buildingStatement = BuildingStatement.Built; // 건물의 상태 변경
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
        foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[0])
        {
            recipe.SetActive(false);
        }
    }
}
