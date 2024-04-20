using UnityEngine;
using System.Collections.Generic;
public class Node
{
    /*
     �� ��尡 ������ 
    �θ��� 
    x, y ��ǥ��
    f  h+g
    h  ������ �� ���� ���� ��ֹ��� �����Ͽ� ��ǥ������ �Ÿ�
    g  �������� ���� �̵��ߴ� �Ÿ� 
     */
    public bool isWall;
    public Node Parentnode;
    public int x, y;
    public int G;
    public int H;
    
    public int F
    {
        get
        {
            return G + H;
        }
    }
    public Node(bool iswall, int x, int y)
    {
        this.isWall = iswall;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return "x : " + x + ",y : " + y;
    }

}
public class Astar : MonoBehaviour
{
    public PalAI palAI;
    public GameObject destination;

    public int range = 5;

    public Vector2Int bottomLeft, topRight, start_Pos, end_Pos;

    public List<Node> final_node;
    public Node[,] nodeArray;

    //�밢���� �̿� �Ұ����� ��� // false�� �� �������θ� �̵�
    public bool AllowDigonal = true;
    //�ڳʸ� �������� ���� ���� ��� �̵��� ���� ���� ��ֹ��� �ִ� �� �Ǵ� //false�� �� �ڳʿ� �����鼭 ������
    public bool DontCrossCorner = true;

    Node startNode, endNode, curNode;

    private int sizeX, sizeY;

    const int CostStraight = 10;
    const int CostDiagonal = 14;

    int index = 0;

    List<Node> OpenList, ClosedList;

    private void Awake()
    {
        palAI = GetComponent<PalAI>();
    }
    public void SetGizmoIndex(int index)
    {
        this.index = index;
    }
    public void SetDestination(GameObject gameObject)
    {
        destination = gameObject;
    }
    public void Clear()
    {
        final_node.Clear();
    }

    public void SetPosInit()
    {
        float minX, maxX,minY,maxY;
        minX = transform.position.x;
        maxX = destination.transform.position.x;
        minY = transform.position.y;
        maxY = destination.transform.position.y;

        if (transform.position.x > destination.transform.position.x)
        {
            float temp = minX;
            minX = maxX;
            maxX = temp;
        }
        if (transform.position.y > destination.transform.position.y)
        {
            float temp = minY;
            minY = maxY;
            maxY = temp;
        }

        //���ۺ��� �������� 5���� ������ �˻�
        minX -= range;
        minY -= range;
        maxX += range;
        maxY += range;

        bottomLeft = new Vector2Int((int)minX,(int)minY);
        topRight = new Vector2Int((int)maxX,(int)maxY);
        start_Pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        end_Pos = new Vector2Int((int)destination.transform.position.x,(int)destination.transform.position.y);
    }

    public List<Node> AStar()
    {
        SetPosInit();
        sizeX = topRight.x-bottomLeft.x + 1;
        sizeY = topRight.y-bottomLeft.y + 1;

        nodeArray = new Node[sizeX,sizeY];
        for (int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                bool isObstacle = false;
                //�� ��忡 0.49f�������� ���� �����Ͽ� �浹���� �� ��� ���
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.49f))
                {
                    if(col.gameObject.layer.Equals(LayerMask.NameToLayer("Resources")))
                    {
                        isObstacle = true;
                    }
                    else if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Furniture")))
                    {
                        int index = col.gameObject.GetComponent<Building>().index;
                        if (palAI.targetBulding.index != index)
                        {
                            isObstacle = true;
                        }
                    }
                }
                nodeArray[i, j] = new Node(isObstacle, i + bottomLeft.x, j + bottomLeft.y);
            }
        }

        //Node �ʱ�ȭ, �������� ������ �־��ֱ�
        startNode = nodeArray[start_Pos.x - bottomLeft.x, start_Pos.y - bottomLeft.y];
        endNode = nodeArray[end_Pos.x - bottomLeft.x, end_Pos.y - bottomLeft.y];

        //List �ʱ�ȭ
        OpenList = new List<Node>();
        ClosedList = new List<Node>();
        final_node = new List<Node>();

        OpenList.Add(startNode);

        while (OpenList.Count > 0)
        {
            curNode = OpenList[0];
            for (int i = 0; i < OpenList.Count; i++)
            {
                //���� ����Ʈ�� ���� F�� �۰� -> F���� ���
                //F�� ���ٸ� H�� ���� ���� ���� ���� ���� -> ���� �������
                if (OpenList[i].F <= curNode.F &&
                    OpenList[i].H < curNode.H)
                {
                    curNode = OpenList[i];
                }
                //���� ����Ʈ���� ���� ����Ʈ�� �ű��
                OpenList.Remove(curNode);
                ClosedList.Add(curNode);

                //��尡 �������� �������� ��
                if (curNode == endNode)
                {
                    Node targetnode = endNode;
                    while (targetnode != startNode)
                    {
                        final_node.Add(targetnode);
                        targetnode = targetnode.Parentnode;
                    }
                    final_node.Add(startNode);
                    final_node.Reverse(); // ������������ ����ȯ
                    return final_node;
                }
                if (AllowDigonal) // �������� �ʾ��� �� ����ؼ� ���, �ֺ���带 ����Ʈ�� �ֱ�
                {
                    //�밢������ �����̴� cost ���
                    // �֢آע�
                    openListAdd(curNode.x + 1, curNode.y - 1);
                    openListAdd(curNode.x - 1, curNode.y + 1);
                    openListAdd(curNode.x + 1, curNode.y + 1);
                    openListAdd(curNode.x - 1, curNode.y - 1);
                }
                //�������� �����̴� cost ���
                // �� �� �� ��
                openListAdd(curNode.x + 1, curNode.y);
                openListAdd(curNode.x - 1, curNode.y);
                openListAdd(curNode.x, curNode.y + 1);
                openListAdd(curNode.x, curNode.y - 1);
            }

        }
        return final_node;

    }

    public void openListAdd(int checkX, int checkY)
    {
        /*
            �����¿� ������ ����� �ʰ�,
            ���� �ƴϸ鼭
            ��������Ʈ�� ����� �Ѵ�.
         */
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1//x�� bottomleft�� top right�ȿ� �ְ� ����� �������� ��
            && checkY >= bottomLeft.y && checkY < topRight.y + 1 //y�� ���������� ���� Ȯ��
            && !nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall //������ Ȯ��
            && !ClosedList.Contains(nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y])//���� ����Ʈ����(�̹� ���� �������)
            )
        {            
            if (AllowDigonal)//�밢�� ��� ��
            {
                if (nodeArray[curNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall &&
                    nodeArray[checkX - bottomLeft.x, curNode.y - bottomLeft.y].isWall)
                {
                    return;
                }
            }
            //�ڳʸ� �������� ���� ���� �� (�̵� �� ���� ���� ��ֹ��� ������ �ȵ�.)
            if (DontCrossCorner)
            {
                if (nodeArray[curNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall ||
                   nodeArray[checkX - bottomLeft.x, curNode.y - bottomLeft.y].isWall)
                {
                    return;
                }
            }
            //check�ϴ� ��带 �̿� ��忡 �ְ� ������ 10 �밢�� 14
            Node neightdorNode =
                nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int movecost = curNode.G +
                (curNode.x - checkX == 0 || curNode.y - checkY == 0 ? CostStraight : CostDiagonal);


            //�̵������ �̿���� G���� �۰ų�, �Ǵ� ���� ����Ʈ�� �̿���尡 ���ٸ�
            if (movecost < neightdorNode.G || !OpenList.Contains(neightdorNode))
            {
                //G H parentnode�� ������ ���� ����Ʈ�� �߰�
                neightdorNode.G = movecost;
                neightdorNode.H = (
                    Mathf.Abs(neightdorNode.x - endNode.x) +
                    Mathf.Abs(neightdorNode.y - endNode.y)) * CostStraight;

                neightdorNode.Parentnode = curNode;

                OpenList.Add(neightdorNode);
            }
        }
    }
    private void OnDrawGizmos()
    {
        //�� ���� Debug�뵵�� �׸��� �׸� �� ����մϴ�. 
        if (final_node != null)
        {
            for (int i = index; i < final_node.Count - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector2(final_node[i].x, final_node[i].y),
                    new Vector2(final_node[i + 1].x, final_node[i + 1].y));
            }
        }
    }
}
