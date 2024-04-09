using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingStatement
{
    FreeBuilding = 0,
    isBuilding =1,
    Built = 2
}
public class Building : MonoBehaviour
{
    public int index;
    static int worldIndexNum;
    public BuildingStatement buildingStatement = BuildingStatement.FreeBuilding;
    bool isContact = false;
    public float MaxConstructTime;
    public float nowConstructTime;

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
        buildingStatement = BuildingStatement.FreeBuilding;
        isContact = false;
        boxCollider2d.isTrigger = true;
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        nowConstructTime = 0;
    }
    public void Build(float work)
    {
        if (buildingStatement == BuildingStatement.Built) return;
        nowConstructTime += work;
        spriteRenderer.color = new Color(1, 1, nowConstructTime / MaxConstructTime);
        if (nowConstructTime > MaxConstructTime)
        {
            buildingStatement = BuildingStatement.Built;
            spriteRenderer.color = Color.white;
            StartCoroutine(falshCoroutine());
            PalManager.Instance.buildings.Remove(this);
        }
    }

    public bool ContactCheck()
    {
        if (isContact) return isContact;
        buildingStatement = BuildingStatement.isBuilding;
        boxCollider2d.isTrigger = false;
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        spriteRenderer.color = Color.yellow;
        index = worldIndexNum++;
        PalManager.Instance.buildings.Add(this);
        return false;
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
}