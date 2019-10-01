using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Camera cam;
    public GameManagerScript gameManager;
    public UnitFactory unitFactory;
    public int duckBuyPrice;

    private bool holdingDuck;

    GameObject duck;

    public void DuckButtonPressed()
    {
        if(gameManager.player.money > duckBuyPrice)
        {
            gameManager.playerBusy = true;
            duck = unitFactory.SpawnDuck();
            holdingDuck = true;
            StartCoroutine("HoldingDuck");
        }
        else
        {
            //maybe play notification that player cant afford item
        }
    }

    IEnumerator HoldingDuck()
    {
        while(holdingDuck)
        {
            yield return new WaitForFixedUpdate();
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            duck.transform.position = new Vector3(pos.x, pos.y, 0.0f);
            if (Input.GetMouseButtonDown(0))
            {
                holdingDuck = false;
                gameManager.player.money -= duckBuyPrice;
                duck.GetComponent<BaseTower>().active = true;
            }
            if(Input.GetMouseButtonUp(1))
            {
                holdingDuck = false;
                Destroy(duck);
            }
        }
    }
}



