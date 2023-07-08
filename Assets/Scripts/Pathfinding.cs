using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = _grid.GetNodeFromWorldPosition(startPos);
        Node targetNode = _grid.GetNodeFromWorldPosition(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        
        while (openSet.Count>0)
        { Node currentNode = openSet[0];    
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].fCost<currentNode.fCost|| openSet[i].fCost==currentNode.fCost&& openSet[i].hCost<currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode==targetNode)
            {
                return;
                
            }
            foreach (var neighbour in _grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable|| closedSet.Contains(neighbour))
                {
                    
                }
            }
        }
        
    }

    private int GetNodeDistance(Node nodeA,Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeB.gridY - nodeB.gridY);
        if (distanceX>distanceY)
        {
            return distanceY;
        }

        return distanceX;
    }
}