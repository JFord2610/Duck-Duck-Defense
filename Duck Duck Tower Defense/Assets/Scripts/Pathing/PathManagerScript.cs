using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class PathManagerScript : MonoBehaviour
{
    public LayerMask obstacleMask;
    public Node[,] Grid;
    public Vector2 gridWorldSize;
    public int gridSizeX, gridSizeY;
    [Range(0.01f, 0.2f)]
    public float nodeRadius;
    [Range(0, 0.5f)]
    public float spacing = 0.1f;
    [Range(0, 20)]
    public float wallPenalty = 5;
    [Range(2, 10)]
    public int blurSize;
    public Transform start;
    public Transform goal;

    public bool showGizmos = true;
    public bool drawVectorLines = false;

    float nodeDiameter;

    float maxDistance = 1;

    void Awake()
    {
        nodeDiameter = 2 * nodeRadius;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
        Node startNode = GetNodeFromWorldPoint(start.position);
        Node goalNode = GetNodeFromWorldPoint(goal.position);
        GenerateHeatMap(startNode, goalNode);
        GenerateVectorField();
    }

    void CreateGrid()
    {
        maxDistance = 1;
        Grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * (gridWorldSize.x / 2) - Vector3.up * (gridWorldSize.y / 2);
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool blocked = Physics2D.OverlapCircle(worldPoint, nodeRadius - nodeRadius * 0.9999999f, obstacleMask) == null;
                Grid[x, y] = new Node(worldPoint, new Vector2(x, y), blocked);
                if (blocked)
                    Grid[x, y].distancePenalty = wallPenalty;
            }
        }

    }

    void GenerateVectorField()
    {
        foreach (Node n in Grid)
        {
            if (n.blocked) continue;
            Node[] arr = GetNeighbors(n);
            Node next = null;
            float dst = float.MaxValue;
            foreach (Node neighbor in arr)
            {
                if (neighbor.blocked) continue;
                if (neighbor.distanceToGoal < dst)
                {
                    next = neighbor;
                    dst = neighbor.distanceToGoal;
                }
            }
            n.vector = (next.worldPosition - n.worldPosition).normalized;
        }
    }

    void GenerateHeatMap(Node startNode, Node targetNode)
    {
        BlurPenaltyMap(blurSize);
        FastPriorityQueue<Node> q = new FastPriorityQueue<Node>(gridSizeX * gridSizeY);
        targetNode.distanceToGoal = 0;
        q.Enqueue(targetNode, 0);

        while (q.Count > 0)
        {
            Node n = q.Dequeue();
            Node[] arr = GetNeighbors(n);
            for (int i = 0; i < arr.Length; i++)
            {
                Node w = arr[i];
                if (w.distanceToGoal != -1) continue;
                if (!w.blocked)
                    w.distanceToGoal = (1 + n.distanceToGoal) + w.distancePenalty;
                if (w.blocked) continue;
                if (w.distanceToGoal > maxDistance) maxDistance = w.distanceToGoal;
                q.Enqueue(w, w.distanceToGoal);
            }
        }
    }

    Node[] GetNeighbors(Node n)
    {
        List<Node> nodes = new List<Node>();
        for (int x = -1; x != 2; x++)
        {
            for (int y = -1; y != 2; y++)
            {
                if (x == 0 && y == 0) continue;
                int xPos = Mathf.Clamp((int)n.gridPosition.x + x, 0, gridSizeX - 1);
                int yPos = Mathf.Clamp((int)n.gridPosition.y + y, 0, gridSizeY - 1);
                Node newNode = Grid[xPos, yPos];
                nodes.Add(newNode);
            }
        }
        return nodes.ToArray();
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPoint)
    {
        float percentX = (worldPoint.x + gridWorldSize.x / 2 - transform.position.x) / gridWorldSize.x;
        float percentY = (worldPoint.y + gridWorldSize.y / 2 - transform.position.y) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return Grid[x, y];
    }

    void BlurPenaltyMap(int blurSize)
    {
        int kernalSize = blurSize * 2 + 1;
        int kernalExtents = (kernalSize - 1) / 2;

        float[,] penaltiesHorizontalPass = new float[gridSizeX, gridSizeY];
        float[,] penaltiesVerticalPass = new float[gridSizeX, gridSizeY];

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = -kernalExtents; x < kernalExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernalExtents);
                penaltiesHorizontalPass[0, y] += Grid[sampleX, y].distancePenalty;
            }

            for (int x = 1; x < gridSizeX; x++)
            {
                int removeIndex = Mathf.Clamp(x - kernalExtents - 1, 0, gridSizeX);
                int addIndex = Mathf.Clamp(x + kernalExtents, 0, gridSizeX - 1);

                penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - Grid[removeIndex, y].distancePenalty + Grid[addIndex, y].distancePenalty;
            }
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = -kernalExtents; y < kernalExtents; y++)
            {
                int sampleY = Mathf.Clamp(y, 0, kernalExtents);
                penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
            }

            float blurredPenalty = (float)penaltiesVerticalPass[x, 0] / (kernalSize * kernalSize);
            Grid[x, 0].distancePenalty = blurredPenalty;

            for (int y = 1; y < gridSizeY; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernalExtents - 1, 0, gridSizeY);
                int addIndex = Mathf.Clamp(y + kernalExtents, 0, gridSizeY - 1);

                penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];

                blurredPenalty = (float)penaltiesVerticalPass[x, y] / (kernalSize * kernalSize);
                Grid[x, y].distancePenalty = blurredPenalty;
                Grid[x, y].distancePenalty = blurredPenalty;
            }
        }
    }
    private void OnValidate()
    {
        nodeDiameter = 2 * nodeRadius;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
        Node startNode = GetNodeFromWorldPoint(start.position);
        Node goalNode = GetNodeFromWorldPoint(goal.position);
        GenerateHeatMap(startNode, goalNode);
        GenerateVectorField();
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Node n = Grid[x, y];
                if (n.blocked)
                    continue;
                Gizmos.color = new Color(0, 0, n.distanceToGoal / maxDistance, 0.6f);
                Gizmos.DrawCube(n.worldPosition, (nodeDiameter - spacing) * Vector3.one);
                Gizmos.color = Color.red;
                if (drawVectorLines)
                    Gizmos.DrawLine(n.worldPosition, n.vector * nodeRadius + n.worldPosition);
            }
        }
    }
}
