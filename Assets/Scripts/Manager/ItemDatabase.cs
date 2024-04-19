using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public Dictionary<int,Item> items = new Dictionary<int, Item>();

    public GameObject poolParent;
    public GameObject itemPrefab;
    public List<DropItem> dropItems = new List<DropItem>();

    public Sprite[] sprites;
    public Sprite[] dropSprites;
    public Sprite[] sprites_product;
    public Sprite[] sprites_equip;
    public Sprite[] sprites_fire;
    public Sprite[] sprites_cook;

    public Dictionary<int, List<Item>> dropDics = new Dictionary<int, List<Item>>();

    public Product testProd;
    private void Awake()
    {
        int index = 0;
        int id = 1; Item item = new Item("나무", id, 40, 3, "건축물이나 아이템의 재료로 쓴다.\n주로 나무를 벌목하여 획득한다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("돌", id, 40, 3, "건축물이나 아이템의 재료로 쓴다.\n돌을 채굴하여 획득한다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("팰지움 파편", id, 40, 1, "세계수의 에너지가 결정화한 물건.\n굉장히 특별한 아이템이나 건축물을 만들 수 있다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("금속 광석", id, 40, 8, "화로에서 제련하면 주괴가 된다.\n동굴 같은 곳에서 발견할 수 있다.\n광석을 탐지할 수 있는 팰도 있는 듯하다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 5; Food food = new Food("빨간 열매", id, 2, 0.2f, 0, "그냥 먹어도, 구워 먹어도 맛있는 만능 식재료.\n섬을 돌아다니면 쉽게 보인다"); food.sprite = sprites[index++]; items.Add(id, food);
        id = 6; food = new Food("버섯", id, 1, 1f, 0, "그냥 먹어도 되는 버섯.\n섬을 돌아다니다 보면 쉽게 보인다."); food.sprite = sprites[index++]; items.Add(id, food);
        id = 7; item = new Item("열매 씨", id, 2, 0.05f, "빨간 열매을(를) 재배할 수 있는 씨.\n농원을 만들 때 필요하다.\n빨간 열매을(를) 채취할 때 얻을 수 있다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 8; item = new Item("섬유", id, 4, 0.5f, "나무에서 얻는 섬유.\n활 등을 제작하는 소재가 된다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 9; item = new Item("양털", id, 2, 1f, "양 팰에게서 얻는 소재.\n침대나 천을(를) 만들 때 필요하다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 10; food = new Food("도로롱", id, 1, 1f, 0, "도로롱의 고기.\n독특한 풍미가 매력적인 붉은 살코기. 잡내가 있지만 맛이 좋다."); food.sprite = sprites[index++]; items.Add(id, food);


        index = 0;
        id = 1000; Product product = new Product("팰 스피어", id, 1, 0, 5, "던져서 팰을 포획하는 도구", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1001; product = new Product("화살", id, 3, 0.02f, 10, "활로 발사하는 화살.", new int[,] { { 1, 1 }, { 2, 1 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1002; product = new Product("천", id, 1, 1, 5, "양털을 짜서 만든 천.\n방어구를 만들 때 필요하다.", new int[,] { { 9, 2 } }); product.sprite = sprites_product[index++]; items.Add(id, product);

        index = 0;
        id = 501; Equip equip = new Equip("나무 곤봉", id, 1, 10, 30, "근접 전투용 나무 곤봉.\n팰과 싸우기엔 좀 불안하다.", new int[,] { { 1, 5 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 502; equip = new Equip("휴대형 횃불", id, 1, 5, 20, "들고 있으면 주변이 환해지는 휴대형 횃불.\n어느 정도 근접 전투도 가능하다.", new int[,] { { 1, 2 }, { 2, 2 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 503; equip = new Equip("돌 곡괭이", id, 1, 10, 30, "채굴용 곡괭이.\n돌로 만들어 효율이 떨어진다.", new int[,] { { 1, 5 }, { 2, 5 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 504; equip = new Equip("돌 도끼", id, 1, 10, 30, "나무를 베는 용도의 도끼.\n돌로 만들어서 절삭력이 조금 아쉽다.", new int[,] { { 1, 5 }, { 2, 5 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 505; equip = new Equip("오래된 활", id, 1, 6, 20, "원거리 공격이 가능한 활.\n이것저것 긁어모아 만든 탓에 위력은 떨어진다.", new int[,] { { 1, 30 }, { 2, 5 }, { 8, 15 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);

        ////DropItemId
        //드랍 오브젝트 드랍리스트
        //나무
        id = 201; List<Item> dropList = new List<Item>(); item = GetItem(1); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //돌
        id = 202; dropList = new List<Item>(); item = GetItem(2); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //팰지움 파편
        id = 203; dropList = new List<Item>(); item = GetItem(3); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //팰스피어
        id = 204; dropList = new List<Item>(); product = GetProduct(1000); product.count = 1; dropList.Add(product); dropDics.Add(id, dropList);
        //베리덤불
        id = 205; dropList = new List<Item>(); food = GetFood(5); food.count = 4; dropList.Add(food); item = GetItem(7); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //버섯
        id = 206; dropList = new List<Item>(); food = GetFood(6); food.count = 4; dropList.Add(food); dropDics.Add(id, dropList);


        //적 팰 드랍리스트
        id = 1; dropList = new List<Item>(); food = GetFood(10); food.count = 1; dropList.Add(food); item = GetItem(9); item.count = 2; dropList.Add(item); dropDics.Add(id, dropList);


        index = 0;
        id = 1100; product = new Product("금속 주괴", id, 1, 0.15f, 10, "금속제 무기나 방어구를 만들 때 필요한 소재.\n금속 광석을 화로에 제련해서 만든다.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; items.Add(id, product);

        index = 0;
        id = 1200; food = new Food("구운 열매", id, 1, 0.15f, 5, "구우면 영양가가 높아진다.\n신기할 정도로 포만감이 좋다.", new int[,] { { 5, 1 } }); food.sprite = sprites_cook[index++]; items.Add(id, food);
        id = 1201; food = new Food("구운 버섯", id, 1, 1, 5, "구우면 영양가가 높아지는 버섯.\n신기할 정도로 포만감이 좋다.", new int[,] { { 6, 1 } }); food.sprite = sprites_cook[index++]; items.Add(id, food);
        id = 1202; food = new Food("도로롱", id, 1, 1, 5, "향신료에 재운 양고기를 구워 만든 요리.\n짙은 개성과 깊이 있는 맛을 체감할 수 있다.", new int[,] { { 10, 1 } }); food.sprite = sprites_cook[index++]; items.Add(id, food);



        //더미 드랍 게임 오브젝트 생성
        for (int i = 0; i < 20; i++)
        {
            GameObject prefab = Instantiate(itemPrefab);
            DropItem dropItem = prefab.GetComponent<DropItem>();
            dropItems.Add(dropItem);
            dropItem.transform.parent = poolParent.transform;
            dropItem.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        giveitem(201).transform.position = new Vector3(2, 0, 0);
        giveitem(202).transform.position = new Vector3(3, 3, 0);
        giveitem(203).transform.position = new Vector3(4, 5, 0);
        giveitem(204).transform.position = new Vector3(5, 7, 0);
        giveitem(205).transform.position = new Vector3(10, 0, 0);
        giveitem(205).transform.position = new Vector3(11, 3, 0);
        giveitem(206).transform.position = new Vector3(11, -2, 0);
    }

    public GameObject giveitem(int id)
    {
        foreach (DropItem item in dropItems)
        {
            if (!item.gameObject.activeSelf)
            {
                item.sprite = dropSprites[id - 201];
                item.items = dropDics[id];
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }
        GameObject prefab = Instantiate(itemPrefab);
        DropItem dropItem = prefab.GetComponent<DropItem>();
        dropItems.Add(dropItem);
        dropItem.transform.parent = poolParent.transform;

        dropItem.sprite = dropSprites[id - 201];
        dropItem.items = dropDics[id];
        dropItem.gameObject.SetActive(true);
        return prefab;
    }

    public  Product GetProduct(int id)
    {
        if (items[id] is Equip)
        {
            Equip equip = new Equip((Equip)items[id]);
            return equip;
        }
        else if (items[id] is Food)
        {
            Food food = new Food((Food)items[id]);
            return food;
        }
        Product product = new Product((Product)items[id]);
        return product;
    }

    public Equip GetEquip(int id)
    {
        Equip equip = new Equip((Equip)items[id]);
        return equip;
    }

    public Food GetFood(int id)
    {
        Food food = new Food((Food)items[id]);
        return food;
    }

    public int[,] NeedItem(int id)
    {
        testProd = (Product)items[id];
        return ((Product)items[id]).ingredients;
    }
    public Item GetItem(int id)
    {
        if (items[id] is Equip)
        {
            Equip equip = new Equip((Equip)items[id]);
            return equip;
        }
        else if (items[id] is Food)
        {
            Food food = new Food((Food)items[id]);
            return food;
        }
        else if (items[id] is Product)
        {
            Product product = new Product((Product)items[id]);
            return product;
        }
        Item item = new Item(items[id]);
        return item;
    }
}
