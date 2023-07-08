using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public Node parent;
    
    public int hCost;
    public int gCost;

    public Node (bool walkable, Vector3 worldPosition,int gridX,int gridY)
    {
        this.walkable = walkable;
        this.worldPosition =worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int FCost => hCost + gCost;

}
