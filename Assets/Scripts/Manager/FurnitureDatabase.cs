using System.Collections;
using System.Collections.Generic;
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

    public Furniture testFur;

    public List<GameObject> workbenchRecipeSlots = new List<GameObject>();
    public List<GameObject> furnaceRecipeSlots = new List<GameObject>();
    private void Awake()
    {
        int index = 0;
        int id = 101; Furniture furniture = new Furniture("원시적인 작업대", id, new int[,] { { 1, 2 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 102; furniture = new Furniture("원시적인 화로", id, new int[,] { { 1, 20 }, { 2, 50 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 103; furniture = new Furniture("팰 상자", id, new int[,] {{ 1, 8 },{ 2, 3 }, {3,1 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);

        for (int i = 0; i < furnitures.Count; i++)
        {
            GameObject prefab = Instantiate(Prefabs[i]);
            furniturePrefabs[i] = new List<GameObject>();
            furniturePrefabs[i].Add(prefab);
            prefab.transform.parent = poolParent.transform;
            prefab.SetActive(false);
        }

        
        for (int i = 0; i < 5; i++)
        {
            GameObject prefab = Instantiate(recipePrefab);
            RecipeSlot slot = prefab.GetComponent<RecipeSlot>();
            slot.id = 1001 + i;
            workbenchRecipeSlots.Add(prefab);
            prefab.SetActive(false);
            prefab.transform.SetParent(GameManager.Instance.recipeCraftPanel.transform);
            prefab.transform.localScale = Vector3.one;
        }
    }

    public GameObject GiveFurniture(int id)
    {
        id %= 100;
        foreach(GameObject furniture in furniturePrefabs[id-1])
        {
            if(!furniture.activeSelf)
            {
                furniture.SetActive(true);
                return furniture;
            }
        }
        GameObject prefab = Instantiate(Prefabs[id-1]);
        furniturePrefabs[id-1].Add(prefab);
        return prefab;
    }

    public int[,] NeedItem(int id)
    {
        testFur = furnitures[id];
        return furnitures[id].buildingItems;
    }
}
