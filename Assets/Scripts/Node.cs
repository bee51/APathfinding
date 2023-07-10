using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node: IHeapItem<Node>
{
    public int HeapIndex
    {
        get => heapIndex;
        set => heapIndex=value;
    }

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public Node parent;
    
    public int hCost;
    public int gCost;
    public int heapIndex;

    public Node (bool walkable, Vector3 worldPosition,int gridX,int gridY)
    {
        this.walkable = walkable;
        this.worldPosition =worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int FCost => hCost + gCost;

    public int CompareTo(Node other)
    {
        int compare = FCost.CompareTo(other.FCost);
        if (compare==0)
        {
            compare = hCost.CompareTo(other.hCost);
        }

        return -compare;
    }

}
