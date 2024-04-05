using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject prefab;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void DestroyTile(Vector3 pos)
    {
        float x, y;
        //x = Mathf.Round(pos.x);
        //y = Mathf.Round(pos.y);
        Vector3Int cellPostion = tilemap.WorldToCell(pos);
        tilemap.SetTile(cellPostion,null);
    }

}
