using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public Dictionary<int,Item> items = new Dictionary<int, Item>();

    public GameObject poolParent;
    public Sprite[] sprites;
    public Sprite[] dropSprites;
    public GameObject itemPrefab;
    public List<DropItem> dropItems = new List<DropItem>();

    public Dictionary<int,List<Item>> dropDics = new Dictionary<int,List<Item>>();
    private void Awake()
    {
        int index = 0;
        int id = 1; Item item = new Item("나무", id, 40, 3, "건축물이나 아이템의 재료로 쓴다.\n주로 나무를 벌목하여 획득한다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("돌", id, 40, 3, "건축물이나 아이템의 재료로 쓴다.\n돌을 채굴하여 획득한다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("팰지움 파편", id, 40, 1, "세계수의 에너지가 결정화한 물건.\n굉장히 특별한 아이템이나 건축물을 만들 수 있다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("금속 광석", id, 40, 8, "화로에서 제련하면 주괴가 된다.\n동굴 같은 곳에서 발견할 수 있다.\n광석을 탐지할 수 있는 팰도 있는 듯하다."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 5; item = new Item("빨간 열매", id, 2, 0.2f, "그냥 먹어도, 구워 먹어도 맛있는 만능 식재료.\n섬을 돌아다니면 쉽게 보인다"); item.sprite = sprites[index++]; items.Add(id, item);
        id = 6; item = new Item("버섯", id, 1, 1f, "그냥 먹어도 되는 버섯.\n섬을 돌아다니다 보면 쉽게 보인다."); item.sprite = sprites[index++]; items.Add(id, item);

        ////DropItemId  
        //나무 드랍 오브젝트 드랍리스트
        id = 201; List<Item> dropList = new List<Item>(); item = GetItem(1); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //돌
        id = 202; dropList = new List<Item>(); item = GetItem(2); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //팰지움 파편
        id = 203; dropList = new List<Item>(); item = GetItem(3); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //팰스피어
        //productDatabase
        //

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



    public Item GetItem(int id)
    {
        Item item = new Item(items[id]);
        return item;
    }
}
