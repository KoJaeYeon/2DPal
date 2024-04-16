using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TechButton : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI text; //name

    private void Awake()
    {
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        //text.text = ItemDatabase
    }
}
