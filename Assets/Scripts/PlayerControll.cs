using Cinemachine;
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
    Building = 3,
    Disassembling = 4
}

public class PlayerControll : MonoBehaviour
{
    Vector2 direction;
    public ViewDirection viewdirection;
    public Statement statement;
    public GameObject target; // 콜라이더 오브젝트
    public Transform GetTargetTrans;
    public Slider slider;
    public Image image;
#pragma warning disable CS0108 // 멤버가 상속된 멤버를 숨깁니다. new 키워드가 없습니다.
    public CinemachineVirtualCamera camera;
#pragma warning restore CS0108 // 멤버가 상속된 멤버를 숨깁니다. new 키워드가 없습니다.

    public bool running = false;
    public bool fire = false;
    public bool moving = false;
    public bool istired = false;
    public bool isaction = false;
    public bool isthorwing = false;

    //Player Data
    public int lv;
    public int exp;
    public int hungry;
    public int health;
    public int moveWeight;
    public int skillPoint;
    public int TechPoint = 10;

    public float speed = 0.01f;
    public float run = 1;
    public float throwPower = 0;

    public float maxStamina = 100;
    public float nowStamina = 100;


    Animator animator;

    public GameObject[] equip;
    public Animator[] animator_Equip;
    public Equipment nowEquip = Equipment.None;

    public GameObject freeBuilding;
    Rigidbody2D rigid;
    GetTarget getTarget;
    public Building building;
    public GameObject palSphere;
    public PalSphere palsphere_PalSphere;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        image = slider.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        getTarget = GetTargetTrans.gameObject.GetComponent<GetTarget>();

        GetTargetTrans = transform.GetChild(0);
        animator_Equip = GetComponentsInChildren<Animator>();
        animator = animator_Equip[0];
        equip = new GameObject[4];
        {
            Animator[] tempAni = new Animator[4];
            for (int i = 0; i < animator_Equip.Length - 1; i++)
            {
                equip[i] = transform.GetChild(i + 1).gameObject;
                tempAni[i] = animator_Equip[i + 1];
            }
            animator_Equip = tempAni;
        }

        for (int i = 0; i < equip.Length; i++)
        {
            if ((int)nowEquip != i)
                equip[i].SetActive(false);

        }
    }
    private void Update()
    {
        Stamina();
        switch (statement)
        {
            case Statement.Building:
                building.Build(0.1f);
                break;
            case Statement.Action:
                building.Work(0.02f);
                break;
        }
        if(isthorwing)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 normal = (point - transform.position).normalized;
            palSphere.transform.position = transform.position + normal * 2;
            if (throwPower < 600f) throwPower += 0.2f;
            palsphere_PalSphere.SetRotateSpeed(throwPower);
            if (camera.m_Lens.OrthographicSize < 12) camera.m_Lens.OrthographicSize += 0.001f;
        }
        else
        {
            if(camera.m_Lens.OrthographicSize > 8) camera.m_Lens.OrthographicSize -= 0.001f;
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

    public void EndBuilding()
    {
        equip[(int)nowEquip].SetActive(true);
        GetTargetTrans.gameObject.SetActive(true);
        statement = Statement.Idle;
        freeBuilding = null;
        GameManager.Instance.ExitMenu();
    }

    public void FreeBuilding()
    {
        equip[(int)nowEquip].SetActive(false);
        GetTargetTrans.gameObject.SetActive(false);
        freeBuilding.transform.parent = transform;
        switch (viewdirection)
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

    public void EndConstruct(Building building)
    {
        if (this.building.Equals(building))
        {
            statement = Statement.Idle;
        }
    }

    public bool GiveResourceData()
    {
        if (target == null) { return false; }
        else if (target.layer.Equals(LayerMask.NameToLayer("Resources"))) // 자원을 캘때
        {
            Bricks bricks = target.GetComponent<Bricks>();
            ResourceManager.Instance.ReceiveResourceData(bricks, nowEquip, GetTargetTrans.transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GiveTargetPositionData()
    {
        return GetTargetTrans.transform.position;
    }

    private void OnThrow(InputValue inputValue) // Q
    {        
        isthorwing = inputValue.isPressed;
        if (isthorwing)
        {
            if (InventoryManager.sphereCount == 0) { isthorwing = false; return; }
            palSphere = BattleManager.Instance.GiveSphere();
            palsphere_PalSphere = palSphere.GetComponent<PalSphere>();
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 normal = (point - transform.position).normalized;
            palSphere.transform.position = transform.position + normal * 2;            
        }
        else
        {
            if (palsphere_PalSphere == null) return;
            InventoryManager.Instance.UseItem(1001);
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 normal = (point - transform.position).normalized;
            Rigidbody2D rb = palSphere.GetComponent<Rigidbody2D>();
            rb.AddForce(normal.normalized * throwPower * 2f);
            throwPower = 0;
            palsphere_PalSphere.Go();
            palSphere = null;
            palsphere_PalSphere = null;
        }
    }

    private void OnEscape(InputValue inputValue) // Esc
    {
        if (statement == Statement.Crafting)
        {
            freeBuilding.transform.parent = FurnitureDatabase.Instance.poolParent.transform;
            freeBuilding.SetActive(false);
            EndBuilding();
            
        }
        else
        {
            statement = Statement.Idle;
            GameManager.Instance.ExitMenu(true);
        }
    }
    private void OnAction(InputValue inputValue) //F
    {
        if (GameManager.Instance.activePanel == getTarget.ActionPanel) { }
        else if (GameManager.Instance.ManagerUsingUi()) return;
        if (target == null) return;
        if (target.CompareTag("Furniture"))
        {
            building = target.GetComponent<Building>();
            isaction = inputValue.isPressed;

            if (isaction)
            {
                switch (building.buildingStatement)
                {
                    case BuildingStatement.isBuilding:
                        statement = Statement.Building;
                        break;
                    case BuildingStatement.Built:
                        building.Action();
                        break;
                    case BuildingStatement.Working:
                        statement = Statement.Action;
                        break;
                    case BuildingStatement.Done:
                        building.Action();
                        break;
                }
            }
            else
            {
                building = null;
                statement = Statement.Idle;
            }
        }
        else if (target.CompareTag("DropItem"))
        {
            DropItem dropItem = target.GetComponent<DropItem>();

            if (inputValue.isPressed)
            {
                foreach(Item item in dropItem.items)
                {
                    InventoryManager.Instance.DropItem(item);
                }                
                dropItem.gameObject.SetActive(false);
            }
        }
    }
    private void OnFire(InputValue inputValue) // Fire1
    {
        fire = inputValue.isPressed;
        if (fire)
        {
            if (GameManager.Instance.activePanel == CraftManager.Instance.BuildingPanel) { }
            else if (GameManager.Instance.ManagerUsingUi()) return;
            if (statement == Statement.Action) return;
            else if (statement == Statement.Crafting)
            {
                if (freeBuilding.GetComponent<Building>().ContactCheck()) return;
                else
                {
                    if (!CraftManager.Instance.canBuild) return; // 재료 부족으로 건축 불가능 시 리턴, 없으면 프로그램 터짐
                    freeBuilding.transform.parent = FurnitureDatabase.Instance.parent.transform;
                    EndBuilding();
                    CraftManager.Instance.PayBuilding();
                    return;
                }
            }
            else if(statement == Statement.Disassembling)
            {
                if (target == null) return;
                if (target.CompareTag("Furniture"))
                {
                    building = target.GetComponent<Building>();
                    if (building.buildingStatement != BuildingStatement.Working) building.DestroyBuilding(); // 작업중이면 파괴 x
                }
            }
            if (istired) return;
        }
        animator_Equip[(int)nowEquip].SetBool("Fire", fire);
        GiveResourceData();
        if (!fire)
        {
            ResourceManager.Instance.DataClear();
        }

    }

    private void OnFire2()
    {
        if (statement == Statement.Crafting)
        {
            if (freeBuilding.GetComponent<Building>().ContactCheck()) return;
            else
            {
                if (!CraftManager.Instance.canBuild) return; // 재료 부족으로 건축 불가능 시 리턴, 없으면 프로그램 터짐
                int id = freeBuilding.GetComponent<Building>().id;
                freeBuilding.transform.parent = FurnitureDatabase.Instance.parent.transform;
                EndBuilding();
                CraftManager.Instance.PayBuilding();
                CraftManager.Instance.FurnitureChoice(id, true);
                return;
            }
        }
    }

    private void OnCancel(InputValue inputValue) //C
    {
        bool isPressed = inputValue.isPressed;
        if (target == null) return;
        if (target.CompareTag("Furniture"))
        {
            building = target.GetComponent<Building>();
        }
        if (statement == Statement.Idle)
        {
            if (getTarget.ActionPanel.activeSelf)
            {
                getTarget.CancelAction(isPressed);
            }
        }
    }
    private void OnChangeWeapon(InputValue inputValue)
    {
        if (GameManager.Instance.ManagerUsingUi()) return;
        if (statement == Statement.Crafting)
        {
            float rotationZ = inputValue.Get<Vector2>().y;
            freeBuilding.transform.Rotate(Vector3.forward * rotationZ * 30f);
            return;
        }
        if (istired) return;

        float change = inputValue.Get<Vector2>().y;
        if (change > 0 && (int)nowEquip != 3)
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
        direction = inputValue.Get<Vector2>();
        moving = true;
        if (direction.x == 0 && direction.y != 0)
        {
            animator.SetBool("Idle", false);
            if (direction.y < 0)
            {
                viewdirection = ViewDirection.Down;
                if (statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(0, -2.5f);
                }
                else GetTargetTrans.localPosition = new Vector2(0, -1.5f);
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
                else GetTargetTrans.localPosition = new Vector2(0, 1.5f);
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
                else GetTargetTrans.localPosition = new Vector2(-1, 0);
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
                    freeBuilding.transform.localPosition = new Vector2(2, 0);
                }
                else GetTargetTrans.localPosition = new Vector2(1, 0);
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
            animator.SetBool("Idle", true);
        }
    }

    private void OnRun(InputValue inputValue)
    {
        if (istired) return;
        running = inputValue.isPressed;
        run = running ? 2 : 1;
    }
}
