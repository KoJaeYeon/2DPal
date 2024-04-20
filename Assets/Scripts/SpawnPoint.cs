using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int maxPal;
    public int palId;
    public int palLevel;
    public List<GameObject> pals;
    bool start = false;

    private void Awake()
    {
        pals = new List<GameObject>();
        Destroy(transform.GetChild(0).gameObject);
    }
    private void OnEnable()
    {
        if (start)
        {
            SpawnPal();
            for(int i = 0; i < pals.Count; i++)
            {
                pals[i].transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
            }

        }
    }

    private void Start()
    {
        start = true;
        SpawnPal();
    }

    public void SpawnPal()
    {
        for(int i = 0; i < maxPal; i++)
        {
            if(transform.childCount != 3)
            {
                GameObject pal = PalDatabase.Instance.GivePal(palId, false);
                Enemy_Pal enemy_Pal = pal.GetComponent<Enemy_Pal>();
                enemy_Pal.pal = PalDatabase.Instance.GetPal(palId);
                enemy_Pal.pal.LevelUp(palLevel);
                enemy_Pal.Status();
                enemy_Pal.statement = Enemy_Pal.EnemyState.Idle;
                pals.Add(pal);
                pal.transform.SetParent(transform);
                pal.transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
            }
        }
    }
}
