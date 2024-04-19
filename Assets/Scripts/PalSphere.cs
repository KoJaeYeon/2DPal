using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PalSphere : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    Rigidbody2D rb;
    Coroutine coroutine;

    Enemy_Pal enemy_Pal;
    public Animator animator_e;
    public Animator animator_p;
    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.GetChild(0).Rotate(Vector3.forward * rotateSpeed / 30);
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
            Captured(collision.gameObject);
            rotateSpeed = 50;
        }
    }
    public void Captured(GameObject enemy_Pal)
    {
        this.enemy_Pal = enemy_Pal.GetComponent<Enemy_Pal>();
        this.enemy_Pal.statement = Enemy_Pal.EnemyState.Idle;
        animator_e = this.enemy_Pal.GetComponent<Animator>();
        animator_p = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator_e.Play("Pal_Small");
        Check(0);
        transform.GetChild(1).gameObject.SetActive(true);
        this.enemy_Pal.StopAllCoroutines();
        this.enemy_Pal.attackPlayed = false;
    }

    public bool Check(int tryCount)
    {
        if (tryCount == 3)
        {
            Debug.Log("Caputre!!!");
            enemy_Pal.gameObject.SetActive(false);
            PalBoxManager.Instance.CatchPal(enemy_Pal.pal.id);
            enemy_Pal.transform.SetParent(PalDatabase.Instance.poolParent.transform);
            gameObject.SetActive(false);
            return true;
        }
        else if (Random.Range(0, 100) > (50 + (enemy_Pal.percent * 0.6f) - (tryCount + 1) * 20) - 20)
        {
            animator_p.Play("Capture" + tryCount);
            float percent = 100 - (50 + (enemy_Pal.percent * 0.6f) - (tryCount + 1) * 20 - 20);
            percent = percent > 100 ? 100 : percent;
            percent = percent < 0 ? 10 : percent;
            textMeshProUGUI.text = percent.ToString("0");
            return true;
        }
        else
        {
            animator_e.Play("Pal_Big");
            enemy_Pal.RushStart();
            gameObject.SetActive(false);
            return false;
        }
    }
}
