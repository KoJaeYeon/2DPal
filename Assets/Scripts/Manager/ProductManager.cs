using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductManager : Singleton<ProductManager>
{
    public GameObject productionPanel;
    public PrimitiveWorkbench primitiveWorkbench;

    public GameObject DataPanel;
    public Image productImage;
    public TextMeshProUGUI productName;
    public TextMeshProUGUI productHasCount;

    private void Awake()
    {
        DataPanel = productionPanel.transform.GetChild(0).gameObject;
        productImage = productionPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        productName = productionPanel.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        productHasCount = productionPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        DataPanel.SetActive(false);
    }

    public void ResetPanel()
    {
        DataPanel.SetActive(false);
    }

    public void UpdateProductPanel(Product product)
    {
        DataPanel.SetActive(true);
        productImage.sprite = product.sprite;
        productName.text = product.itemName + "x1";
        if (InventoryManager.inventorySum.ContainsKey(product.id)) { productHasCount.text = InventoryManager.inventorySum[product.id].ToString(); }
        else productHasCount.text = "0";
    }
}
