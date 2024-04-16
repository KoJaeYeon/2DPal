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
        int id = 1; Item item = new Item("����", id, 40, 3, "���๰�̳� �������� ���� ����.\n�ַ� ������ �����Ͽ� ȹ���Ѵ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("��", id, 40, 3, "���๰�̳� �������� ���� ����.\n���� ä���Ͽ� ȹ���Ѵ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("������ ����", id, 40, 1, "������� �������� ����ȭ�� ����.\n������ Ư���� �������̳� ���๰�� ���� �� �ִ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("�ݼ� ����", id, 40, 8, "ȭ�ο��� �����ϸ� �ֱ��� �ȴ�.\n���� ���� ������ �߰��� �� �ִ�.\n������ Ž���� �� �ִ� �ӵ� �ִ� ���ϴ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 5; item = new Item("���� ����", id, 2, 0.2f, "�׳� �Ծ, ���� �Ծ ���ִ� ���� �����.\n���� ���ƴٴϸ� ���� ���δ�"); item.sprite = sprites[index++]; items.Add(id, item);
        id = 6; item = new Item("����", id, 1, 1f, "�׳� �Ծ �Ǵ� ����.\n���� ���ƴٴϴ� ���� ���� ���δ�."); item.sprite = sprites[index++]; items.Add(id, item);

        ////DropItemId  
        //���� ��� ������Ʈ �������Ʈ
        id = 201; List<Item> dropList = new List<Item>(); item = GetItem(1); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //��
        id = 202; dropList = new List<Item>(); item = GetItem(2); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //������ ����
        id = 203; dropList = new List<Item>(); item = GetItem(3); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //�ӽ��Ǿ�
        //productDatabase
        //

        //���� ��� ���� ������Ʈ ����
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
