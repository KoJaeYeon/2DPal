using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour // ���� â�� ��ġ�Ǵ� ������ ����
{
    public int id;
    public Image image;
    public Product product;
    public NeedPanel_Craft[] needPanel;
    public TextMeshProUGUI[] itemData = new TextMeshProUGUI[2]; // 0 : ItemName, 1 : Description

    private void Awake()
    {
        needPanel = GetComponentsInChildren<NeedPanel_Craft>();
        image = transform.GetChild(0).GetComponent<Image>();        
        itemData[0] = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        itemData[1] = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void Start()
    {
        product = ItemDatabase.Instance.GetProduct(id);
        image.sprite = product.sprite;
        transform.GetChild(1).gameObject.SetActive(false);
        itemData[0].text = product.itemName;
        itemData[1].text = product.description;
    }

    public void UpdateSlot() // ���� ������Ʈ
    {
        if(needPanel.Length == 0) needPanel = GetComponentsInChildren<NeedPanel_Craft>();
        int[,] needIngredients = ItemDatabase.Instance.NeedItem(id);
        for(int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            int needNum = needIngredients[i, 1];
            needPanel[i].UpdateDataPanel(needItem,needNum);
        }
        
    }

    public void PressButton() //������ ������ �� �����ʿ� ���� ������ ����
    {
        ProductManager.Instance.UpdateProductPanel(product);
        ProductManager.Instance.GetMax(product);
    }
}
