using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureDatabase : Singleton<FurnitureDatabase>
{
    public Dictionary<int, Furniture> furnitures = new Dictionary<int, Furniture>();

    public Sprite[] sprites;
    private void Start()
    {
        int index = 0;
        int id = 101; Furniture furniture = new Furniture("�������� �۾���", id, new int[,] { { 1, 2 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 102; furniture = new Furniture("�������� ȭ��", id, new int[,] { { 1, 20 }, { 2, 50 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
        id = 103; furniture = new Furniture("�� ����", id, new int[,] {{ 1, 8 },{ 2, 3 }, {3,1 } }); furniture.sprite = sprites[index++]; furnitures.Add(id, furniture);
    }
}
