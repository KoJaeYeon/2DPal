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

    public bool ManagerUsingUi()
    {
        if(isMenuOn||isCraftOn) return true;
        return false;
    }
    private void OnMenu()
    {
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

    private void OnInventoryCheck()
    {
        Dictionary<int, Item> dictionary = InventoryManager.Instance.inventory;
        foreach (Item item in dictionary.Values)
        {
            Debug.Log(item);
        }
    }
}
