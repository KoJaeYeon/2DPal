using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : Singleton<CraftManager>
{
    PlayerControll playerControll;

    public GameObject Hole;
    public GameObject DataPanel;

    public Image holeImage;
    public TextMeshProUGUI holeTMP;
    public Sprite initialImage;
    public NeedPanel[] needPanels;

    public bool canBuild;

    public int Crafting_id;
    int[,] needItem;

    public int length;

    private void Awake()
    {
        playerControll = GameManager.Instance.playerController.GetComponent<PlayerControll>();
        holeImage = Hole.GetComponent<Image>();
        holeTMP = Hole.GetComponentInChildren<TextMeshProUGUI>();
        needPanels = DataPanel.GetComponentsInChildren<NeedPanel>();
        initialImage = holeImage.sprite;
        holeTMP.text = "";
    }

    public void FurnitureOver(int id)
    {
        canBuild = true;
        int[,] needItem = FurnitureDatabase.Instance.furnitures[id].buildingItems;
        for (int i = 0; i < needItem.GetLength(0); i++)
        {
            int needId = needItem[i, 0];
            int needNum = needItem[i, 1];
            if (!needPanels[i].UpdateDataPanel(ItemDatabase.Instance.items[needId], needNum)) canBuild = false;
        }
        holeImage.sprite = FurnitureDatabase.Instance.furnitures[id].sprite;
        holeTMP.text = FurnitureDatabase.Instance.furnitures[id].itemName;
    }

    public void FurnitureExit(int id)
    {
        int length = FurnitureDatabase.Instance.furnitures[id].buildingItems.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            needPanels[i].RemoveDataPanel();
        }
        holeImage.sprite= initialImage;
        holeTMP.text = "";
    }

    public void FurnitureChoice(int id)
    {
        this.Crafting_id = id;
        needItem = FurnitureDatabase.Instance.NeedItem(id);
        length = needItem.GetLength(0);
        
        if (!canBuild) //제작 불가능
        {
            return;
        }
        else // 제작가능
        {
            GameManager.Instance.OnCraft();
            playerControll.statement = Statement.Crafting;
            playerControll.freeBuilding = FurnitureDatabase.Instance.GiveFurniture(id);
            playerControll.FreeBuilding();
            PayBuilding();
        }
    }

    public void PayBuilding()
    {
        for(int i = 0; i < length; i++)
        {
            int payItemId = needItem[i,0];
            int payitemNum = needItem[i,1];
            Item payitem = ItemDatabase.Instance.GetItem(payItemId);
            Debug.Log(payitemNum);
            payitem.count = payitemNum;
            InventoryManager.Instance.UseItem(payitem);
        }
    }

    public void ReturnBuilding()
    {

    }
}
