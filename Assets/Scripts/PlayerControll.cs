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
    private ViewDirection _viewdirection;
    private Statement _statement;
    public GameObject target; // 콜라이더 오브젝트
    [SerializeField]
    private Transform GetTargetTrans;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image image;
#pragma warning disable CS0108 // 멤버가 상속된 멤버를 숨깁니다. new 키워드가 없습니다.
    [SerializeField]
    private CinemachineVirtualCamera camera;
#pragma warning restore CS0108 // 멤버가 상속된 멤버를 숨깁니다. new 키워드가 없습니다.

    private bool running = false;
    private bool fire = false;
    private bool moving = false;
    private bool istired = false;
    private bool isaction = false;
    private bool isthorwing = false;

    //Player Data
    private int _lv = 1;
    private int _exp = 0;
    private int _maxExp = 10;
    private float _hungry = 100;
    private float _health = 500;
    private float _maxHealth = 500;
    private float _moveWeight = 300;
    private int _skillPoint = 0;
    private int _TechPoint = 3;
    private float _attack = 100;

    private float speed = 0.1f;
    private float run = 1;
    private float throwPower = 0;

    private float _maxStamina = 100;
    private float _nowStamina = 100;

    #region Player Data Property
    public ViewDirection viewDirection { get => _viewdirection; set => _viewdirection = value; }
    public Statement statement { get => _statement; set => _statement = value; }
    public int lv { get => _lv; set => _lv = value; }
    public int exp { get => _exp; set => _exp = value; }
    public int maxExp { get => _maxExp; set => _maxExp = value; }
    public float hungry { get => _hungry; set => _hungry = value; }
    public float health { get => _health; set => _health = value; }
    public float maxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float moveWeight { get => _moveWeight; set => _moveWeight = value; }
    public int skillPoint { get => _skillPoint; set => _skillPoint = value; }
    public int TechPoint { get => _TechPoint; set => _TechPoint = value; }
    public float attack { get => _attack; set => _attack = value; }
    public float maxStamina { get => _maxStamina; set => _maxStamina = value;}
    public float nowStamina { get => _nowStamina; set => _nowStamina = value; }

    #endregion
    Animator animator;

    public GameObject[] equip;
    public Animator[] animator_Equip;
    public int nowEquip = 0;

    public GameObject freeBuilding;
    Rigidbody2D rigid;
    GetTarget getTarget;
    public Building building;
    public GameObject palSphere;
    public PalSphere palsphere_PalSphere;
    CircleCollider2D circleCollider2D;   

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        image = slider.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        getTarget = GetTargetTrans.gameObject.GetComponent<GetTarget>();

        GetTargetTrans = transform.GetChild(0);
        equip = new GameObject[4];
        animator_Equip = new Animator[4];
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Stamina();
        switch (_statement)
        {
            case Statement.Building:
                building.Build(10f * Time.deltaTime);
                break;
            case Statement.Action:
                building.Work(2f * Time.deltaTime);
                break;
        }
        if(isthorwing)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 normal = (point - transform.position).normalized;
            palSphere.transform.position = transform.position + normal * 2;
            if (throwPower < 600f) throwPower += 70f * Time.deltaTime;
            palsphere_PalSphere.SetRotateSpeed(throwPower);
            if (camera.m_Lens.OrthographicSize < 12) camera.m_Lens.OrthographicSize += 2f * Time.deltaTime;
        }
        else
        {
            if(camera.m_Lens.OrthographicSize > 8) camera.m_Lens.OrthographicSize -= 2f * Time.deltaTime;
        }

    }


    public void Stamina()
    {
        if (running && moving)
        {
            slider.gameObject.SetActive(true);
            _nowStamina -= 10f * Time.deltaTime;
        }
        else if (fire)
        {
            if (GameManager.Instance.ManagerUsingUi())
            {
                if (_nowStamina < _maxStamina)
                    _nowStamina += 15f * Time.deltaTime;
                else
                {
                    istired = false;
                    image.color = new Color(1, 1, 0, 0.8f);
                    slider.gameObject.SetActive(false);
                }
                slider.value = _nowStamina / _maxStamina;
                return;
            }
            else if(!istired)
            {
                slider.gameObject.SetActive(true);
                _nowStamina -= 20f * Time.deltaTime;
            }
            else
            {
                _nowStamina += 15f * Time.deltaTime;
                slider.value = _nowStamina / _maxStamina;
                if (_nowStamina > _maxStamina)
                {
                    istired = false;
                    image.color = new Color(1, 1, 0, 0.8f);
                    slider.gameObject.SetActive(false);
                }
                return;
            }
        }
        else
        {
            if (_nowStamina < _maxStamina)
                _nowStamina += 15f * Time.deltaTime;
            else
            {
                istired = false;
                image.color = new Color(1, 1, 0, 0.8f);
                slider.gameObject.SetActive(false);
            }
        }
        if (_nowStamina < 0)
        {
            istired = true;
            running = false;
            run = running ? 2 : 1;
            fire = false;
            animator_Equip[nowEquip].SetBool("Fire", fire);
            if (!fire)
            {
                ResourceManager.Instance.DataClear();
            }
            image.color = new Color(1, 0, 0, 0.8f);
        }
        slider.value = _nowStamina / _maxStamina;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * run);
    }

    public void EndBuilding()
    {
        equip[nowEquip].SetActive(true);
        GetTargetTrans.gameObject.SetActive(true);
        _statement = Statement.Idle;
        freeBuilding = null;
        GameManager.Instance.ExitMenu();
    }

    public void FreeBuilding()
    {
        equip[nowEquip].SetActive(false);
        GetTargetTrans.gameObject.SetActive(false);
        freeBuilding.transform.parent = transform;
        switch (_viewdirection)
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
            _statement = Statement.Idle;
        }
    }

    public bool GiveResourceData()
    {
        if (target == null) { return false; }
        else if (target.layer.Equals(LayerMask.NameToLayer("Resources"))) // 자원을 캘때
        {
            Bricks bricks = target.GetComponent<Bricks>();
            Weapon weapon = equip[nowEquip].GetComponent<Weapon>();
            ResourceManager.Instance.ReceiveResourceData(bricks, weapon.equip, GetTargetTrans.transform.position);
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
            circleCollider2D = palSphere.GetComponent<CircleCollider2D>();
            circleCollider2D.enabled = false;
            palsphere_PalSphere = palSphere.GetComponent<PalSphere>();
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 normal = (point - transform.position).normalized;
            palSphere.transform.position = transform.position + normal * 2;            
        }
        else
        {
            if (palsphere_PalSphere == null) return;
            InventoryManager.Instance.UseItem(1000);
            circleCollider2D.enabled = true;
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
        if (_statement == Statement.Crafting)
        {
            freeBuilding.transform.parent = FurnitureDatabase.Instance.poolParent.transform;
            freeBuilding.SetActive(false);
            EndBuilding();
            
        }
        else
        {
            _statement = Statement.Idle;
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
                        _statement = Statement.Building;
                        break;
                    case BuildingStatement.Built:
                        building.Action();
                        break;
                    case BuildingStatement.Working:
                        _statement = Statement.Action;
                        break;
                    case BuildingStatement.Done:
                        building.Action();
                        break;
                }
            }
            else
            {
                building = null;
                _statement = Statement.Idle;
            }
        }
        else if (target.CompareTag("DropItem"))
        {
            DropItem dropItem = target.GetComponent<DropItem>();
            List<Item> items = dropItem.items;

            if (inputValue.isPressed)
            {
                int length = items.Count;
                for (int i = 0; i < length; i++)
                {
                    Item item = ItemDatabase.Instance.GetItem(items[i].id);
                    item.count = items[i].count;
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
            if (GameManager.Instance.activePanel == CraftManager.Instance.buildingPanel) { }
            else if (GameManager.Instance.ManagerUsingUi()) return;
            if (_statement == Statement.Action) return;
            else if (_statement == Statement.Crafting)
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
            else if(_statement == Statement.Disassembling)
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
        if (_statement == Statement.Crafting)
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
        if (_statement == Statement.Idle)
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
        if (_statement == Statement.Crafting)
        {
            float rotationZ = inputValue.Get<Vector2>().y;
            freeBuilding.transform.Rotate(Vector3.forward * rotationZ * 30f);
            return;
        }
        if (istired) return;

        float change = inputValue.Get<Vector2>().y;
        if (change > 0)
        {
            equip[nowEquip].SetActive(false);
            if(nowEquip == 3) nowEquip = 0;
            else nowEquip += 1;
            equip[nowEquip].SetActive(true);
            animator_Equip[nowEquip].SetInteger("Direction", (int)_viewdirection);
            animator_Equip[nowEquip].SetTrigger("Move");
        }
        else if (change < 0 && nowEquip != 0)
        {
            equip[nowEquip].SetActive(false);
            if (nowEquip == 0) nowEquip = 3;
            else nowEquip -= 1;
            equip[nowEquip].SetActive(true);
            animator_Equip[nowEquip].SetInteger("Direction", (int)_viewdirection);
            animator_Equip[nowEquip].SetTrigger("Move");
        }
    }
    private void OnMove(InputValue inputValue)
    {
        if (_statement == Statement.Action) return;
        direction = inputValue.Get<Vector2>();
        moving = true;
        if (direction.x == 0 && direction.y != 0)
        {
            animator.SetBool("Idle", false);
            if (direction.y < 0)
            {
                _viewdirection = ViewDirection.Down;
                if (_statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(0, -2.5f);
                }
                else GetTargetTrans.localPosition = new Vector2(0, -1.5f);
                if (animator.GetInteger("Direction") != (int)_viewdirection)
                {
                    animator.SetInteger("Direction", (int)_viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)_viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }

            }
            else if (direction.y > 0)
            {
                _viewdirection = ViewDirection.Up;
                if (_statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(0, 2.5f);
                }
                else GetTargetTrans.localPosition = new Vector2(0, 1.5f);
                if (animator.GetInteger("Direction") != (int)_viewdirection)
                {
                    animator.SetInteger("Direction", (int)_viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)_viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }

            }
        }
        else if (direction.x != 0 && direction.y == 0)
        {
            animator.SetBool("Idle", false);
            if (direction.x < 0)
            {
                _viewdirection = ViewDirection.Left;
                if (_statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(-2, 0);
                }
                else GetTargetTrans.localPosition = new Vector2(-1, 0);
                if (animator.GetInteger("Direction") != (int)_viewdirection)
                {
                    animator.SetInteger("Direction", (int)_viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)_viewdirection);
                    animator_Equip[(int)nowEquip].SetTrigger("Move");
                }
            }
            else if (direction.x > 0)
            {
                _viewdirection = ViewDirection.Right;
                if (_statement == Statement.Crafting)
                {
                    freeBuilding.transform.localPosition = new Vector2(2, 0);
                }
                else GetTargetTrans.localPosition = new Vector2(1, 0);
                if (animator.GetInteger("Direction") != (int)_viewdirection)
                {
                    animator.SetInteger("Direction", (int)_viewdirection);
                    animator.SetTrigger("Move");
                    animator_Equip[(int)nowEquip].SetInteger("Direction", (int)_viewdirection);
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
