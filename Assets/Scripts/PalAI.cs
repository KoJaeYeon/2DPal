using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PalStates
{
    //Decimal
    Idle = 0,
    Move = 1
}

public class PalAI : MonoBehaviour
{
    public PalStates palState;
    
    public GameObject target;

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
        palState = PalStates.Move;
        pathInitIndex = 0; // 초기화
        pathFinalIndex = path.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            OnGo();
        }

        switch(palState)
        {
            case PalStates.Idle:
                break;
            case PalStates.Move:
                PalMove();
                break;
        }

        
    }

    public void PalMove() // Astar pathFinding
    {
        targetx = path[pathInitIndex].x;
        targety = path[pathInitIndex].y;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetx, targety), 0.003f);
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
}
