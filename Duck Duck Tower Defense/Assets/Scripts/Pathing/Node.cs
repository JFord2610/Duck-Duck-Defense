using UnityEngine;
using Priority_Queue;

public class Node : FastPriorityQueueNode
{
    public Vector3 worldPosition;
    public Vector2 gridPosition;
    public Vector3 vector;
    public float distanceToGoal;
    public float distancePenalty;
    public bool blocked;
    

    public Node(Vector3 _worldPosition, Vector2 _gridPosition, bool _blocked)
    {
        worldPosition = _worldPosition;
        gridPosition = _gridPosition;
        blocked = _blocked;
        distanceToGoal = -1;
        vector = Vector3.zero;
        distancePenalty = 0;
        Priority = distancePenalty;
    }
}
