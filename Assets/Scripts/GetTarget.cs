using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTarget : MonoBehaviour
{
    PlayerControll playerControll;
    public GameObject ActionPanel;
    TextMeshProUGUI Number; // BuildingPanel
    TextMeshProUGUI Number2; // WrokingPanel
    Building building;

    public Animator[] animators;

    private void Awake()
    {
        playerControll = transform.parent.GetComponent<PlayerControll>();
        Number = ActionPanel.GetComponent<ActionPanel>().Number.GetComponent<TextMeshProUGUI>();
        Number2 = ActionPanel.GetComponent<ActionPanel>().Number2.GetComponent<TextMeshProUGUI>();
        animators = ActionPanel.GetComponentsInChildren<Animator>();
        ActionPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(ActionPanel.activeSelf)
        {
            switch(building.buildingStatement)
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
            ActionPanel.SetActive(false);
            ActionPanel.SetActive(true);
            building = collision.gameObject.GetComponent<Building>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerControll.target = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerControll.target = null;
        if (collision.gameObject.CompareTag("Furniture"))
        {
            animators[0].Play("UnShowEffect");
        }
        GameManager.Instance.EscapeMenu(true);
    }
}
