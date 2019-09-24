using System;
using System.Collections.Generic;
using UnityEngine;
public struct Node
{
    public int gridX;
    public int gridY;
    public Vector3 worldPosition;

    public Node(int _gridX, int _gridY, Vector3 _worldPosition)
    {
        gridX = _gridX;
        gridY = _gridY;
        worldPosition = _worldPosition;
    }
}
