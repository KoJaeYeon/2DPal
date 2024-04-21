using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    Weapon weapon;
    private void Awake()
    {
        weapon = transform.parent.GetComponent<Weapon>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("EnemyPal")) return;
        weapon.Attack(collision);
    }
}
