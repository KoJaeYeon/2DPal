using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    bool isFreeBuilding = true;
    bool isContact = false;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2d;
    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isFreeBuilding = true;
        isContact = false;
        boxCollider2d.isTrigger = true;
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public bool ContactCheck()
    {
        if (isContact) return isContact;
        spriteRenderer.color = Color.white;
        isFreeBuilding = false;
        boxCollider2d.isTrigger = false;
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isFreeBuilding) return;
        if (!isContact)
        {
            spriteRenderer.color = Color.red;
            isContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isFreeBuilding) return;
        isContact = false;
        spriteRenderer.color = Color.green;
    }
}
