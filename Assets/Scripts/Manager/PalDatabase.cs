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
        int id = 1; Pal pal = new Pal(id, "도로롱", 1, 0, 15 ,100, 100, "언덕길을 걷다 제 혼자 데굴데굴 구른다.\n결국 눈이 핑핑 돌아 몸을 못 가눌 때 간단히 처치할 수 있는 먹이 사슬의 최하층이다."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 2; pal = new Pal(id, "까부냥", 1, 0, 14 , 100, 100, "얼핏 보기엔 당당하지만 실은 대단한 겁쟁이다.\n까부냥이 핥아준다는건 어떤 의미에선 최고의 굴욕이다."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 3; pal = new Pal(id, "꼬꼬닭", 1, 0, 13 , 100, 100, "너무나 약하고 또 너무나 맛있다.\n도로롱와(과) 함께 최약체를 담당한다.\n많이 잡았다 싶으면 또 어디선가 튀어나온다."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 4; pal = new Pal(id, "큐룰리스", 1,12, 0, 100, 100, "5~7세 정도의 지능이 있다.\n파트너용이지만 무기 쓰는 법을 배운 개체가 주인을 살해한 기록도 일부 존재한다."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 5; pal = new Pal(id, "파이호", 1, 0,18, 100, 100, "태어난 직후엔 불을 잘 못 다뤄서 걸핏하면 불을 뿜다가 숨이 탁 막힌다.\n파이호의 재채기는 산림 화재의 원인이 된다."); pal.portrait = poratraits[index++]; pals.Add(id, pal);
        id = 6; pal = new Pal(id, "청부리", 1, 0,20, 100, 100, "자신이 탄생한 물에선 어디든지 물결을 일으킨다.\n급할 때는 몸으로 물살을 타고 이동한다.\n기운이 넘쳐 종종 벽에 부딪혀 죽는다."); pal.portrait = poratraits[index++]; pals.Add(id, pal);

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
