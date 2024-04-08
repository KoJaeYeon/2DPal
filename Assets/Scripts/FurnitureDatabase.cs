using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureDatabase : Singleton<FurnitureDatabase>
{
    public Dictionary<int, Furniture> furnitures = new Dictionary<int, Furniture>();
    public GameObject parent;
    public List<GameObject>[] furniturePrefabs = new List<GameObject>[10];
    public GameObject[] Prefabs = new GameObject[10];
    public Sprite[] sprites;
    private void Awake()
    {
        int index = 0;
        int id = 101; Furniture furniture = new Furniture("원시적인 작업대", id, new int[,] { { 1, 2 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 102; furniture = new Furniture("원시적인 화로", id, new int[,] { { 1, 20 }, { 2, 50 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 103; furniture = new Furniture("팰 상자", id, new int[,] {{ 1, 8 },{ 2, 3 }, {3,1 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);

        for (int i = 0; i < 3; i++)
        {
            GameObject prefab = Instantiate(Prefabs[i]);
            furniturePrefabs[i] = new List<GameObject>();
            furniturePrefabs[i].Add(prefab);
            prefab.transform.parent = parent.transform;
            prefab.SetActive(false);
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
        prefab.transform.parent = parent.transform;

        return prefab;
    }
}
