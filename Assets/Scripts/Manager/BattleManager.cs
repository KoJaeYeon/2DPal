using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public GameObject sphereParent;

    public GameObject palSpherePrefab;

    public Queue<GameObject> palSpheres;

    private int index = 0;

    public Material[] sphere_Material = new Material[3];

    public PalSphere palSphere;
    public Enemy_Pal enemy_Pal;
    public Animator animator_e;
    public Animator animator_p;
    public SpriteRenderer spriteRenderer;
    public int id;
    private void Awake()
    {
        palSpheres = new Queue<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject prefab = Instantiate(palSpherePrefab);
            palSpheres.Enqueue(prefab);
            prefab.transform.parent = sphereParent.transform;
            prefab.name = "PalSphere" + index++;
            prefab.SetActive(false);
        }
    }
    public GameObject GiveSphere()
    {
        GameObject sphere = palSpheres.Dequeue();
        if (!sphere.activeSelf)
        {
            palSpheres.Enqueue(sphere);
            sphere.SetActive(true);
            return sphere;
        }
        GameObject prefab = Instantiate(palSpherePrefab);
        palSpheres.Enqueue(prefab);
        prefab.transform.parent = sphereParent.transform;
        prefab.name = "PalSphere" + index++;

        return prefab;
    }

    public void Captured(PalSphere palSphere, GameObject enemy_Pal)
    {
        this.palSphere = palSphere;
        this.enemy_Pal = enemy_Pal.GetComponent<Enemy_Pal>();
        this.enemy_Pal.statement = Enemy_Pal.EnemyState.Idle;
        animator_e = this.enemy_Pal.GetComponent<Animator>();
        animator_p = palSphere.GetComponentInChildren<Animator>();
        spriteRenderer = palSphere.GetComponent<SpriteRenderer>();
        Debug.Log("caputured");
        animator_e.Play("Pal_Small");
        Check(0);
    }
    private void Start()
    {
        PalBoxManager.Instance.CatchPal(2);
        PalBoxManager.Instance.CatchPal(1);
        PalBoxManager.Instance.CatchPal(3);
        PalBoxManager.Instance.CatchPal(4);
        PalBoxManager.Instance.CatchPal(5);
        PalBoxManager.Instance.CatchPal(6);
    }
    public bool Check(int tryCount)
    {
        if(tryCount == 3)
        {
            Debug.Log("Caputre!!!");
            enemy_Pal.gameObject.SetActive(false);
            palSphere.gameObject.SetActive(false);
            PalBoxManager.Instance.CatchPal(enemy_Pal.pal.id);
            return true;
        }
        else if(Random.Range(0,100) > 10)
        {
            animator_p.Play("Capture" + tryCount);
            Debug.Log(true);
            return true;
        }
        else
        {
            animator_e.Play("Pal_Big");
            palSphere.gameObject.SetActive(false);
            Debug.Log(false);
            return false;
        }
    }
}
