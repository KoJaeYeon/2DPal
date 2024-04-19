using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalDatabase : Singleton<PalDatabase>
{
    public Dictionary<int, Pal> pals = new Dictionary<int, Pal>();
    public List<Sprite> poratraits = new List<Sprite>();

    public GameObject parent_Base;
    public GameObject parent_Wild;
    public GameObject poolParent;
    public List<GameObject>[] palPrefabs = new List<GameObject>[10];
    public List<GameObject>[] palPrefabs_enemy = new List<GameObject>[10];
    public GameObject[] Prefabs = new GameObject[10];
    public GameObject[] Prefabs_enemy = new GameObject[10];
    private void Awake()
    {
        int index = 0;
        int id = 1; Pal pal = new Pal(id, "���η�", 1, 0, 15 ,100, 100, "������� �ȴ� �� ȥ�� �������� ������.\n�ᱹ ���� ���� ���� ���� �� ���� �� ������ óġ�� �� �ִ� ���� �罽�� �������̴�."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 2; pal = new Pal(id, "��γ�", 1, 0, 14 , 100, 100, "���� ���⿣ ��������� ���� ����� �����̴�.\n��γ��� �Ӿ��شٴ°� � �ǹ̿��� �ְ��� �����̴�."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 3; pal = new Pal(id, "������", 1, 0, 13 , 100, 100, "�ʹ��� ���ϰ� �� �ʹ��� ���ִ�.\n���ηտ�(��) �Բ� �־�ü�� ����Ѵ�.\n���� ��Ҵ� ������ �� ��𼱰� Ƣ��´�."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 4; pal = new Pal(id, "ť�긮��", 1,12, 0, 100, 100, "5~7�� ������ ������ �ִ�.\n��Ʈ�ʿ������� ���� ���� ���� ��� ��ü�� ������ ������ ��ϵ� �Ϻ� �����Ѵ�."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 5; pal = new Pal(id, "����ȣ", 1, 0,18, 100, 100, "�¾ ���Ŀ� ���� �� �� �ٷＭ �����ϸ� ���� �մٰ� ���� Ź ������.\n����ȣ�� ��ä��� �긲 ȭ���� ������ �ȴ�."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 6; pal = new Pal(id, "û�θ�", 1, 0,20, 100, 100, "�ڽ��� ź���� ������ ������ ������ ����Ų��.\n���� ���� ������ ������ Ÿ�� �̵��Ѵ�.\n����� ���� ���� ���� �ε��� �״´�."); pal.portrait = poratraits[index++]; pals.Add(id, pal);

        for (int i = 0; i < pals.Count; i++)
        {
            GameObject prefab = Instantiate(Prefabs[i]);
            palPrefabs[i] = new List<GameObject>();
            palPrefabs[i].Add(prefab);
            prefab.transform.SetParent(poolParent.transform);
            prefab.SetActive(false);
        }
        for (int i = 0; i < pals.Count; i++)
        {
            GameObject prefab_enemy = Instantiate(Prefabs_enemy[i]);
            palPrefabs_enemy[i] = new List<GameObject>();
            palPrefabs_enemy[i].Add(prefab_enemy);
            prefab_enemy.transform.SetParent(poolParent.transform);
            prefab_enemy.SetActive(false);
        }
    }

    public GameObject GivePal(int id, bool basePal = true)
    {
        id %= 100;
        List<GameObject> list;
        if (basePal) list = palPrefabs[id-1];
        else list = palPrefabs_enemy[id - 1]; ;
        foreach (GameObject pal in list)
        {
            if (!pal.activeSelf)
            {
                pal.SetActive(true);
                return pal;
            }
        }
        if (basePal)
        {
            GameObject prefab = Instantiate(Prefabs[id - 1]);
            list.Add(prefab);
            return prefab;
        }
        else
        {
            GameObject prefab = Instantiate(Prefabs_enemy[id - 1]);
            list.Add(prefab);
            return prefab;
        }

    }

    private void Start()
    {
        GivePal(1, false);
        GivePal(1, false);
    }

    public Pal GetPal(int id)
    {
        return new Pal(pals[id]);
    }
}
