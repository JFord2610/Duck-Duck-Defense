using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> geese { get; private set; }
    public PlayerHolder player { get; private set; }
    public ProjectileFactory projectileFactory { get; private set; }

    public TowerInfo[] towers;

    private void Awake()
    {
        geese = new List<GameObject>();
        player = GameObject.Find("PlayerHolder").GetComponent<PlayerHolder>();
        projectileFactory = GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactory>();
    }

}
