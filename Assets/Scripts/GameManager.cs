using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerController;
    public GameObject resourceManager;
    public GameObject palManager;
    public GameObject itemManager;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Dictionary<int, Item> dictionary = itemManager.GetComponent<ItemManager>().inventory;
            foreach (Item item in dictionary.Values)
            {
                Debug.Log(item);
            }
        }
    }
}
