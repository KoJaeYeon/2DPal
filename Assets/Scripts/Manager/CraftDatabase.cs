using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftDatabase : Singleton<CraftDatabase>
{
    public Dictionary<int, Product> Productions = new Dictionary<int, Product>();
    public Sprite[] sprites_product;
    public Sprite[] sprites_fire;
    public Sprite[] sprites_cook;

    public Product testProd;
    private void Awake()
    {
        int index = 0;
        int id = 1001; Product product = new Product("�� ���Ǿ�", id, 1, 0,5, "������ ���� ��ȹ�ϴ� ����", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1002; product = new Product("���� ���", id, 1, 10,30, "���� ������ ���� ���.\n�Ӱ� �ο�⿣ �� �Ҿ��ϴ�.",new int[,] { { 1, 5 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1003; product = new Product("�޴��� ȶ��", id, 1, 5,20, "��� ������ �ֺ��� ȯ������ �޴��� ȶ��.\n��� ���� ���� ������ �����ϴ�.", new int[,] { { 1, 2 }, { 2, 2 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1004; product = new Product("�� ���", id, 1, 10,30, "�� ��̴�.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1005; product = new Product("�� ����", id, 1, 10,30, "�� ������.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);

        index = 0;
        id = 1100; product = new Product("�ݼ� �ֱ�", id, 1, 0.15f, 10, "�ݼ��� ���⳪ ���� ���� �� �ʿ��� ����.\n�ݼ� ������ ȭ�ο� �����ؼ� �����.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; Productions.Add(id, product);

        index = 0;
        id = 1200; product = new Product("���� ����", id, 1, 0.15f, 5, "\n����� ���簡�� ��������.\n�ű��� ������ �������� ����.", new int[,] { { 5, 1 } }); product.sprite = sprites_cook[index++]; Productions.Add(id, product);
        id = 1201; product = new Product("���� ����", id, 1, 1, 5, "\n����� ���簡�� �������� ����.\n�ű��� ������ �������� ����.", new int[,] { { 6, 1 } }); product.sprite = sprites_cook[index++]; Productions.Add(id, product);

        //�ӽ��Ǿ�
        id = 204; List<Item> dropList = new List<Item>(); dropList.Clear(); Product item = GetItem(1001); item.count = 1; dropList.Add(item); ItemDatabase.Instance.dropDics.Add(id, dropList);
    }

    public Product GetItem(int id)
    {
        Product product = new Product(Productions[id]);
        return product;
    }

    public int[,] NeedItem(int id)
    {
        testProd = Productions[id];
        return Productions[id].ingredients;
    }
}
