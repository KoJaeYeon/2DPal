using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerController;
    public GameObject resourceManager;
    public GameObject palManager;
    public GameObject itemManager;
    public GameObject spriteManager;

    public GameObject OptionPanel;

    bool isMenuOn = false;
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

    private void OnInventoryCheck()
    {
        Dictionary<int, Item> dictionary = InventoryManager.Instance.inventory;
        foreach (Item item in dictionary.Values)
        {
            Debug.Log(item);
        }
    }
}
