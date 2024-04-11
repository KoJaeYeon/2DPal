using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : Singleton<CraftManager>
{
    PlayerControll playerControll;

    public GameObject Hole; // �߾ӿ� �ߴ� ������ ����
    public GameObject DataPanel; // ���� ����â�� �ߴ� ������ ���� �г�
    public GameObject BuildingPanel; // ���� ������ �� �ߴ� �г�

    public Image holeImage;
    public TextMeshProUGUI holeTMP;
    public Sprite initialImage;
    public NeedPanel[] needPanels; //���� ����â�� �ߴ� ������ �г�
    public NeedPanel_Build[] needPanels_Build; // ������ ����â�� �ߴ� ���� �г�

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
        needPanels_Build = BuildingPanel.GetComponentsInChildren<NeedPanel_Build>();
        initialImage = holeImage.sprite;
        holeTMP.text = "";
        Hole.SetActive(false);
    }
    private void Start()
    {

        BuildingPanel.SetActive(false);
    }

    public void FurnitureOver(int id)
    {
        Hole.SetActive(true);
        //DataPanel.transform.parent.gameObject.SetActive(true); //�������� ���ؼ� ����
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
        Hole.SetActive(false);
        //DataPanel.transform.parent.gameObject.SetActive(false); 
    }

    public void FurnitureChoice(int id, bool player = false)
    {
        this.Crafting_id = id;
        needItem = FurnitureDatabase.Instance.NeedItem(id);
        length = needItem.GetLength(0);
        
        if (!canBuild) //���� �Ұ���
        {
            return;
        }
        else // ���۰���
        {
            if(!player) GameManager.Instance.OnCraft();
            playerControll.statement = Statement.Crafting;
            playerControll.freeBuilding = FurnitureDatabase.Instance.GiveFurniture(id);
            playerControll.FreeBuilding();
            BuildingPanel.SetActive(true);
            canBuild = true;
            int[,] needItem = FurnitureDatabase.Instance.furnitures[id].buildingItems;
            for (int i = 0; i < needItem.GetLength(0); i++)
            {
                int needId = needItem[i, 0];
                int needNum = needItem[i, 1];
                if (!needPanels_Build[i].UpdateDataPanel(ItemDatabase.Instance.items[needId], needNum)) canBuild = false;
            }
        }
    }

    public void PayBuilding() // �ǹ����� �� �ڿ��Ҹ�
    {
        for(int i = 0; i < length; i++)
        {
            int payItemId = needItem[i,0];
            int payitemNum = needItem[i,1];
            Item payitem = ItemDatabase.Instance.GetItem(payItemId);
            payitem.count = payitemNum;
            InventoryManager.Instance.UseItem(payitem);
        }
    }

    public void ReturnBuilding(int id = 0) // ��� �Ǵ� ������ �� �ڿ� ȸ��
    {
        if (id == 0) id = Crafting_id;
        needItem = FurnitureDatabase.Instance.NeedItem(id);
        for (int i = 0; i < needItem.GetLength(0); i++)
        {
            int needId = needItem[i, 0];
            int needNum = needItem[i, 1];
            Item returnItem = ItemDatabase.Instance.GetItem(needId);
            returnItem.count = needNum;
            InventoryManager.Instance.DropItem(returnItem);

        }
    }
}
