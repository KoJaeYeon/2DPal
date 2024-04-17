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

    private void Awake()
    {
        weaponAble = new bool[weaponPrefabs.Length][];
        for(int i = 0; i < weaponAble.Length; i++)
        {
            weaponAble[i] = new bool[4];
        }
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            GameObject[] gameObjects = new GameObject[4];
            for(int j = 0; j < 4; j++)
            {
                gameObjects[j] = Instantiate(weaponPrefabs[i]);
                gameObjects[j].SetActive(false);
            }
            weaponPools.Add(gameObjects);            
        }

        for (int i = 0; i < 4; i++)
        {
            weaponAble[0][i] = true;
        }
        //SetFalse(500);
    }
    public GameObject GetWeapon(int id, int key)
    {
        id -= 500;
        int index = key - 21 + 1;
        for (int i = 0; i < 4; i++)
        {
            if (weaponAble[id][i] == false)
            {
                GameObject weapon = weaponPools[id][i];
                weapon.SetActive(true);

                Transform parent = player.transform.GetChild(index); // 장착할 위치

                int equipedId = parent.GetChild(0).GetComponent<Weapon>().id - 500; // 교체하기 전 무기 제거
                for(int j = 0; j < 4; j++)
                {
                    if(weaponAble[equipedId][j] == true)
                    {
                        weaponAble[equipedId][j] = false;
                        GameObject usedWeapon = parent.transform.GetChild(0).gameObject;
                        usedWeapon.transform.SetParent(poolparent.transform);
                        usedWeapon.gameObject.SetActive(false);
                        break;
                    }
                }
                weaponAble[id][i] = true;
                weapon.transform.SetParent(parent); // 교체
                break;
            }
        }
        GameObject gameObject = new GameObject();
        return gameObject;
    }

    public void SetTrue(int id)
    {
        for (int i = 0; i < 4; i++)
        {
            weaponAble[0][i] = true;
        }
    }

}
