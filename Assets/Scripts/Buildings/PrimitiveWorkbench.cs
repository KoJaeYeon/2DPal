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
        buildingName = "�������� �۾���";
        base.Awake();
        recipePanel = GameManager.Instance.recipePanel;
        todoList = PalManager.Instance.producing;
        buildingType = BuildingType.Recipe;

    }
    public override void Action()
    {
        switch(buildingStatement)
        {
            case BuildingStatement.Built: // ���� ��� ��ȣ�ۿ�
                recipePanel.GetComponent<QuitPanelUI>().PanelActive(); // ������ �г� Ȱ��ȭ
                ProductManager.Instance.ResetPanel(); // ������ �г� �ʱ�ȭ
                ProductManager.Instance.buildingName.text = buildingName; // ������ �гΰ� Ȱ��ȭ�� �ǹ� �̸� ����
                foreach (GameObject recipe in FurnitureDatabase.Instance.RecipeSlots[0]) // �ǹ��ɷ¿� ���� ���� ������ ������
                {
                    recipe.SetActive(true); //������ Ȱ��ȭ
                }
                ProductManager.Instance.nowBuilding = this; //���� ��ɽ� ��ǥ�������� �����ؾ� �ϴ� �ǹ� ����
                break;
            case BuildingStatement.Done: // ���� �Ϸ� ��ȣ�ۿ�
                InventoryManager.Instance.DropItem(production); // ���� �Ϸ�� ������ ȹ��
                GameManager.Instance.GetExp(production.count * 2); // ���� �ð��� ����� ����ġ ȹ��
                buildingStatement = BuildingStatement.Built; // �ǹ��� ���� ����
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
