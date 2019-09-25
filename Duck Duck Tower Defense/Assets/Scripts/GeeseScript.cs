using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeeseScript : MonoBehaviour
{
    public float health = 10;
    public float speed = 10;
    public int lifeWorth = 5;
    public int goldWorth = 10;

    public float turnThreshold = 0.3f;

    public Path currentPath;
    public GameManagerScript gameManager;
    public Transform sprite;

    public Vector3 Position { get { return transform.position; } }

    private int currentPathIndex;
    private bool moving;
    private Vector3 MovingTo;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        moving = true;
    }
    private void Start()
    {
        currentPathIndex = 0;
        MovingTo = GetWorldPosFromPathIndex(currentPathIndex);
    }

    private void Update()
    {
        

    }

    private void FixedUpdate()
    {
        if ((transform.position - MovingTo).magnitude < turnThreshold)
        {
            moving = false;
        }

        if ((transform.position - MovingTo).magnitude > turnThreshold && moving)
        {
            transform.Translate((MovingTo - transform.position).normalized * Time.deltaTime * speed);
        }
        else
        {
            RotateToPoint(MovingTo = GetWorldPosFromPathIndex(currentPathIndex));

            currentPathIndex++;
            moving = true;
        }
    }

    Vector3 GetWorldPosFromPathIndex(int pathIndex)
    {
        if (pathIndex == currentPath.Waypoints.Length)
        {
            Die();
            gameManager.player.life -= lifeWorth;
            return Vector3.zero;
        }
        Vector2 point = currentPath.Waypoints[pathIndex];
        Node[,] grid = gameManager.GetGrid();
        Node n = grid[(int)point.x, (int)point.y];

        return n.worldPosition;
    }

    void RotateToPoint(Vector3 p)
    {
        Vector3 dir = p - sprite.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sprite.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void Die()
    {
        gameManager.player.AddMoney(goldWorth);
        gameManager.geese.Remove(gameObject);
        Destroy(gameObject);
    }
}
