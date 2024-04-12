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

    public bool thisPalCanbuild = true;
    public bool thisPalCanProduce = false;

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
        if (palState == PalStates.Idle)
        {
            if(thisPalCanProduce && PalManager.Instance.producing.Count > 0)
            {
                int count = PalManager.Instance.producing.Count;
                for (int i = 0; i < count; i++)
                {
                    targetBulding = PalManager.Instance.producing[i];
                    if(targetBulding.workingPal == null)
                    {
                        targetBulding.workingPal = this;
                        target = targetBulding.gameObject;
                        OnGo();
                        break;
                    }
                }
            }
           if(thisPalCanbuild == true && PalManager.Instance.buildings.Count > 0 && palState == PalStates.Idle)
            {
                targetBulding = PalManager.Instance.buildings[0];
                target = targetBulding.gameObject;
                OnGo();
            }

        }
        else if (palState == PalStates.Move) 
        {
            if(targetBulding.buildingStatement == BuildingStatement.Built || targetBulding.buildingStatement == BuildingStatement.Done) //가기 전에 건물 완공되면 업무취소
            {
                palState = PalStates.Idle;
                target = null;
            }
            
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
        switch(targetBulding.buildingStatement)
        {
            case BuildingStatement.Built:
                palState = PalStates.Idle;
                break;
            case BuildingStatement.isBuilding:
                targetBulding.Build(1);
                if (!targetBulding.gameObject.activeSelf)
                {
                    palState = PalStates.Idle;
                }
                break;
            case BuildingStatement.Working:
                targetBulding.Work(0.1f);
                break;
            case BuildingStatement.Done:
                palState = PalStates.Idle;
                break;
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
            else
            {
                OnGo();
            }
        }
        else
        {
            return;
        }
    }
}
