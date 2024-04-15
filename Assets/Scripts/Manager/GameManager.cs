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
    public GameObject recipeCraftPanel;
    public GameObject palBoxPanel;

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
        palBoxPanel.SetActive(false);
    }
    public void ActivePanel(GameObject panel)
    {
        activePanel = panel;
    }
    public bool ManagerUsingUi()
    {
        if(isMenuOn||isCraftOn) return true;
        if(activePanel != null) return true;
        return false;
    }

    public void EscapeMenu(bool range = false)
    {
        if(activePanel != null) { activePanel.SetActive(false); activePanel = null; return; }
        if (range) return; // �гθ� ���� ��
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
    public string CountString(int count)
    {
        string countString;
        if (count < 10)
        {
            countString = "00" + count;
        }
        else if (count < 100)
        {
            countString = "0" + count;
        }
        else
        {
            countString = count.ToString();
        }
        return countString;
    }
}
