using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum ViewDirection
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public enum Equipment
{
    None = 0,
    Pickage = 1,
    Axe = 2,
    Sword = 3,
    Gun = 4,
    Bow = 5
}
public class PlayerControll : MonoBehaviour
{
    Vector2 direction;
    public ViewDirection viewdirection;
    public GameObject target; // 콜라이더 오브젝트
    public Transform GetTarget;
    public bool running = false;
    public bool fire = false;

    public float speed = 0.01f;
    public float run = 1;

    Animator animator;

    public GameObject[] equip;
    public Animator[] animator_Equip;
    public Equipment nowEquip = Equipment.None;

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        GetTarget = transform.GetChild(0);
        animator_Equip = GetComponentsInChildren<Animator>();
        animator = animator_Equip[0];
        animator_Equip[0] = null;
        equip = new GameObject[6];
        for (int i = 0; i < animator_Equip.Length - 1;  i++)
        {
            equip[i] = transform.GetChild(i+1).gameObject;
            animator_Equip[i] = animator_Equip[i + 1];
        }
        for (int i = 0; i < equip.Length; i++)
        {
            if((int)nowEquip != i)
            equip[i].SetActive(false);

        }
    }
    private void Update()
    {
        if (fire)
        {

        }


    }
    private void FixedUpdate()
    {
        transform.Translate(direction * speed * run);
    }
    private void OnFire(InputValue inputValue)
    {
        fire = inputValue.isPressed;
        animator_Equip[(int)nowEquip].SetBool("Fire", fire);

        GiveResourceData();   

        if (!fire)
        {
            ResourceManager.Instance.DataClear();        
        }

    }

    public bool GiveResourceData()
    {
        if (target == null) { return false; }
        else if (target.layer.Equals(LayerMask.NameToLayer("Resources"))) // 자원을 캘때
        {
            Bricks bricks = target.GetComponent<Bricks>();
            ResourceManager.Instance.ReceiveResourceData(bricks, nowEquip, GetTarget.transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GiveTargetPositionData()
    {
        return GetTarget.transform.position;
    }

    private void OnChangeWeapon(InputValue inputValue)
    {
        float change = inputValue.Get<Vector2>().y;
        if (change > 0 && (int)nowEquip != 5)
        {
            equip[(int)nowEquip].SetActive(false);
            nowEquip += 1;
            equip[(int)nowEquip].SetActive(true);
            animator_Equip[(int)nowEquip].SetInteger("Direction", (int)viewdirection);
            animator_Equip[(int)nowEquip].SetTrigger("Move");
        }
        else if (change < 0 && nowEquip != 0)
        {
            equip[(int)nowEquip].SetActive(false);
            nowEquip -= 1;
            equip[(int)nowEquip].SetActive(true);
            animator_Equip[(int)nowEquip].SetInteger("Direction", (int)viewdirection);
            animator_Equip[(int)nowEquip].SetTrigger("Move");
        }
    }
    private void OnMove(InputValue inputValue)
    {
        direction  = inputValue.Get<Vector2>();
        if (direction.x == 0 && direction.y != 0)
        {
            animator.SetBool("Idle", false);
            if (direction.y < 0)
            {
                viewdirection = ViewDirection.Down;
                GetTarget.localPosition = new Vector2(0, -1.5f);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }

            }
            else if (direction.y > 0)
            {
                viewdirection = ViewDirection.Up;
                GetTarget.localPosition = new Vector2(0, 1.5f);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }

            }
        }
        else if (direction.x != 0 && direction.y == 0)
        {
            animator.SetBool("Idle", false);
            if (direction.x < 0)
            {
                viewdirection = ViewDirection.Left;
                GetTarget.localPosition = new Vector2(-1, 0);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }
            }
            else if (direction.x > 0)
            {
                viewdirection = ViewDirection.Right;
                GetTarget.localPosition = new Vector2(1, 0);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }
            }
        }
        else if (direction.x == 0 && direction.y == 0)
        {
            animator.SetBool("Idle",true);
        }
    }

    private void OnRun(InputValue inputValue)
    {
        running = inputValue.isPressed;
        run = running ? 2 : 1;
    }
}
