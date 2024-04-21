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

    private void OnEnable()
    {
        Renew();
    }

    public void Renew()
    {
        string date = DataManager.Instance.ButtonText(1);
        if (date != null) textMeshProUGUI[0].text = "���彽��1\n" + date;
        else
        {
            textMeshProUGUI[0].text = "���彽��1\n" + "����� ������ ����\n ";
            loadButtons[0].interactable = false;
        }

        date = DataManager.Instance.ButtonText(2);
        if (date != "null") textMeshProUGUI[1].text = "���彽��1\n" + date;
        else
        {
            textMeshProUGUI[1].text = "���彽��2\n" + "����� ������ ����\n ";
            loadButtons[1].interactable = false;
        }

        date = DataManager.Instance.ButtonText(3);
        if (date != "null") textMeshProUGUI[2].text = "���彽��1\n" + date;
        else
        {
            textMeshProUGUI[2].text = "���彽��3\n" + "����� ������ ����\n ";
            loadButtons[2].interactable = false;
        }
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
