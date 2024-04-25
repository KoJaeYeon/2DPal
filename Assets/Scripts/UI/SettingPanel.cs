using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour,QuitPanelUI
{
    public GameObject lastPanel;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void PanelActive()
    {
        this.gameObject.SetActive(true);
        lastPanel = GameManager.Instance.activePanel;
        GameManager.Instance.activePanel = this.gameObject;
    }

    public void PanelDeactive()
    {
        GameManager.Instance.activePanel = lastPanel;
        gameObject.SetActive(false);
    }
}
