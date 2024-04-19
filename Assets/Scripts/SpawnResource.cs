using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class SpawnResource : MonoBehaviour
{
    Tilemap tilemap;
    public Tile tile;

    private void Awake()
    {
        tilemap = transform.parent.GetComponent<Tilemap>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetTile());
    }

    IEnumerator SetTile()
    {
        while (true)
        {

            for(int i = 0; i < transform.childCount; i++)
            {
                Vector3 randPos = new Vector3(Random.Range(-1, 1.5f), Random.Range(-1, 1.5f));
                Vector3 newpos = transform.GetChild(i).position + randPos;
                Collider2D[] cols = Physics2D.OverlapCircleAll(newpos, 1.5f);
                {
                    if (cols.Length == 0)
                    {
                        Vector3Int cellPostion = tilemap.WorldToCell(newpos);
                        if (tilemap.GetTile(cellPostion) == null) tilemap.SetTile(cellPostion, tile);
                    }
                }
            }


            yield return new WaitForSeconds(300);
        }
        
    }
}
