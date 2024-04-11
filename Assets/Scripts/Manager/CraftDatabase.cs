using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftDatabase : Singleton<CraftDatabase>
{
    public Dictionary<int, Product> Productions = new Dictionary<int, Product>();
    public Sprite[] sprites;

    public Product testProd;
    private void Awake()
    {
        int index = 0;
        int id = 1001; Product product = new Product("�� ���Ǿ�", id, 1, 0, "������ ���� ��ȹ�ϴ� ����", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1002; product = new Product("���� ���", id, 1, 10, "���� ������ ���� ���.\n�Ӱ� �ο�⿣ �� �Ҿ��ϴ�.",new int[,] { { 1, 5 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1003; product = new Product("�޴��� ȶ��", id, 1, 5, "��� ������ �ֺ��� ȯ������ �޴��� ȶ��.\n��� ���� ���� ������ �����ϴ�.", new int[,] { { 1, 2 }, { 2, 2 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1004; product = new Product("�� ���", id, 1, 10, "�� ��̴�.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1005; product = new Product("�� ����", id, 1, 10, "�� ������.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
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
