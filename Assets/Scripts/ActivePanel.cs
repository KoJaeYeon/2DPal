using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePanel : MonoBehaviour
{
    public GameObject[] panel;
    public void PanelActive(int num)
    {
        for(int i = 0; i < panel.Length; i++)
        {
            if(i == num) panel[i].SetActive(true);
            else panel[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        PanelActive(0);
    }
}
