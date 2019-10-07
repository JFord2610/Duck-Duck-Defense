using UnityEngine;
using System.Collections;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    public GameObject CreateProjectile(string type, float damage, float speed, Vector3 moveVector, Vector3 startPosition, int bounceCount)
    {
        GameObject g = null;
        for (int i = 0; i < prefabs.Length; i++)
        {
            if(prefabs[i].name == type)
                g = Instantiate(prefabs[i]);
        }
        if (g == null)
            throw new System.Exception($"Passed projectile type \"{type}\" was not found");
        g.transform.position = startPosition;
        ProjectileController pc = g.GetComponent<ProjectileController>();
        pc.damage = damage;
        pc.speed = speed;
        pc.bounceTotal = bounceCount;
        Rigidbody2D rb2d = g.GetComponent<Rigidbody2D>();
        rb2d.velocity = moveVector * speed;

        return g;
    }
}
