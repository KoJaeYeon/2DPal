using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPanel : MonoBehaviour
{
    public GameObject Number; // BuildingPanel
    public GameObject Number2; // WorkingPanel
    public void UnShow()
    {
        gameObject.SetActive(false);
    }
}
