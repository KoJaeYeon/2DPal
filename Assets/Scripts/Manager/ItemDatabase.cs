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
        int id = 1; Item item = new Item("����", id, 40, 3, "���๰�̳� �������� ���� ����.\n�ַ� ������ �����Ͽ� ȹ���Ѵ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 2; item = new Item("��", id, 40, 3, "���๰�̳� �������� ���� ����.\n���� ä���Ͽ� ȹ���Ѵ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 3; item = new Item("������ ����", id, 40, 1, "������� �������� ����ȭ�� ����.\n������ Ư���� �������̳� ���๰�� ���� �� �ִ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 4; item = new Item("�ݼ� ����", id, 40, 8, "ȭ�ο��� �����ϸ� �ֱ��� �ȴ�.\n���� ���� ������ �߰��� �� �ִ�.\n������ Ž���� �� �ִ� �ӵ� �ִ� ���ϴ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 5; Food food = new Food("���� ����", id, 2, 0.2f, 0, "�׳� �Ծ, ���� �Ծ ���ִ� ���� �����.\n���� ���ƴٴϸ� ���� ���δ�"); food.sprite = sprites[index++]; items.Add(id, food);
        id = 6; food = new Food("����", id, 1, 1f, 0, "�׳� �Ծ �Ǵ� ����.\n���� ���ƴٴϴ� ���� ���� ���δ�."); food.sprite = sprites[index++]; items.Add(id, food);
        id = 7; item = new Item("���� ��", id, 2, 0.05f, "���� ������(��) ����� �� �ִ� ��.\n����� ���� �� �ʿ��ϴ�.\n���� ������(��) ä���� �� ���� �� �ִ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 8; item = new Item("����", id, 4, 0.5f, "�������� ��� ����.\nȰ ���� �����ϴ� ���簡 �ȴ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 9; item = new Item("����", id, 2, 1f, "�� �ӿ��Լ� ��� ����.\nħ�볪 õ��(��) ���� �� �ʿ��ϴ�."); item.sprite = sprites[index++]; items.Add(id, item);
        id = 10; food = new Food("���η�", id, 1, 1f, 0, "���η��� ���.\n��Ư�� ǳ�̰� �ŷ����� ���� ���ڱ�. �⳻�� ������ ���� ����."); food.sprite = sprites[index++]; items.Add(id, food);


        index = 0;
        id = 1000; Product product = new Product("�� ���Ǿ�", id, 1, 0, 5, "������ ���� ��ȹ�ϴ� ����", new int[,] { { 1, 3 }, { 2, 3 }, { 3, 1 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1001; product = new Product("ȭ��", id, 3, 0.02f, 10, "Ȱ�� �߻��ϴ� ȭ��.", new int[,] { { 1, 1 }, { 2, 1 } }); product.sprite = sprites_product[index++]; items.Add(id, product);
        id = 1002; product = new Product("õ", id, 1, 1, 5, "������ ¥�� ���� õ.\n���� ���� �� �ʿ��ϴ�.", new int[,] { { 9, 2 } }); product.sprite = sprites_product[index++]; items.Add(id, product);

        index = 0;
        id = 501; Equip equip = new Equip("���� ���", id, 1, 10, 30, "���� ������ ���� ���.\n�Ӱ� �ο�⿣ �� �Ҿ��ϴ�.", new int[,] { { 1, 5 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 502; equip = new Equip("�޴��� ȶ��", id, 1, 5, 20, "��� ������ �ֺ��� ȯ������ �޴��� ȶ��.\n��� ���� ���� ������ �����ϴ�.", new int[,] { { 1, 2 }, { 2, 2 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 503; equip = new Equip("�� ���", id, 1, 10, 30, "ä���� ���.\n���� ����� ȿ���� ��������.", new int[,] { { 1, 5 }, { 2, 5 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 504; equip = new Equip("�� ����", id, 1, 10, 30, "������ ���� �뵵�� ����.\n���� ���� ������� ���� �ƽ���.", new int[,] { { 1, 5 }, { 2, 5 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);
        id = 505; equip = new Equip("������ Ȱ", id, 1, 6, 20, "���Ÿ� ������ ������ Ȱ.\n�̰����� �ܾ��� ���� ſ�� ������ ��������.", new int[,] { { 1, 30 }, { 2, 5 }, { 8, 15 } }); equip.sprite = sprites_equip[index++]; items.Add(id, equip);

        ////DropItemId
        //��� ������Ʈ �������Ʈ
        //����
        id = 201; List<Item> dropList = new List<Item>(); item = GetItem(1); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //��
        id = 202; dropList = new List<Item>(); item = GetItem(2); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //������ ����
        id = 203; dropList = new List<Item>(); item = GetItem(3); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //�ӽ��Ǿ�
        id = 204; dropList = new List<Item>(); product = GetProduct(1000); product.count = 1; dropList.Add(product); dropDics.Add(id, dropList);
        //��������
        id = 205; dropList = new List<Item>(); food = GetFood(5); food.count = 4; dropList.Add(food); item = GetItem(7); item.count = 1; dropList.Add(item); dropDics.Add(id, dropList);
        //����
        id = 206; dropList = new List<Item>(); food = GetFood(6); food.count = 4; dropList.Add(food); dropDics.Add(id, dropList);


        //�� �� �������Ʈ
        id = 1; dropList = new List<Item>(); food = GetFood(10); food.count = 1; dropList.Add(food); item = GetItem(9); item.count = 2; dropList.Add(item); dropDics.Add(id, dropList);


        index = 0;
        id = 1100; product = new Product("�ݼ� �ֱ�", id, 1, 0.15f, 10, "�ݼ��� ���⳪ ���� ���� �� �ʿ��� ����.\n�ݼ� ������ ȭ�ο� �����ؼ� �����.", new int[,] { { 4, 2 } }); product.sprite = sprites_fire[index++]; items.Add(id, product);

        index = 0;
        id = 1200; food = new Food("���� ����", id, 1, 0.15f, 5, "����� ���簡�� ��������.\n�ű��� ������ �������� ����.", new int[,] { { 5, 1 } }); food.sprite = sprites_cook[index++]; items.Add(id, food);
        id = 1201; food = new Food("���� ����", id, 1, 1, 5, "����� ���簡�� �������� ����.\n�ű��� ������ �������� ����.", new int[,] { { 6, 1 } }); food.sprite = sprites_cook[index++]; items.Add(id, food);
        id = 1202; food = new Food("���η�", id, 1, 1, 5, "��ŷῡ ��� ���⸦ ���� ���� �丮.\n£�� ������ ���� �ִ� ���� ü���� �� �ִ�.", new int[,] { { 10, 1 } }); food.sprite = sprites_cook[index++]; items.Add(id, food);



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
