using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    internal List<GameObject> geese { get; private set; }
    internal PlayerHolder player { get; private set; }
    public TowerInfo[] towers;

    private void Awake()
    {
        geese = new List<GameObject>();
        player = GameObject.Find("PlayerHolder").GetComponent<PlayerHolder>();
    }

    internal TowerInfo GetTower(string name)
    {
        foreach (TowerInfo towerInfo in towers)
        {
            if (towerInfo.towerName == name)
                return towerInfo;
        }
        return null;
    }
}
