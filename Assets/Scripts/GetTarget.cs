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

                if(playerControll.statement == Statement.Building)
                {
                    animators[0].SetBool("Press", true);
                }
                else
                {
                    animators[0].SetBool("Press", false);
                }
            }
            else
            {

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
            animators[1].Play("LongPressC");
        }
        else
        {
            Debug.Log("IDLE");
            animators[1].Play("Idle");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Furniture"))
        {
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
            ActionPanel.SetActive(false);
        }
    }
}
