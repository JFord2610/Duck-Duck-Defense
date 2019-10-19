using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthChargeController : MonoBehaviour
{
    public float damage = 0;
    public float explosionForce = 8;
    public float explosionRadius = 1;

    Vector2 targetPos;
    bool traveling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Vector3 v = explosionForce * (collision.transform.position - transform.position).normalized;
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].tag != "Enemy") continue;
                collisions[i].gameObject.GetComponent<BaseGoose>().Damage(damage);
                float dstScale = (collisions[i].transform.position - transform.position).magnitude / explosionRadius;
                collisions[i].GetComponent<Rigidbody2D>().velocity = v / dstScale;
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(traveling)
        {
            if ((targetPos - (Vector2)transform.position).magnitude < 0.01)
                traveling = false;
            else
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        }
    }

    public void TravelToPoint(Vector2 point)
    {
        targetPos = point;
        traveling = true;
    }
}
