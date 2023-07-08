using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    
    
    public int hCost;
    public int gCost;

    public Node (bool walkable, Vector3 worldPosition,int gridX,int gridY)
    {
        this.walkable = walkable;
        this.worldPosition =worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost => hCost + gCost;

}