using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    public int id;
    public Image image;
    public Product product;
    public NeedPanel_Craft[] needPanel;
    public TextMeshProUGUI[] itemData = new TextMeshProUGUI[2]; // 0 : ItemName, 1 : Description

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        needPanel = GetComponentsInChildren<NeedPanel_Craft>();
        itemData[0] = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        itemData[1] = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        product = CraftDatabase.Instance.GetItem(id);
        image.sprite = product.sprite;
        transform.GetChild(1).gameObject.SetActive(false);
        itemData[0].text = product.itemName;
        itemData[1].text = product.description;
    }

    public void UpdateSlot()
    {
        int[,] needIngredients = CraftDatabase.Instance.NeedItem(id);
        for(int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            int needNum = needIngredients[i, 1];
            needPanel[i].UpdateDataPanel(needItem,needNum);
        }
        
    }

    public void PressButton()
    {
        ProductManager.Instance.UpdateProductPanel(product);
        ProductManager.Instance.GetMax(product);
    }
}
