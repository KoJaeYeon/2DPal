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

    private void Start()
    {
        PalBoxManager.Instance.CatchPal(2);
        PalBoxManager.Instance.CatchPal(1);
        //PalBoxManager.Instance.CatchPal(3);
        //PalBoxManager.Instance.CatchPal(4);
        //PalBoxManager.Instance.CatchPal(5);
        //PalBoxManager.Instance.CatchPal(6);
    }

}
