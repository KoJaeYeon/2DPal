using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FurnitureDatabase : Singleton<FurnitureDatabase>
{
    public Dictionary<int, Furniture> furnitures = new Dictionary<int, Furniture>();
    public GameObject parent;
    public GameObject poolParent;
    public List<GameObject>[] furniturePrefabs = new List<GameObject>[10];
    public GameObject[] Prefabs = new GameObject[10];
    public Sprite[] sprites;
    public Material flashMaterial;
    public GameObject recipePrefab;

    public GameObject[] furnitureButton;

    public Furniture testFur;

    public List<GameObject>[] RecipeSlots = new List<GameObject>[4];

    public GameObject unlockPanel;
    private TechButton techButton;
    public List<int> techList;
    public GameObject techContent;
    public TechButton[] techButtons;

    public List<Building> ConstructedBuilding;

    private void Awake()
    {
        int index = 0;
        int id = 101; Furniture furniture = new Furniture("�������� �۾���", id, new int[,] { { 1, 2 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 102; furniture = new Furniture("�������� ȭ��", id, new int[,] { { 1, 20 }, { 2, 50 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 103; furniture = new Furniture("�� ����", id, new int[,] { { 1, 8 }, { 2, 3 }, { 3, 1 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 104; furniture = new Furniture("��ں�", id, new int[,] { { 1, 10 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 105; furniture = new Furniture("���� ����", id, new int[,] { { 1, 15 }, { 2, 5 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 106; furniture = new Furniture("¤ �� ħ��", id, new int[,] { { 1, 10 }, { 2, 5 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        for (int i = 0; i < furnitures.Count; i++)
        {
            GameObject prefab = Instantiate(Prefabs[i]);
            furniturePrefabs[i] = new List<GameObject>();
            furniturePrefabs[i].Add(prefab);
            prefab.transform.parent = poolParent.transform;
            prefab.SetActive(false);
        }

        for (int i = 0; i < RecipeSlots.Length; i++)
        {
            RecipeSlots[i] = new List<GameObject>();
        }

        for (int i = 0; i < 4; i++) //���۴� ���� ���� �ο�
        {
            MakeSlot(0, 501 + i);
        }

        for (int i = 0; i < 1; i++) //ȭ�� ���� ���� �ο�
        {
            MakeSlot(1,1100 + i);
        }

        for (int i = 0; i < 3; i++) //��ں� ���� ���� �ο�
        {
            MakeSlot(2,1200 + i);
        }

        techList.Clear();
        techButtons = techContent.GetComponentsInChildren<TechButton>();
    }

    public void LoadAll(List<FurnitureData> furnitureDatas)
    {
        foreach(TechButton techButton in techButtons)
        {
            if(techList.Contains(techButton.id))
            {
                techButton.ActiveSlot();
            }
        }

        int length = ConstructedBuilding.Count;
        for(int i = 0;i < length;i++)
        {
            ConstructedBuilding[0].gameObject.SetActive(false);
        }

        length = furnitureDatas.Count;
        for(int i = 0; i < length; i++)
        {
            GameObject furniture = GiveFurniture(furnitureDatas[i].id);
            Building building = furniture.GetComponent<Building>();
            furniture.transform.SetParent(FurnitureDatabase.Instance.parent.transform);
            furniture.transform.position = new Vector2(furnitureDatas[i].positionX, furnitureDatas[i].positionY);
            building.index = furnitureDatas[i].index;
            building.buildingStatement = (BuildingStatement)furnitureDatas[i].buildingStatement;
            building.nowConstructTime = furnitureDatas[i].nowContructTime;
            building.MaxWorkTime = furnitureDatas[i].maxWorkTime;
            building.nowWorkTime = furnitureDatas[i].nowWorkTime;
            building.ChangeRigid();

            if ((int)building.buildingStatement == 1)
            {
                building.BuildColor();
                PalManager.Instance.buildings.Add(building);
            }
            else if (building.buildingType == 0 && (int)building.buildingStatement >= 3)
            {
                if (building is PrimitiveWorkbench)
                {
                    ((PrimitiveWorkbench)building).production = (Product)ItemDatabase.Instance.GetItem(furnitureDatas[i].productId);
                    ((PrimitiveWorkbench)building).production.count = furnitureDatas[i].productCount;
                    PalManager.Instance.producing.Add(building);
                }
                else if (building is PrimitiveFurnature)
                {
                    ((PrimitiveFurnature)building).production = (Product)ItemDatabase.Instance.GetItem(furnitureDatas[i].productId);
                    ((PrimitiveFurnature)building).production.count = furnitureDatas[i].productCount;
                    PalManager.Instance.cooking.Add(building);
                }
                else if (building is Campfire)
                {
                    ((PrimitiveFurnature)building).production = (Product)ItemDatabase.Instance.GetItem(furnitureDatas[i].productId);
                    ((PrimitiveFurnature)building).production.count = furnitureDatas[i].productCount;
                    PalManager.Instance.cooking.Add(building);
                }
            }
            else if(building.buildingType == Building.BuildingType.None)
            {
                ((StrawPalBed)building).Sleep();
            }

        }

    }

    public GameObject GiveFurniture(int id) // ���� ������ ������Ʈ ����
    {
        id %= 100;
        foreach (GameObject furniture in furniturePrefabs[id - 1])
        {
            if (!furniture.activeSelf)
            {
                furniture.SetActive(true);
                return furniture;
            }
        }
        GameObject prefab = Instantiate(Prefabs[id - 1]);
        furniturePrefabs[id - 1].Add(prefab);
        return prefab;
    }
    public void OpenUnlockPanel(TechButton techButton) // ��� �ر� â ����
    {
        this.techButton = techButton;
        unlockPanel.SetActive(true);
        unlockPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = techButton.text[0].text + "��(��) �ع��ұ��?";
        GameManager.Instance.activePanel = unlockPanel;
    }
    public void UnlockTech() // ��� �ر�
    {
        techButton.ActiveSlot();
    }

    public void OpenFruniture(int id) // ���� �ر�
    {
        furnitureButton[id - 102].SetActive(true);
    }

    public int[,] NeedItem(int id) // ���� �ʿ� ��� �����ϱ�
    {
        testFur = furnitures[id];
        return furnitures[id].buildingItems;
    }

    public void MakeSlot(int type, int id)
    {
        GameObject prefab = Instantiate(recipePrefab);
        RecipeSlot slot = prefab.GetComponent<RecipeSlot>();
        slot.id = id;
        RecipeSlots[type].Add(prefab);
        prefab.SetActive(false);
        prefab.transform.SetParent(GameManager.Instance.recipeCraftPanel.transform);
        prefab.transform.localScale = Vector3.one;
    }
}
