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
    //정보 없으면 데이터 다시 받아오기
}
