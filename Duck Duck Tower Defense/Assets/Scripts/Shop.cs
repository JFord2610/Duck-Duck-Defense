using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Camera cam;
    public GameManagerScript gameManager;
    public UnitFactory unitFactory;
    public int duckBuyPrice;

    GameObject duck;

    private void Update()
    {

    }

    public void DuckButtonPressed()
    {
        if(gameManager.player.money > duckBuyPrice)
        {
            gameManager.player.money -= duckBuyPrice;
            gameManager.playerBusy = true;
            duck = unitFactory.SpawnDuck();
            TowerController tc = duck.GetComponent<TowerController>();
            if(tc != null)
            {
                tc.AttachToMouse();
            }
        }
        else
        {
            //maybe play notification that player cant afford item
        }
    }
}



