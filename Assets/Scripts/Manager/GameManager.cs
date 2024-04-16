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

    public GameObject activePanel;

    public TextMeshProUGUI technicPoint;

    bool isMenuOn = false;
    bool isCraftOn = false;

    private void Awake()
    {
        playerControll = playerController.GetComponent<PlayerControll>();
    }
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
        if (range) return; // 패널만 닫을 때
        if (isCraftOn) { OnCraft(); return; }
        OnMenu();
    }
    private void OnMenu()
    {
        if (activePanel != null) {  return; }
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

    public void GetExp(int exp)
    {
        playerControll.exp += exp;
    }

    public void TechnicPoint(int point)
    {
        playerControll.TechPoint += point;
        technicPoint.text = playerControll.TechPoint.ToString();
    }

    public bool CheckTechPoint(int point)
    {
        if(point < playerControll.TechPoint) return false;
        else return true;
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
