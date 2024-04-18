using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public GameObject[] weaponPrefabs = new GameObject[6];
    public List<GameObject[]> weaponPools = new List<GameObject[]>();
    public bool[][] weaponAble;
    public GameObject poolparent;
    public GameObject player;
    public PlayerControll playerControll;

    public GameObject[] WeaponIndex = new GameObject[4];
    public bool[] debugAble0;
    public bool[] debugAble1;
    public bool actived;

    private void Awake()
    {
        playerControll = player.GetComponent<PlayerControll>();

        for(int i = 0; i < 4; i++)
        {
            WeaponIndex[i] = player.transform.GetChild(i+1).gameObject;
        }

        weaponAble = new bool[weaponPrefabs.Length][];
        for(int i = 0; i < weaponAble.Length; i++)
        {
            weaponAble[i] = new bool[5];
        }
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            GameObject[] gameObjects = new GameObject[5];
            for(int j = 0; j < 5; j++)
            {
                gameObjects[j] = Instantiate(weaponPrefabs[i]);
                gameObjects[j].SetActive(false);
                gameObjects[j].transform.SetParent(poolparent.transform);
                Weapon weapon = gameObjects[j].GetComponent<Weapon>();
                weapon.list = j;
            }
            weaponPools.Add(gameObjects);            
        }
    }

    private void Start()
    {
        Equip(500, 21);
        Equip(500, 22);
        Equip(500, 23);
        Equip(500, 24);
        weaponPools[0][0].gameObject.SetActive(true);
    }
    public void Equip(int id, int key)
    {
        id -= 500;
        int index = key - 21;

        UnEquip(index); //기존 위치 장착 해제
        
        for(int i = 0; i < 5; i++)
        {            
            if (weaponAble[id][i] == false) // 사용 가능이면
            {
                weaponAble[id][i] = true; // 사용 중으로 변경
                GameObject weapon = weaponPools[id][i]; // 사용할 무기 받아오기
                weapon.transform.SetParent(WeaponIndex[index].transform); // 플레이어에게 무기 부여
                if(actived) weapon.gameObject.SetActive(true); // 사용중인 무기였으면 사용중으로 돌려주기
                playerControll.equip[index] = weapon.gameObject; // 장비변경
                playerControll.animator_Equip[index] = weapon.transform.GetComponent<Animator>(); // 애니메이션 변경
                debugAble0 = weaponAble[0];
                debugAble1 = weaponAble[1];
                break;
            }
        }
        
    }

    public void UnEquip(int index)
    {
        if (WeaponIndex[index].transform.childCount == 0) { return; } // 최초 실행시 해제할 장비가 없음
        actived = WeaponIndex[index].transform.GetChild(0).gameObject.activeSelf;
        WeaponIndex[index].transform.GetChild(0).gameObject.SetActive(true); // 비활성화시 받아오기 위한 활성화
        Weapon weapon =  WeaponIndex[index].GetComponentInChildren<Weapon>(); // ID 받아오기 위한 컴포넌트
        int id = weapon.id;
        int list = weapon.list;
        weapon.transform.SetParent(poolparent.transform);
        weapon.gameObject.SetActive(false); // 무기 비활성화
        SetFalse(id, list); // 무기 회수로 인한 대기상태
    }

    public void SetFalse(int id, int list) // 해당하는 무기위치 비활성화
    {
        weaponAble[id - 500][list] = false;
    }
}
