using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftDatabase : Singleton<CraftDatabase>
{
    //public Dictionary<int, Product> Productions = new Dictionary<int, Product>();
    public Sprite[] sprites_product;
    public Sprite[] sprites_fire;
    public Sprite[] sprites_cook;

    public Product testProd;
    //private void Awake()
    //{


    //    index = 0;
    //    id = 1100; product = new Product("�ݼ� �ֱ�", id, 1, 0.15f, 10, "�ݼ��� ���⳪ ���� ���� �� �ʿ��� ����.\n�ݼ� ������ ȭ�ο� �����ؼ� �����.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; ItemDatabase.Instance.items.Add(id, product);

    //    index = 0;
    //    id = 1200; product = new Product("���� ����", id, 1, 0.15f, 5, "\n����� ���簡�� ��������.\n�ű��� ������ �������� ����.", new int[,] { { 5, 1 } }); product.sprite = sprites_cook[index++]; ItemDatabase.Instance.items.Add(id, product);
    //    id = 1201; product = new Product("���� ����", id, 1, 1, 5, "\n����� ���簡�� �������� ����.\n�ű��� ������ �������� ����.", new int[,] { { 6, 1 } }); product.sprite = sprites_cook[index++]; ItemDatabase.Instance.items.Add(id, product);

    //    //�ӽ��Ǿ�
    //    id = 204; List<Item> dropList = new List<Item>(); dropList.Clear(); Product item = GetItem(1001); item.count = 1; dropList.Add(item); ItemDatabase.Instance.dropDics.Add(id, dropList);
    //}


}
