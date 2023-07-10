using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private LayerMask unwalkableArea;
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadius;
    public List<Node> path;

    private Node[,] _grid;
    private float _nodeDiameter;
    private int _gridXCount;
    private int _gridYCount;
    

    private void Start()
    {
        _nodeDiameter = nodeRadius * 2;
        _gridXCount =Mathf.RoundToInt( gridWorldSize.x / _nodeDiameter);
        _gridYCount =Mathf.RoundToInt( gridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    public int MaxSize => _gridXCount * _gridYCount;

    public void CreateGrid()
    {
        _grid = new Node[_gridXCount,_gridYCount];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2-Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridWorldSize.x; x++)
        {
            for (int y = 0; y < gridWorldSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + nodeRadius) +
                                                        Vector3.forward * (y * _nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableArea));
                _grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX>= 0 && checkX<_gridXCount && checkY>=0 && checkY <_gridYCount)
                {
                    neighbours.Add(_grid[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }
 
    public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY= Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((_gridXCount - 1) * percentX);
        int y = Mathf.RoundToInt((_gridYCount- 1) *percentY);
        return _grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
        if (_grid!=null)
        {
            Node playerNode = GetNodeFromWorldPosition(player.position);
            
            foreach (var node in _grid)
            {
                Gizmos.color = node.walkable ? Color.white : Color.red;

                if (playerNode==node)
                {
                    Gizmos.color=Color.cyan;
                    
                }

                if (path.Contains(node))
                {
                    Gizmos.color=Color.black;
                }
                Gizmos.DrawCube(node.worldPosition,Vector3.one*(_nodeDiameter-.1f));
            }
        }
    }
}