using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTarget : MonoBehaviour
{
    PlayerControll playerControll;
    public GameObject ActionPanel;
    TextMeshProUGUI Number; // BuildingPanel
    TextMeshProUGUI Number2; // WorkingPanel
    TextMeshProUGUI interactiveText;
    Building building;

    public Animator[] animators;

    public NeedPanel_Build[] needPanels_DisAsseble; // 오른쪽 정보창에 뜨는 해체 패널
    private void Awake()
    {
        playerControll = transform.parent.GetComponent<PlayerControll>();
        Number = ActionPanel.GetComponent<ActionPanel>().Number.GetComponent<TextMeshProUGUI>();
        Number2 = ActionPanel.GetComponent<ActionPanel>().Number2.GetComponent<TextMeshProUGUI>();
        interactiveText = ActionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        animators = ActionPanel.GetComponentsInChildren<Animator>();
        ActionPanel.gameObject.SetActive(false);
        needPanels_DisAsseble = CraftManager.Instance.BuildingPanel.transform.GetChild(1).GetComponentsInChildren<NeedPanel_Build>();
    }
    private void Update()
    {
        if(ActionPanel.activeSelf)
        {
            if(building != null)
            {
                switch (building.buildingStatement)
                {
                    case BuildingStatement.isBuilding:
                        float leftwork = building.GetLeftBuild();
                        leftwork = leftwork < 0 ? 0 : leftwork;
                        Number.text = leftwork.ToString("0.0");
                        ActionPanel.transform.GetChild(0).gameObject.SetActive(true);
                        ActionPanel.transform.GetChild(1).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(2).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(3).gameObject.SetActive(false);

                        if (playerControll.statement == Statement.Building)
                        {
                            animators[1].SetBool("Press", true);
                        }
                        else
                        {
                            animators[1].SetBool("Press", false);
                        }
                        break;
                    case BuildingStatement.Built:
                        ActionPanel.transform.GetChild(0).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(1).gameObject.SetActive(true);
                        ActionPanel.transform.GetChild(2).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(3).gameObject.SetActive(false);
                        break;
                    case BuildingStatement.Working:
                        leftwork = building.GetLeftWork();
                        leftwork = leftwork < 0 ? 0 : leftwork;
                        Number2.text = leftwork.ToString("0.0");
                        ActionPanel.transform.GetChild(0).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(1).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(2).gameObject.SetActive(true);
                        ActionPanel.transform.GetChild(3).gameObject.SetActive(false);
                        if (playerControll.statement == Statement.Action)
                        {
                            animators[3].SetBool("Press", true);
                        }
                        else
                        {
                            animators[3].SetBool("Press", false);
                        }
                        break;
                    case BuildingStatement.Done:
                        ActionPanel.transform.GetChild(0).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(1).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(2).gameObject.SetActive(false);
                        ActionPanel.transform.GetChild(3).gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ActionPanel.transform.GetChild(0).gameObject.SetActive(false);
                ActionPanel.transform.GetChild(1).gameObject.SetActive(false);
                ActionPanel.transform.GetChild(2).gameObject.SetActive(false);
                ActionPanel.transform.GetChild(3).gameObject.SetActive(true);
            }
            
        }
    }

    public void CancelBuilding()
    {
        building.Cancel();
    }

    public void CancelAction(bool ispressed)
    {
        Animator selectedAnimator = animators[2];
        switch(building.buildingStatement)
        {
            case BuildingStatement.isBuilding:
                selectedAnimator = animators[2];
                break;
            case BuildingStatement.Working:
                selectedAnimator = animators[4];
                break;
        }
        if(ispressed)
        {
            selectedAnimator.Play("LongPressC");
        }
        else
        {
            selectedAnimator.Play("Idle");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Furniture"))
        {
            building = collision.gameObject.GetComponent<Building>();

            if (playerControll.statement == Statement.Disassembling) // 분해모드 일 때
            {
                int[,] needItem = FurnitureDatabase.Instance.furnitures[building.id].buildingItems;
                for (int i = 0; i < needItem.GetLength(0); i++)
                {
                    int needId = needItem[i, 0];
                    int needNum = needItem[i, 1];
                    needPanels_DisAsseble[i].UpdateDataPanel(ItemDatabase.Instance.items[needId], needNum);
                }
            }
            else
            {
                ActionPanel.SetActive(false);
                ActionPanel.SetActive(true);
                //텍스트 변경 Interactive
                switch (building.buildingType)
                {
                    case Building.BuildingType.Recipe:
                        interactiveText.text = "레시피 선택";
                        break;
                    case Building.BuildingType.Pal:
                        interactiveText.text = "팰 상자 관리 메뉴";
                        break;
                    case Building.BuildingType.Chest:
                        interactiveText.text = "열기";
                        break;
                    case Building.BuildingType.None:
                        if(building.buildingStatement != BuildingStatement.isBuilding)
                            interactiveText.text = "";
                        ActionPanel.SetActive(false);
                        break;
                }
            }

        }
        else if(collision.gameObject.CompareTag("DropItem"))
        {
            ActionPanel.SetActive(false);
            ActionPanel.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerControll.target = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerControll.target = null;
        building = null;
        if (collision.gameObject.CompareTag("Furniture") || collision.gameObject.CompareTag("DropItem"))
        {
            if(playerControll.statement == Statement.Disassembling)
            {
                int length = needPanels_DisAsseble.Length;
                for (int i = 0; i < length; i++)
                {
                    needPanels_DisAsseble[i].RemoveDataPanel();
                }
            }
            else
            {                
                animators[0].Play("UnShowEffect");
            }
        }
        if(playerControll.statement != Statement.Disassembling)
        {
            GameManager.Instance.ExitMenu();
        }
    }
}
