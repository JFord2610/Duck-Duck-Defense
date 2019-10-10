using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour
{
    private GameManagerScript gameManager;
    private Rigidbody2D rb2d;

    public ProjectileInfo projInfo;
    public TowerController towerController;

    public float damage = 0;
    public float speed = 0;
    public int bounceTotal = 0;
    public int pierceTotal = 1;
    public float maxDistance = 10;

    List<GameObject> geeseHit = new List<GameObject>();
    int bounceCount = 0;
    private int pierceCount = 0;
    bool bouncing = false;
    bool colliding = false;
    float timeAlive = 0;

    GameObject target = null;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        rb2d = GetComponent<Rigidbody2D>();
        speed = projInfo.speed;
        bounceTotal = projInfo.bounces;
        pierceTotal = projInfo.pierce;
        maxDistance = projInfo.timeBeforeDeath;
    }

    private void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > maxDistance)
            Destroy(gameObject);
        if (bouncing)
        {
            if (target != null)
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
            GameObject goose1 = collision.gameObject;
            GameObject goose2 = null;
            foreach (GameObject g in geeseHit)
                if (g == goose1)
                {
                    goose2 = FindNearestTarget();
                    target = goose2;
                    colliding = false;
                    return;
                }

            goose1.GetComponent<BaseGoose>().Damage(damage);

            if (pierceCount < pierceTotal)
            {
                if (bounceCount < bounceTotal)
                {
                    bouncing = true;
                    bounceCount++;
                    target = goose1;
                    geeseHit.Add(goose1);
                    colliding = false;
                    return;
                }
                else
                {
                    pierceCount++;
                    bounceCount = 0;
                    colliding = false;
                    return;
                }
            }

            Destroy(gameObject);

            colliding = false;
        }
    }

    private GameObject FindNearestTarget()
    {
        List<GameObject> geese = gameManager.geese;
        GameObject closestGoose = null;
        float dst = float.MaxValue;
        foreach (GameObject goose in geese)
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
