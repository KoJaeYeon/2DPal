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
        int id = 1001; Product product = new Product("팰 스피어", id, 1, 0, "던져서 팰을 포획하는 도구", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1002; product = new Product("나무 곤봉", id, 1, 10, "근접 전투용 나무 곤봉.\n팰과 싸우기엔 좀 불안하다.",new int[,] { { 1, 5 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1003; product = new Product("휴대형 횃불", id, 1, 5, "들고 있으면 주변이 환해지는 휴대형 횃불.\n어느 정도 근접 전투도 가능하다.", new int[,] { { 1, 2 }, { 2, 2 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1004; product = new Product("돌 곡괭이", id, 1, 10, "돌 곡괭이다.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
        id = 1005; product = new Product("돌 도끼", id, 1, 10, "돌 도끼다.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites[index++]; Productions.Add(id, product);
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
