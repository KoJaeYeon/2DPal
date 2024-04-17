using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int weapon_damage = 100;

    [SerializeField] void Attack()
    {
        ResourceManager.Instance.Attack(weapon_damage);
    }
}
