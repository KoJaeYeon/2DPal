using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingStatement
{
    FreeBuilding = 0,
    isBuilding =1,
    Built = 2,
    Working = 3,
    Done = 4
}
public class Building : MonoBehaviour
{
    public int id;
    public int index;
    static int worldIndexNum = 1;
    public BuildingStatement buildingStatement = BuildingStatement.FreeBuilding;
    bool isContact = false; // 설치 시 접촉되어 있는지 판단하는 변수
    public float MaxConstructTime;
    public float nowConstructTime;
    public float MaxWorkTime;
    public float nowWorkTime;
    protected string buildingName;

    public enum BuildingType { Recipe, Pal, Chest, None }
    public BuildingType buildingType;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2d;
    Rigidbody2D rigidbody2d;
    public PalAI workingPal;

    public List<Building> todoList;

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        todoList = PalManager.Instance.buildings;
    }


    protected void OnEnable()
    {
    }

    protected void OnDisable()
    {
        isContact = false;
        boxCollider2d.isTrigger = true;
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        nowConstructTime = 0;
        workingPal = null;
    }

    public virtual void Action()
    {
    }

    public void Build(float work)
    {
        if (buildingStatement == BuildingStatement.Built) return;
        nowConstructTime += work;
        if ((nowConstructTime / MaxConstructTime) < 0.5f) spriteRenderer.color = new Color((nowConstructTime / MaxConstructTime) * 2, (nowConstructTime / MaxConstructTime) * 2, 0);
        else spriteRenderer.color = new Color(1, 1, ((nowConstructTime / MaxConstructTime) - 0.5f) * 2);
        if (nowConstructTime > MaxConstructTime)
        {
            buildingStatement = BuildingStatement.Built;
            spriteRenderer.color = Color.white;
            StartCoroutine(falshCoroutine());
            PalManager.Instance.buildings.Remove(this);
            PlayerControll playerControll = GameManager.Instance.playerController.GetComponent<PlayerControll>();
            GameManager.Instance.GetExp((int)(MaxConstructTime / 5 * 2));
            if (playerControll.statement == Statement.Building)
            {
                playerControll.EndConstruct(this);
            }
        }
    }

    public virtual void ConfirmProduct(Product product)
    {

    }

    public virtual void ResetPanel()
    {

    }
    public virtual void Work(float work)
    {
        if (buildingStatement == BuildingStatement.Built) return;
        nowWorkTime += work;
        if (nowWorkTime > MaxWorkTime)
        {
            workingPal = null;
            buildingStatement = BuildingStatement.Done;
            todoList.Remove(this);
            PlayerControll playerControll = GameManager.Instance.playerController.GetComponent<PlayerControll>();
            if (playerControll.statement == Statement.Action)
            {
                playerControll.EndConstruct(this);
            }
        }
    }

    public void Cancel() // C를 계속눌러 취소됐을 때
    {
        switch (buildingStatement)
        {
            case BuildingStatement.isBuilding:
                CraftManager.Instance.ReturnBuilding(id);
                PalManager.Instance.buildings.Remove(this);
                transform.parent = FurnitureDatabase.Instance.poolParent.transform;
                buildingStatement = BuildingStatement.FreeBuilding;
                gameObject.SetActive(false);
                break;
            case BuildingStatement.Working:
                break;
        }
    }
    public bool ContactCheck()
    {
        if (isContact) return isContact;
        buildingStatement = BuildingStatement.isBuilding;
        boxCollider2d.isTrigger = false;
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        spriteRenderer.color = Color.black;
        index = worldIndexNum++;
        PalManager.Instance.buildings.Add(this);
        return false;
    }

    public void DestroyBuilding()
    {
        if (buildingStatement == BuildingStatement.isBuilding) { PalManager.Instance.buildings.Remove(this); }
        CraftManager.Instance.ReturnBuilding(id);
        transform.parent = FurnitureDatabase.Instance.poolParent.transform;
        buildingStatement = BuildingStatement.FreeBuilding;
        gameObject.SetActive(false);
    }

    public float GetLeftBuild()
    {
        return MaxConstructTime - nowConstructTime;
    }
    public float GetLeftWork()
    {
        return MaxWorkTime - nowWorkTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (buildingStatement != BuildingStatement.FreeBuilding) return;
        if (!isContact)
        {
            spriteRenderer.color = Color.red;
            isContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (buildingStatement != BuildingStatement.FreeBuilding) return;
        isContact = false;
        spriteRenderer.color = Color.green;
    }

    IEnumerator falshCoroutine()
    {
        Material material = spriteRenderer.material;
        spriteRenderer.material = FurnitureDatabase.Instance.flashMaterial;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.material = material;
        yield break;
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        Building building = other as Building;
        return index == building.index;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public int getBuildStaticNum()
    {
        return worldIndexNum;
    }

    public void setBuildStaticNum(int num)
    {
        worldIndexNum = num;
    }
}
