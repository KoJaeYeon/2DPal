using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PalStates
{
    //Decimal
    Idle = 0,
    Move = 1,
    Action = 2
}

public class PalAI : MonoBehaviour
{
    public PalStates palState;    
    public GameObject target;
    public Building targetBulding;

    #region Astar Pathfinding
    Astar astar;
    private List<Node> path;
    public int pathInitIndex, pathFinalIndex;
    public float targetx;
    public float targety;
    #endregion

    private void Awake()
    {
        astar = GetComponent<Astar>();
    }

    void OnGo()
    {
        astar.SetDestination(target); // 타켓 목적지 설정
        path = astar.AStar(); // 길찾기 연산        
        if (path.Count == 0)
        {
            return;        
        }
        palState = PalStates.Move;
        pathInitIndex = 0; // 초기화
        pathFinalIndex = path.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Search());
    }
    IEnumerator Search()
    {
        Debug.Log("Search");
        if (palState == PalStates.Idle && PalManager.Instance.buildings.Count > 0)
        {
            targetBulding = PalManager.Instance.buildings[0];
            target = targetBulding.gameObject;
            OnGo();
        }
        else if (palState == PalStates.Move && targetBulding.buildingStatement == BuildingStatement.Built)
        {
            palState = PalStates.Idle;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Search());
    }

    private void FixedUpdate()
    {
        switch (palState)
        {
            case PalStates.Idle:
                break;
            case PalStates.Move:
                PalMove();
                break;
            case PalStates.Action:
                PalWork();
                break;
        }
    }


    void PalWork()
    {
        if(targetBulding.buildingStatement == BuildingStatement.Built)
        {
            palState = PalStates.Idle;
        }
        else if(targetBulding.buildingStatement == BuildingStatement.isBuilding)
        {
            targetBulding.Build(1);
        }
        
    }

    public void PalMove() // Astar pathFinding
    {
        targetx = path[pathInitIndex].x;
        targety = path[pathInitIndex].y;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetx, targety), 0.03f);
        if (Mathf.Abs(transform.position.x - targetx) < 0.1f && Mathf.Abs(transform.position.y - targety) < 0.1f)
        {
            astar.SetGizmoIndex(pathInitIndex);
            pathInitIndex++;
            if (pathInitIndex == pathFinalIndex)
            {
                palState = PalStates.Idle;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Furniture"))
        {
            int index = collision.gameObject.GetComponent<Building>().index;
            if(targetBulding.index == index)
            {
                palState = PalStates.Action;
            }
        }
        else
        {
            return;
        }
    }
}
