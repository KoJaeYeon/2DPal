using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : Singleton<CraftManager>
{
    PlayerControll playerControll;

    public GameObject Hole; // 중앙에 뜨는 아이템 정보
    public GameObject DataPanel; // 왼쪽 정보창에 뜨는 아이템 정보 패널
    public GameObject BuildingPanel; // 건축 시작할 때 뜨는 패널

    public Image holeImage;
    public TextMeshProUGUI holeTMP;
    public Sprite initialImage;
    public NeedPanel[] needPanels; //왼쪽 정보창에 뜨는 아이템 패널
    public NeedPanel_Build[] needPanels_Build; // 오른쪽 정보창에 뜨는 건축 패널

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
        //DataPanel.transform.parent.gameObject.SetActive(true); //깜빡임이 심해서 제외
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
        
        if (!canBuild) //제작 불가능
        {
            return;
        }
        else // 제작가능
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

    public void PayBuilding() // 건물지을 때 자원소모
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

    public void ReturnBuilding(int id = 0) // 취소 또는 분해할 때 자원 회수
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
