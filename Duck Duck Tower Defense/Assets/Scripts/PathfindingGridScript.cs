using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathfindingGridScript : MonoBehaviour
{
    public int width;
    public int height;
    public Node[,] NodeGrid;

    public Path mapPath;

    private void Awake()
    {
        CreateGrid(width, height);
    }

    private void CreateGrid(int _width, int _height)
    {
        NodeGrid = new Node[width, height];
        Vector3 worldBottomLeft = transform.position - Vector3.right * width / 2 - Vector3.up * height / 2;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * x + Vector3.up * y;
                NodeGrid[x, y] = new Node(x, y, worldPoint + new Vector3(0.5f, 0.5f, 0));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1));
        if(NodeGrid != null)
        {
            foreach (Node n in NodeGrid)
            {
                //Gizmos.DrawWireCube(n.worldPosition, new Vector3(1, 1, 1));
                //Handles.Label(n.worldPosition + new Vector3(-0.3f, 0.0f, 0.0f), $"({n.gridX}, {n.gridY})");
            }
        }
    }
}
