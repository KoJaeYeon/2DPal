using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTarget : MonoBehaviour
{
    PlayerControll playerControll;

    private void Awake()
    {
        playerControll = transform.parent.GetComponent<PlayerControll>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("oncollision");
        playerControll.target = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerControll.target = null;
    }
}
