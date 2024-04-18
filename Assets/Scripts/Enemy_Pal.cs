using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Pal : MonoBehaviour
{
    public Pal pal;
    public int id;
    public float percent = 100;

    public float power = 300;

    public List<Item> dropItems;

    TMPro.TextMeshProUGUI levelName;
    public Slider slider;
    public Image fillImage;
    public enum EnemyState { Idle, Battle }
    public EnemyState statement = EnemyState.Idle;

    Rigidbody2D rigid;

    Animator animator;

    Coroutine coroutine;
    public bool attackPlayed;
    public bool canAttack = false;
    private void Awake()
    {

        gameObject.layer = 9;
        levelName = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        slider = GetComponentInChildren<Slider>();
        fillImage = slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        transform.GetComponent<PalAI>().StopAllCoroutines();
        transform.GetChild(0).gameObject.SetActive(true);
        tag = "EnemyPal";

        pal = PalDatabase.Instance.GetPal(id);

        levelName.text = "Lv." + pal.lv + " " + pal.palName;
        slider.value = pal.health / pal.maxHealth;
    }

    private void Update()
    {
        //if ()
        //{
            
        //}
    }

    IEnumerator Rush()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            canAttack = true;
            rigid.AddForce((GameManager.Instance.playerController.transform.position - transform.position).normalized * power);
            yield return new WaitForSeconds(0.5f);
            canAttack = false;
            rigid.velocity = Vector3.zero;
        }
        
    }

    public void Attacked(float damage)
    {
        statement = EnemyState.Battle;
        pal.health -= damage;
        slider.value = pal.health / pal.maxHealth;
        percent = slider.value;
        if(slider.value < 0.2)
        {
            fillImage.color = Color.red;
        }
        else if(slider.value  < 0.5)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.green;
        }
        if (pal.health <= 0) Die();
        if(!attackPlayed)
        {
            attackPlayed = true;
            coroutine = StartCoroutine(Rush());
        }
    }

    public void RushStart()
    {
        attackPlayed = true;
        coroutine = StartCoroutine(Rush());
        statement = EnemyState.Battle;
    }

    public void MoveAgain()
    {
        animator.Play("Down");
    }

    public void Die()
    {
        StopAllCoroutines();
        statement = EnemyState.Idle;
        attackPlayed = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(canAttack)
            {
                GameManager.Instance.GetDamage(pal.attack);
                rigid.velocity = Vector3.zero;
                collision.rigidbody.velocity = Vector3.zero;
                canAttack = false;
            }
        }
    }
}
