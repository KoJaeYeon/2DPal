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

        UnEquip(index); //���� ��ġ ���� ����
        
        for(int i = 0; i < 5; i++)
        {            
            if (weaponAble[id][i] == false) // ��� �����̸�
            {
                weaponAble[id][i] = true; // ��� ������ ����
                GameObject weapon = weaponPools[id][i]; // ����� ���� �޾ƿ���
                weapon.transform.SetParent(WeaponIndex[index].transform); // �÷��̾�� ���� �ο�
                if(actived) weapon.gameObject.SetActive(true); // ������� ���⿴���� ��������� �����ֱ�
                playerControll.equip[index] = weapon.gameObject; // ��񺯰�
                playerControll.animator_Equip[index] = weapon.transform.GetComponent<Animator>(); // �ִϸ��̼� ����
                debugAble0 = weaponAble[0];
                debugAble1 = weaponAble[1];
                break;
            }
        }
        
    }

    public void UnEquip(int index)
    {
        if (WeaponIndex[index].transform.childCount == 0) { return; } // ���� ����� ������ ��� ����
        actived = WeaponIndex[index].transform.GetChild(0).gameObject.activeSelf;
        WeaponIndex[index].transform.GetChild(0).gameObject.SetActive(true); // ��Ȱ��ȭ�� �޾ƿ��� ���� Ȱ��ȭ
        Weapon weapon =  WeaponIndex[index].GetComponentInChildren<Weapon>(); // ID �޾ƿ��� ���� ������Ʈ
        int id = weapon.id;
        int list = weapon.list;
        weapon.transform.SetParent(poolparent.transform);
        weapon.gameObject.SetActive(false); // ���� ��Ȱ��ȭ
        SetFalse(id, list); // ���� ȸ���� ���� ������
    }

    public void SetFalse(int id, int list) // �ش��ϴ� ������ġ ��Ȱ��ȭ
    {
        weaponAble[id - 500][list] = false;
    }
}
