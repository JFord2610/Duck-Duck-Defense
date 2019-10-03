using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGoose : MonoBehaviour
{
    public float health = 10;
    public float speed = 10;
    public int lifeWorth = 5;
    public int goldWorth = 10;

    public bool moving;

    public GameManagerScript gameManager;
    private PathFinder pathFinder;

    public Vector3 Position { get { return transform.position; } }

    private void Awake()
    {
        moving = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        pathFinder = gameObject.GetComponent<PathFinder>();
    }

    private void Start()
    {
        GetComponent<PathFinder>().StartPath();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        gameManager.player.AddMoney(goldWorth);
        gameManager.geese.Remove(gameObject);
        if(!moving)
            gameManager.player.Damage(lifeWorth);
        Destroy(gameObject);
    }
}
