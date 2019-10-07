using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour
{
    private GameManagerScript gameManager;
    private Rigidbody2D rb2d;

    public float damage = 0;
    public float speed = 0;
    public int bounceTotal = 0;
    public float timeBeforeDeath = 10;

    List<GameObject> geeseHit = new List<GameObject>();
    float bounceDistance = float.MaxValue;
    int bounceCount = 0;
    bool bouncing = false;
    bool colliding = false;
    float timeAlive = 0;

    GameObject target = null;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > timeBeforeDeath)
            Destroy(gameObject);
        if(bouncing)
        {
            if(target != null)
            {
                rb2d.velocity = (target.transform.position - transform.position).normalized * speed;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (colliding)
                return;
            colliding = true;
            GameObject goose1 = collision.gameObject.transform.parent.gameObject;
            GameObject goose2 = null;
            foreach (GameObject g in geeseHit)
                if (g == goose1)
                {
                    colliding = false;
                    goose2 = FindNearestTarget();
                    target = goose2;
                    return;
                }
            goose2 = FindNearestTarget();
            goose1.GetComponent<BaseGoose>().Damage(damage);
            if (goose2 == null || bounceCount >= bounceTotal)
                Destroy(gameObject);
            else
            {
                bouncing = true;
                target = goose2;
                geeseHit.Add(goose1);
                bounceCount++;
            }
            colliding = false;
        }
    }

    private GameObject FindNearestTarget()
    {
        List<GameObject> geese = gameManager.geese;
        GameObject closestGoose = null;
        float dst = bounceDistance;
        foreach(GameObject goose in geese)
        {
            bool flag = false;
            foreach (GameObject g in geeseHit)
                if (g == goose)
                    flag = true;
            if (flag == true) continue;
            Vector3 v = goose.transform.position - transform.position;
            if (v.magnitude < dst)
            {
                dst = v.magnitude;
                closestGoose = goose;
            }
        }

        if (closestGoose == null)
            return null;
        return closestGoose;
    }
}
