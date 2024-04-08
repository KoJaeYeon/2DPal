using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : Singleton<CraftManager>
{
    PlayerControll playerControll;
    private void Awake()
    {
        playerControll = GameManager.Instance.playerController.GetComponent<PlayerControll>();
    }
    public void FurnitureChoice()
    {
        GameManager.Instance.OnCraft();
        playerControll.statement = Statement.Crafting;
    }
}
