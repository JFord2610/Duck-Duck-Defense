using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public PathfindingGridScript gridScript;
    public List<GameObject> geese;
    public PlayerHolder player;
    public bool playerBusy = false;

    private void Awake()
    {
        geese = new List<GameObject>();
    }

    public Node[,] GetGrid()
    {
        return gridScript.NodeGrid;
    }

}
