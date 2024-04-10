using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

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

public enum Statement
{
    Idle = 0,
    Crafting = 1,
    Action = 2,
    Building = 3
}

public class PlayerControll : MonoBehaviour
{
    Vector2 direction;
    public ViewDirection viewdirection;
    public Statement statement;
    public GameObject target; // 콜라이더 오브젝트
    public Transform GetTarget;
    public Slider slider;
    public Image image;

    public bool running = false;
    public bool fire = false;
    public bool moving = false;
    public bool istired = false;
    public bool isaction = false;

    public float speed = 0.01f;
    public float run = 1;

    public float maxStamina = 100;
    public float nowStamina = 100;

    Animator animator;

    public GameObject[] equip;
    public Animator[] animator_Equip;
    public Equipment nowEquip = Equipment.None;

    public GameObject freeBuilding;
    Rigidbody2D rigid;

    public Building building;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        image = slider.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();

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
        Stamina();
        if(statement== Statement.Building)
        {
            building.Build(1);
        }
    }


    public void Stamina()
    {
        if (running && moving)
        {
            slider.gameObject.SetActive(true);
            nowStamina -= 0.015f;
        }
        else if (fire)
        {
            slider.gameObject.SetActive(true);
            nowStamina -= 0.03f;
        }
        else
        {
            if (nowStamina < maxStamina)
                nowStamina += 0.03f;
            else
            {
                istired = false;
                image.color = new Color(1, 1, 0, 0.8f);
                slider.gameObject.SetActive(false);
            }
        }
        if (nowStamina < 0)
        {
            istired = true;
            running = false;
            run = running ? 2 : 1;
            fire = false;
            animator_Equip[(int)nowEquip].SetBool("Fire", fire);
            if (!fire)
            {
                ResourceManager.Instance.DataClear();
            }
            image.color = new Color(1, 0, 0, 0.8f);
        }
        slider.value = nowStamina / maxStamina;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * run);
    }

    public void OnAction(InputValue inputValue)
    {
        if (target == null) return;
        if(target.CompareTag("Furniture"))
        {
            building = target.GetComponent<Building>();
            isaction = inputValue.isPressed;

            if(isaction && building.buildingStatement == BuildingStatement.isBuilding)
            {
                statement = Statement.Building;
            }
            else if (isaction && building.buildingStatement == BuildingStatement.Built)
            {
                statement = Statement.Action;
            }
            else
            {
                building = null;
                statement = Statement.Idle;
            }
        }
    }
    private void OnFire(InputValue inputValue)
    {
        if (GameManager.Instance.ManagerUsingUi()) return;
        if (istired) return;
        if (statement == Statement.Action) return;
        if(statement == Statement.Crafting)
        {
            if(freeBuilding.GetComponent<Building>().ContactCheck()) return;
            else
            {
                EndBuilding();
                CraftManager.Instance.PayBuilding();
                return;
            }
        }
        fire = inputValue.isPressed;
        animator_Equip[(int)nowEquip].SetBool("Fire", fire);
        GiveResourceData();
        if (!fire)
        {
            ResourceManager.Instance.DataClear();        
        }

    }

    private void OnCancel()
    {
        if (target == null) return;
        if (target.CompareTag("Furniture"))
        {
            building = target.GetComponent<Building>();
        }

        if (statement == Statement.Crafting)
        {
            freeBuilding.SetActive(false);
            CraftManager.Instance.ReturnBuilding();
            EndBuilding();
        }
    }

    public void EndBuilding()
    {
        equip[(int)nowEquip].SetActive(true);
        GetTarget.gameObject.SetActive(true);
        statement = Statement.Idle;
        freeBuilding.transform.parent = FurnitureDatabase.Instance.parent.transform;
        freeBuilding = null;
        CraftManager.Instance.BuildingPanel.SetActive(false);
    }

    public void FreeBuilding()
    {
        equip[(int)nowEquip].SetActive(false);
        GetTarget.gameObject.SetActive(false);
        freeBuilding.transform.parent = transform;
        switch(viewdirection)
        {
            case ViewDirection.Up:
                freeBuilding.transform.localPosition = new Vector2(0, 2.5f);
                break;
            case ViewDirection.Down:
                freeBuilding.transform.localPosition = new Vector2(0, -2.5f);
                break;
            case ViewDirection.Right:
                freeBuilding.transform.localPosition = new Vector2(2, 0);
                break;
            case ViewDirection.Left:
                freeBuilding.transform.localPosition = new Vector2(-2, 0);
                break;
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
        if (GameManager.Instance.ManagerUsingUi()) return;
        if (istired) return;
        if (statement == Statement.Crafting)
        {
            return;
        }

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
        if (statement == Statement.Action) return;
        direction  = inputValue.Get<Vector2>();
        moving = true;
        if (direction.x == 0 && direction.y != 0)
        {
            animator.SetBool("Idle", false);
            if (direction.y < 0)
            {
                viewdirection = ViewDirection.Down;
                if(statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(0, -2.5f);
                }
                else GetTarget.localPosition = new Vector2(0, -1.5f);
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
                if (statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(0, 2.5f);
                }
                else GetTarget.localPosition = new Vector2(0, 1.5f);
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
                if (statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(-2, 0);
                }
                else GetTarget.localPosition = new Vector2(-1, 0);
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
                if (statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(2,0);
                }
                else GetTarget.localPosition = new Vector2(1, 0);
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
            moving = false;
            animator.SetBool("Idle",true);
        }
    }

    private void OnRun(InputValue inputValue)
    {
        if (istired) return;
        running = inputValue.isPressed;
        run = running ? 2 : 1;
    }
}
