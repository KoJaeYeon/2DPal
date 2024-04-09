using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerController;

    public GameObject OptionPanel;
    public GameObject CraftingPanel;

    bool isMenuOn = false;
    bool isCraftOn = false;

    private void Start()
    {

    }
    public bool ManagerUsingUi()
    {
        if(isMenuOn||isCraftOn) return true;
        return false;
    }

    private void OnEscape()
    {
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
