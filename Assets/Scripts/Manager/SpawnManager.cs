using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    GameObject[] spawnPoint;
    public GameObject pointer;
    int index = 0;

    Coroutine coroutine;
    private void Start()
    {
        coroutine = StartCoroutine(ActivePoint());
        spawnPoint = new GameObject[pointer.transform.childCount];
        int length = spawnPoint.Length;
        for (int i = 0; i < length; i++)
        {
            spawnPoint[i] = pointer.transform.GetChild(i).gameObject;
        }
    }

    IEnumerator ActivePoint()
    {
        while (true)
        {
            SearchPoint();
            yield return new WaitForSeconds(15);
            index++;
            if(index >= 8)
            {
                SearchPoint(true);
                yield return new WaitForSeconds(15);
                index = 0;
            }

        }
    }
    
    private void SearchPoint(bool set = false)
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            Vector3 offset = spawnPoint[i].transform.position - GameManager.Instance.playerController.transform.position;
            float distance = Vector2.SqrMagnitude(offset);
            if(distance < 5000)
            {
                spawnPoint[i].SetActive(true);
                if (true) spawnPoint[i].GetComponent<SpawnPoint>().SpawnPal();
            }
            else
            {
                spawnPoint[i].SetActive(false);
            }
        }
    }

}
