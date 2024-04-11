using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTarget : MonoBehaviour
{
    PlayerControll playerControll;
    public GameObject ActionPanel;
    TextMeshProUGUI Number;
    Building building;

    public Animator[] animators;

    private void Awake()
    {
        playerControll = transform.parent.GetComponent<PlayerControll>();
        Number = ActionPanel.GetComponent<ActionPanel>().Number.GetComponent<TextMeshProUGUI>();
        animators = ActionPanel.GetComponentsInChildren<Animator>();
        ActionPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(ActionPanel.activeSelf)
        {
            if(building.buildingStatement == BuildingStatement.isBuilding)
            {
                float leftwork = building.GetLeftWork();
                leftwork = leftwork < 0 ? 0 : leftwork;
                Number.text = leftwork.ToString();
                ActionPanel.transform.GetChild(0).gameObject.SetActive(true);
                ActionPanel.transform.GetChild(1).gameObject.SetActive(false);

                if (playerControll.statement == Statement.Building)
                {
                    animators[1].SetBool("Press", true);
                }
                else
                {
                    animators[1].SetBool("Press", false);
                }
            }
            else
            {
                ActionPanel.transform.GetChild(1).gameObject.SetActive(true);
                ActionPanel.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void CancelBuilding()
    {
        building.Cancel();
    }

    public void CancelAction(bool ispressed)
    {
        if(ispressed)
        {
            animators[2].Play("LongPressC");
        }
        else
        {
            Debug.Log("IDLE");
            animators[2].Play("Idle");
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
