using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerController;

    private PlayerControll playerControll;

    public GameObject OptionPanel;
    public GameObject CraftingPanel;
    public GameObject RecipePanel;
    public GameObject recipeCraftPanel;
    public GameObject palBoxPanel;
    public GameObject chestPanel;

    public GameObject activePanel;

    public TextMeshProUGUI technicPoint;

    public Slider healthSlider;



    private void Awake()
    {
        playerControll = playerController.GetComponent<PlayerControll>();
    }
    private void Start()
    {
        OptionPanel.SetActive(false);
        CraftingPanel.SetActive(false);
        RecipePanel.SetActive(false);
        palBoxPanel.SetActive(false);
        chestPanel.SetActive(false);
        CraftManager.Instance.BuildingPanel.SetActive(false);
    }

    public void ActivePanel(GameObject panel)
    {
        activePanel = panel;
    }
    public bool ManagerUsingUi()
    {
        if(activePanel != null) return true;
        return false;
    }


    public void ExitMenu(bool range = false)
    {
        if (activePanel != null) { activePanel.SetActive(false); activePanel = null; return; }
        if(activePanel == InventoryManager.Instance.FoodPanel) { activePanel = OptionPanel; InventoryManager.Instance.FoodPanel.SetActive(false); return; }
        if (!range) { return; }
        activePanel = OptionPanel; OptionPanel.SetActive(true);
    }
    public void OnCraft()
    {
        if(activePanel == null)
        {
            CraftingPanel.SetActive(true);
            activePanel = CraftingPanel;
        }
        else if(activePanel == CraftingPanel)
        {
            playerControll.statement = Statement.Disassembling;
            CraftingPanel.SetActive(false);
            activePanel = CraftManager.Instance.BuildingPanel;
            CraftManager.Instance.BuildingPanel.SetActive(true);
            CraftManager.Instance.BuildingPanel.transform.GetChild(0).gameObject.SetActive(false);
            CraftManager.Instance.BuildingPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void OnMenu()
    {
        if (activePanel == null)
        {
            activePanel = OptionPanel;
            OptionPanel.SetActive(true);
        }
        else if (activePanel == OptionPanel) { ExitMenu(); }
    }

    public void GetDamage(float damage)
    {
        playerControll.health -= damage;
        healthSlider.value = playerControll.health / playerControll.maxHealth;
    }

    public void GetExp(int exp)
    {
        playerControll.exp += exp;
    }

    public void TechnicPointUse(int point)
    {
        playerControll.TechPoint -= point;
        technicPoint.text = playerControll.TechPoint.ToString();
    }

    public bool CheckTechPoint(int point)
    {
        if(point < playerControll.TechPoint) return false;
        else return true;
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
