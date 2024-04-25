using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductManager : Singleton<ProductManager>
{
    public GameObject productionPanel;
    public GameObject frunacePanel;

    public Building nowBuilding;
    public GameObject DataPanel;
    public GameObject IngredientPanel;
    public GameObject ConfirmPanel;    
    public Image productImage;
    public TextMeshProUGUI productName;
    public TextMeshProUGUI productHasCount;
    public TextMeshProUGUI countText;

    public TextMeshProUGUI buildingName;

    public NeedPanel[] needPanel;
    public Product setProduct;
    public int max = 9999;
    public int count = 1;

    private void Awake()
    {
        DataPanel = productionPanel.transform.GetChild(0).gameObject;
        IngredientPanel = productionPanel.transform.GetChild(1).gameObject;
        ConfirmPanel = productionPanel.transform.GetChild(2).gameObject;
        productImage = productionPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        productName = productionPanel.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        productHasCount = productionPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        needPanel = productionPanel.GetComponentsInChildren<NeedPanel>();
        countText = productionPanel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        DataPanel.SetActive(false);
        ConfirmPanel.SetActive(false);
    }

    public void ResetPanel()
    {
        setProduct = null;

        if(nowBuilding != null) { nowBuilding.ResetPanel(); }        
        nowBuilding = null;
        count = 1;
        countText.text = GameManager.Instance.CountString(count);
        DataPanel.SetActive(false);
        ConfirmPanel.SetActive(false);
    }

    public void UpdateProductPanel(Product product)
    {
        setProduct = product;
        DataPanel.SetActive(true);
        IngredientPanel.SetActive(true);
        ConfirmPanel.SetActive(true);
        for (int i = 0; i < needPanel.Length; i++)
        {
            needPanel[i].RemoveDataPanel();
        }
        productImage.sprite = product.sprite;
        productName.text = product.itemName + "x" + product.count * 1;        
        if (InventoryManager.inventorySum.ContainsKey(product.id)) { productHasCount.text = InventoryManager.inventorySum[product.id].ToString(); }
        else productHasCount.text = "0";

        int[,] needIngredients = product.ingredients;
        for (int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            int needNum = needIngredients[i, 1];
            needPanel[i].UpdateDataPanel(needItem, needNum);
        }
    }

    public void GetMax(Product product)
    {
        max = 9999;
        int[,] needIngredients = product.ingredients;
        for (int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            bool nowNumIn = InventoryManager.inventorySum.ContainsKey(needItem.id);
            int nowNum = nowNumIn ? InventoryManager.inventorySum[needItem.id] : 0;
            int needNum = needIngredients[i, 1];
            max = max <= (nowNum / needNum) ? max : nowNum/needNum;
        }
        
    }

    public void ConfirmProduct()
    {
        setProduct.count = count * setProduct.count;
        nowBuilding.ConfirmProduct(setProduct);
        int[,] needIngredients = setProduct.ingredients;
        for (int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            needItem.count = needIngredients[i, 1] * count;
            InventoryManager.Instance.UseItem(needItem);
        }
        for (int i = 0; i < needPanel.Length; i++)
        {
            needPanel[i].RemoveDataPanel();
        }


    }

    public void SetPlusMinus(bool plus)
    {
        if (plus && count < max) { count++; }
        else if (!plus && count > 1) { count--; }
        else return;

        productName.text = setProduct.itemName + "x" + count;
        int[,] needIngredients = setProduct.ingredients;
        for (int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            int needNum = needIngredients[i, 1];
            needPanel[i].UpdateDataPanel(needItem, needNum, count);
            countText.text = GameManager.Instance.CountString(count);
        }
    }

    public void SetMaxOrMin(bool Bmax)
    {
        if(Bmax)count = max;
        else count = 1;

        productName.text = setProduct.itemName + "x" + count;
        int[,] needIngredients = setProduct.ingredients;
        for (int i = 0; i < needIngredients.GetLength(0); i++)
        {
            Item needItem = ItemDatabase.Instance.GetItem(needIngredients[i, 0]);
            int needNum = needIngredients[i, 1];
            needPanel[i].UpdateDataPanel(needItem, needNum,count);
            countText.text = GameManager.Instance.CountString(count);
        }
    }
}
