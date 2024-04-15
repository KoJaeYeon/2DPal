using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_Pal;

public class Enemy_Pal : MonoBehaviour
{
    public Pal pal;
    public int id;
    private void Start()
    {
        pal = PalDatabase.Instance.GetPal(id);
    }
    public enum EnemyState {Idle, Battle}
    public EnemyState statement = EnemyState.Idle;
}
