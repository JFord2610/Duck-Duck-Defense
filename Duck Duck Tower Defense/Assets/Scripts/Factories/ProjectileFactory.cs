using UnityEngine;
using System.Collections;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs = new GameObject[1];

    public GameObject CreateProjectile(string type, float damage, Vector3 moveVector, Vector3 startPosition, ProjectileInfo projInfo)
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
        pc.projInfo = projInfo.CreateCopy();
        Rigidbody2D rb2d = g.GetComponent<Rigidbody2D>();
        rb2d.velocity = moveVector * projInfo.speed;
        
        return g;
    }
}
