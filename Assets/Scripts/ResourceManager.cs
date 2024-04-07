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
                        ItemManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.down))
                    {
                        Debug.Log("down");
                        ItemManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.up))
                    {
                        Debug.Log("up");
                        ItemManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                }
                else
                {
                    if (nowAttackBricks.DestroyTile(bricsPosition))
                    {
                        ItemManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.left))
                    {
                        ItemManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                    else if (nowAttackBricks.DestroyTile(bricsPosition + Vector3.right))
                    {
                        ItemManager.Instance.DropItem(giveItem);
                        DataClear();
                    }
                }

            }
            else
            {
                ItemManager.Instance.DropItem(giveItem);
            }
        }

    }
    //정보 없으면 데이터 다시 받아오기
}
