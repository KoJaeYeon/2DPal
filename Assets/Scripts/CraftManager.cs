using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : Singleton<CraftManager>
{
    PlayerControll playerControll;

    List<Furniture> furnitureList;

    private void Awake()
    {
        playerControll = GameManager.Instance.playerController.GetComponent<PlayerControll>();
    }

    private void Start()
    {
        for(int i = 0; i < FurnitureDatabase.Instance.furnitures.Count; i++)
        {
            
        }
    }

    public void FurnitureChoice()
    {
        GameManager.Instance.OnCraft();
        playerControll.statement = Statement.Crafting;
    }
}
