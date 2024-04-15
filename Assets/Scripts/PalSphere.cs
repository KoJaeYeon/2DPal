using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphere : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    Rigidbody2D rb;
    Coroutine coroutine;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed / 30);
    }

    public void SetRotateSpeed(float throwPower)
    {
        rotateSpeed = throwPower;
    }
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(1);
        Vector3 dir = rb.velocity.normalized;
        rb.AddForce(-dir * rotateSpeed  / 2);
        yield return new WaitForSeconds(1);
        rb.AddForce(-dir * rotateSpeed  / 2);
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        yield break;
    }
    public void Go()
    {
        coroutine = StartCoroutine(Disapear());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyPal"))
        {
            StopCoroutine(coroutine);
            rb.velocity = Vector2.zero;
            BattleManager.Instance.Captured(this,collision.gameObject);
            rotateSpeed = 50;
        }
    }
}
