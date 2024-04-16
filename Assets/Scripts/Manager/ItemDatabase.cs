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
    public Sprite[] sprites_fire;
    public Sprite[] sprites_cook;

    public Dictionary<int,List<Item>> dropDics = new Dictionary<int,List<Item>>();

    public Product testProd;
    private void Awake()
    {
        int index = 0;
        int id = 1; Item item = new Item("����", id, 40, 3, "���๰�̳� �������� ���� ����.\n�ַ� ������ �����Ͽ� ȹ���Ѵ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("��", id, 40, 3, "���๰�̳� �������� ���� ����.\n���� ä���Ͽ� ȹ���Ѵ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("������ ����", id, 40, 1, "������� �������� ����ȭ�� ����.\n������ Ư���� �������̳� ���๰�� ���� �� �ִ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("�ݼ� ����", id, 40, 8, "ȭ�ο��� �����ϸ� �ֱ��� �ȴ�.\n���� ���� ������ �߰��� �� �ִ�.\n������ Ž���� �� �ִ� �ӵ� �ִ� ���ϴ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 5; item = new Item("���� ����", id, 2, 0.2f, "�׳� �Ծ, ���� �Ծ ���ִ� ���� �����.\n���� ���ƴٴϸ� ���� ���δ�"); item.sprite = sprites[index++]; items.Add(id, item);
        id = 6; item = new Item("����", id, 1, 1f, "�׳� �Ծ �Ǵ� ����.\n���� ���ƴٴϴ� ���� ���� ���δ�."); item.sprite = sprites[index++]; items.Add(id, item);

        index = 0;
        id = 1001; Product product = new Product("�� ���Ǿ�", id, 1, 0, 5, "������ ���� ��ȹ�ϴ� ����", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1002; product = new Product("���� ���", id, 1, 10, 30, "���� ������ ���� ���.\n�Ӱ� �ο�⿣ �� �Ҿ��ϴ�.", new int[,] { { 1, 5 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1003; product = new Product("�޴��� ȶ��", id, 1, 5, 20, "��� ������ �ֺ��� ȯ������ �޴��� ȶ��.\n��� ���� ���� ������ �����ϴ�.", new int[,] { { 1, 2 }, { 2, 2 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1004; product = new Product("�� ���", id, 1, 10, 30, "�� ��̴�.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1005; product = new Product("�� ����", id, 1, 10, 30, "�� ������.", new int[,] { { 1, 5 }, { 2, 5 } }); product.sprite = sprites_product[index++]; items.Add(id, product);

        ////DropItemId  
        //���� ��� ������Ʈ �������Ʈ
        id = 201; List<Item> dropList = new List<Item>(); item = GetItem(1); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //��
        id = 202; dropList = new List<Item>(); item = GetItem(2); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //������ ����
        id = 203; dropList = new List<Item>(); item = GetItem(3); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //�ӽ��Ǿ�
        id = 204; dropList = new List<Item>(); product = GetProduct(1001); product.count = 1; dropList.Add(item); dropDics.Add(id, dropList);

        index = 0;
        id = 1100; product = new Product("�ݼ� �ֱ�", id, 1, 0.15f, 10, "�ݼ��� ���⳪ ���� ���� �� �ʿ��� ����.\n�ݼ� ������ ȭ�ο� �����ؼ� �����.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; items.Add(id, product);
        index = 0;
        id = 1200; product = new Product("���� ����", id, 1, 0.15f, 5, "����� ���簡�� ��������.\n�ű��� ������ �������� ����.", new int[,] { { 5, 1 } }); product.sprite = sprites_cook[index++]; items.Add(id, product);
        id = 1201; product = new Product("���� ����", id, 1, 1, 5, "����� ���簡�� �������� ����.\n�ű��� ������ �������� ����.", new int[,] { { 6, 1 } }); product.sprite = sprites_cook[index++]; items.Add(id, product);

        

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

    public  Product GetProduct(int id)
    {
        Product product = new Product((Product)ItemDatabase.Instance.items[id]);
        return product;
    }

    public int[,] NeedItem(int id)
    {
        testProd = (Product)items[id];
        return ((Product)items[id]).ingredients;
    }
    public Item GetItem(int id)
    {
        Item item = new Item(items[id]);
        return item;
    }
}
