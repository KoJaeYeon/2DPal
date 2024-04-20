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
    private void Awake()
    {
        int index = 0;
        int id = 101; Furniture furniture = new Furniture("원시적인 작업대", id, new int[,] { { 1, 2 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 102; furniture = new Furniture("원시적인 화로", id, new int[,] { { 1, 20 }, { 2, 50 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 103; furniture = new Furniture("팰 상자", id, new int[,] { { 1, 8 }, { 2, 3 }, { 3, 1 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 104; furniture = new Furniture("모닥불", id, new int[,] { { 1, 10 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 105; furniture = new Furniture("나무 상자", id, new int[,] { { 1, 15 }, { 2, 5 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 106; furniture = new Furniture("짚 팰 침대", id, new int[,] { { 1, 10 }, { 2, 5 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
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

        for (int i = 0; i < 4; i++) //제작대 최초 슬롯 부여
        {
            MakeSlot(0, 501 + i);
        }

        for (int i = 0; i < 1; i++) //화로 최초 슬롯 부여
        {
            MakeSlot(1,1100 + i);
        }

        for (int i = 0; i < 3; i++) //모닥불 최초 슬롯 부여
        {
            MakeSlot(2,1200 + i);
        }
    }

    public GameObject GiveFurniture(int id) // 가구 프리펩 오브젝트 전달
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
    public void OpenUnlockPanel(TechButton techButton) // 기술 해금 창 열기
    {
        this.techButton = techButton;
        unlockPanel.SetActive(true);
        unlockPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = techButton.text[0].text + "을(를) 해방할까요?";
        GameManager.Instance.activePanel = unlockPanel;
    }
    public void UnlockTech() // 기술 해금
    {
        techButton.ActiveSlot();
    }

    public void OpenFruniture(int id) // 가구 해금
    {
        furnitureButton[id - 102].SetActive(true);
    }

    public int[,] NeedItem(int id) // 건축 필요 재료 전달하기
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
