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

    private void Awake()
    {
        playerControll = transform.parent.GetComponent<PlayerControll>();
        Number = ActionPanel.GetComponent<ActionPanel>().Number.GetComponent<TextMeshProUGUI>();
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
            }
            else
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("df");
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
