using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveWorkbench : Building
{
    public GameObject RecipePanel;

    private new void Awake()
    {
        base.Awake();
        RecipePanel = GameManager.Instance.RecipePanel;
    }
    public override void Action()
    {
        RecipePanel.SetActive(true);
        GameManager.Instance.activePanel = RecipePanel;
    }
}
