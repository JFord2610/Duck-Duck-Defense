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

    public Vector3 Position { get { return transform.position; } }

    private void Awake()
    {
        moving = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die(false);
    }

    public void Die(bool endOfPath)
    {
        if (!endOfPath)
            gameManager.player.AddMoney(goldWorth);
        if (endOfPath)
            gameManager.player.Damage(lifeWorth);
        gameManager.geese.Remove(gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
            Die(true);
    }
}
