using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public GameObject sphereParent;

    public GameObject palSpherePrefab;

    public Queue<GameObject> palSpheres;
    private int index = 0;
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

    public void ThorwSphere(float throwPower)
    {

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

}
