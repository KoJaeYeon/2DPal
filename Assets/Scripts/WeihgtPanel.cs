using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeihgtPanel : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;
    public TextMeshProUGUI[] tmpText;//0 NowWeight, 1: Max

    private void Awake()
    {
        tmpText = new TextMeshProUGUI[2];
        slider = GetComponentInChildren<Slider>();
        fillImage = slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        tmpText[0] = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        tmpText[1] = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateWeight(float nowWeight, float maxWeight) // 아이템이 추가되거나 삭제되면 무게 업데이트 하는 함수
    {
        tmpText[0].text = nowWeight.ToString("0.0");
        string text = maxWeight.ToString("0.0");
        if(nowWeight < 10)
        {
            text = "      /" + text;
        }
        else if(nowWeight < 100)
        {
            text = "         /" + text;
        }
        else
        {
            text = "           /" + text;
        }
        tmpText[1].text = text;
        slider.value = nowWeight / maxWeight;
    }
}
