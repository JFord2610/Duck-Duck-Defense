using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> geese;
    public PlayerHolder player;
    public bool playerBusy = false;

    private void Awake()
    {
        geese = new List<GameObject>();
    }
}
