using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerController;

    public GameObject OptionPanel;
    public GameObject CraftingPanel;
    public GameObject RecipePanel;

    public GameObject activePanel;

    bool isMenuOn = false;
    bool isCraftOn = false;

    private void Start()
    {
        OnMenu();
        OnMenu();
        OnCraft();
        OnCraft();
        RecipePanel.SetActive(false);
    }
    public void ActivePanel(GameObject panel)
    {
        activePanel = panel;
    }
    public bool ManagerUsingUi()
    {
        if(isMenuOn||isCraftOn) return true;
        return false;
    }

    public void EscapeMenu(bool range = false)
    {
        if(activePanel != null) { activePanel.SetActive(false); return; }
        if (range) return; // 패널만 닫을 때
        if (isCraftOn) { OnCraft(); return; }
        OnMenu();
    }
    private void OnMenu()
    {
        if (isCraftOn) return;
        if (isMenuOn)
        {
            OptionPanel.SetActive(false);
            isMenuOn = false;
        }
        else
        {
            OptionPanel.SetActive(true);
            isMenuOn = true;
        }
    }

    public void OnCraft()
    {
        if (isMenuOn) return;
        if (isCraftOn)
        {
            CraftingPanel.SetActive(false);
            isCraftOn = false;
        }
        else
        {
            CraftingPanel.SetActive(true);
            isCraftOn = true;
        }
    }
}
