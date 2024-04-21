using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] textMeshProUGUI;
    [SerializeField]
    Button[] loadButtons;
    [SerializeField] bool save;

    private void OnEnable()
    {
        Renew();
    }

    public void Renew()
    {
        string date = DataManager.Instance.ButtonText(1);
        if (date != null) textMeshProUGUI[0].text = "저장슬롯1\n" + date;
        else
        {
            textMeshProUGUI[0].text = "저장슬롯1\n" + "저장된 데이터 없음\n ";
            if(!save) loadButtons[0].interactable = false;
            else loadButtons[2].interactable = true;
        }

        date = DataManager.Instance.ButtonText(2);
        if (date != null) textMeshProUGUI[1].text = "저장슬롯2\n" + date;
        else
        {
            textMeshProUGUI[1].text = "저장슬롯2\n" + "저장된 데이터 없음\n ";
            if (!save) loadButtons[1].interactable = false;
            else loadButtons[2].interactable = true;
        }

        date = DataManager.Instance.ButtonText(3);
        if (date != null) textMeshProUGUI[2].text = "저장슬롯3\n" + date;
        else
        {
            textMeshProUGUI[2].text = "저장슬롯3\n" + "저장된 데이터 없음\n ";
            if (!save) loadButtons[2].interactable = false;
            else loadButtons[2].interactable = true;
        }
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
