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
    //    id = 1100; product = new Product("금속 주괴", id, 1, 0.15f, 10, "금속제 무기나 방어구를 만들 때 필요한 소재.\n금속 광석을 화로에 제련해서 만든다.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; ItemDatabase.Instance.items.Add(id, product);

    //    index = 0;
    //    id = 1200; product = new Product("구운 열매", id, 1, 0.15f, 5, "\n구우면 영양가가 높아진다.\n신기할 정도로 포만감이 좋다.", new int[,] { { 5, 1 } }); product.sprite = sprites_cook[index++]; ItemDatabase.Instance.items.Add(id, product);
    //    id = 1201; product = new Product("구운 버섯", id, 1, 1, 5, "\n구우면 영양가가 높아지는 버섯.\n신기할 정도로 포만감이 좋다.", new int[,] { { 6, 1 } }); product.sprite = sprites_cook[index++]; ItemDatabase.Instance.items.Add(id, product);

    //    //팰스피어
    //    id = 204; List<Item> dropList = new List<Item>(); dropList.Clear(); Product item = GetItem(1001); item.count = 1; dropList.Add(item); ItemDatabase.Instance.dropDics.Add(id, dropList);
    //}


}
