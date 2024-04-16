using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Bricks nowAttackBricks;
    public Vector3 bricsPosition;
    public Equipment equipment;

    public PlayerControll playerControll;

    public float attackDamage = 0;
    public int expCheck;

    private void Awake()
    {
        playerControll = GameManager.Instance.playerController.GetComponent<PlayerControll>();
    }

    public void ReceiveResourceData(Bricks bricks, Equipment equipment, Vector3 position)
    {
        nowAttackBricks = bricks;
        this.equipment = equipment;
        bricsPosition = position;
    }

    public void DataClear()
    {
        nowAttackBricks = null;
        attackDamage = 0;
    }

    public void Attack(float damage)
    {
        if(nowAttackBricks == null)
        {
            playerControll.GiveResourceData();
        }
        else
        {
            bricsPosition =  playerControll.GiveTargetPositionData();
        }
        if(nowAttackBricks != null)
        {
            attackDamage += damage;
            Item giveItem = nowAttackBricks.Attack(damage);
            PlusExp(giveItem.count);
            if (nowAttackBricks.duration < 0)
            {
                if (playerControll.viewdirection == ViewDirection.Left || playerControll.viewdirection == ViewDirection.Right)
                {
                    
                    if (nowAttackBricks.DestroyTile(bricsPosition))
                    {                        
                        InventoryManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.down))
                    {
                        InventoryManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.up))
                    {
                        InventoryManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                }
                else
                {
                    if (nowAttackBricks.DestroyTile(bricsPosition))
                    {
                        InventoryManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.left))
                    {
                        InventoryManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.right))
                    {
                        InventoryManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                }

            }
            else
            {
                InventoryManager.Instance.DropItem(giveItem);
            }
        }

    }

    public void PlusExp(int count)
    {
        expCheck += count;
        if(expCheck > 20)
        {
            GameManager.Instance.GetExp(expCheck / 20);
            expCheck %= 20;
        }
    }
}
