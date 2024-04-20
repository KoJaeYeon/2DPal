using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Equipment
{
    None = 0,
    Pickage = 1,
    Axe = 2,
    Sword = 3,
    Gun = 4,
    Bow = 5
}
public class Weapon : MonoBehaviour
{
    public int id;
    public int weapon_damage = 10;

    public int list;
    public bool canAttack = false;
    public Equipment equip;

    [SerializeField] void Mining()
    {
        ResourceManager.Instance.Attack(weapon_damage);
        canAttack = false;
    }    

    [SerializeField] void CanAttackTrue()
    {
        canAttack = true;
    }
    [SerializeField]
    void CanAttackFalse()
    {
        canAttack = false;
    }
    public void Attack(Collider2D collision)
    {
        if (canAttack)
        {
            canAttack = false;
            Enemy_Pal enemy_Pal = collision.GetComponent<Enemy_Pal>();
            enemy_Pal.Attacked(weapon_damage);
        }
    }

}
