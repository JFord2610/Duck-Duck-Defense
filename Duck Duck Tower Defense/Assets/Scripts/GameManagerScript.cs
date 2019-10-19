using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> geese { get; private set; }
    public List<GameObject> towers { get; private set; }
    public PlayerHolder player { get; private set; }
    public ProjectileFactory projectileFactory { get; private set; }

    public bool playerBusy { get { return playerBusyCount > 0; } }
    int playerBusyCount = 0;

    private void Awake()
    {
        geese = new List<GameObject>();
        towers = new List<GameObject>();
        player = GameObject.Find("PlayerHolder").GetComponent<PlayerHolder>();
        projectileFactory = GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactory>();
    }

    public void IncrementPlayerBusy()
    {
        playerBusyCount++;
    }

    public void DecrementPlayerBusy()
    {
        playerBusyCount--;
    }
}
