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
        int id = 1001; Product product = new Product("팰 스피어", id, 1, 0,5, "던져서 팰을 포획하는 도구", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1002; product = new Product("나무 곤봉", id, 1, 10,30, "근접 전투용 나무 곤봉.\n팰과 싸우기엔 좀 불안하다.",new int[,] { { 1, 5 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1003; product = new Product("휴대형 횃불", id, 1, 5,20, "들고 있으면 주변이 환해지는 휴대형 횃불.\n어느 정도 근접 전투도 가능하다.", new int[,] { { 1, 2 }, { 2, 2 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1004; product = new Product("돌 곡괭이", id, 1, 10,30, "돌 곡괭이다.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);
        id = 1005; product = new Product("돌 도끼", id, 1, 10,30, "돌 도끼다.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites_product[index++]; Productions.Add(id, product);

        index = 0;
        id = 1100; product = new Product("금속 주괴", id, 1, 0.15f, 10, "금속제 무기나 방어구를 만들 때 필요한 소재.\n금속 광석을 화로에 제련해서 만든다.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; Productions.Add(id, product);

        index = 0;
        id = 1200; product = new Product("구운 열매", id, 1, 0.15f, 5, "\n구우면 영양가가 높아진다.\n신기할 정도로 포만감이 좋다.", new int[,] { { 5, 1 } }); product.sprite = sprites_cook[index++]; Productions.Add(id, product);
        id = 1201; product = new Product("구운 버섯", id, 1, 1, 5, "\n구우면 영양가가 높아지는 버섯.\n신기할 정도로 포만감이 좋다.", new int[,] { { 6, 1 } }); product.sprite = sprites_cook[index++]; Productions.Add(id, product);

        //팰스피어
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
