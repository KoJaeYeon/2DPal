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
    public GameObject[] Prefabs = new GameObject[10];
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
            prefab.transform.parent = poolParent.transform;
            prefab.SetActive(false);
        }
    }

    public GameObject GivePal(int id, bool basePal = true)
    {
        id %= 100;
        foreach (GameObject pal in palPrefabs[id - 1])
        {
            if (!pal.activeSelf)
            {
                if (basePal)
                {
                    pal.GetComponent<PalAI>().enabled = true;
                    pal.GetComponent<Enemy_Pal>().enabled = false;
                    
                }
                else
                {
                    pal.GetComponent<PalAI>().enabled = false;
                    pal.GetComponent<Enemy_Pal>().enabled = true;
                }
                pal.SetActive(true);
                return pal;
            }
        }
        GameObject prefab = Instantiate(Prefabs[id - 1]);
        palPrefabs[id - 1].Add(prefab);
        if (basePal)
        {
            prefab.GetComponent<PalAI>().enabled = true;
            prefab.GetComponent<Enemy_Pal>().enabled = false;
            prefab.transform.GetChild(0).gameObject.SetActive(false);
            prefab.layer = 8;
        }
        else
        {
            prefab.GetComponent<PalAI>().enabled = true;
            prefab.GetComponent<Enemy_Pal>().enabled = true;
            prefab.transform.GetChild(0).gameObject.SetActive(true);
            prefab.tag = "EnemyPal";
            prefab.layer = 9;
        }
        return prefab;
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
