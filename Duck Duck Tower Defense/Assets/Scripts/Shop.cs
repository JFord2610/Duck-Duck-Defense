using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shop : MonoBehaviour
{
    public Camera cam;
    public GameManagerScript gameManager;
    public UnitFactory unitFactory;
    public int duckBuyPrice;

    private bool holdingDuck;

    GameObject duckObj;
    BaseTower duckTower;

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        if (holdingDuck)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(!duckTower.colliding)
                {
                    holdingDuck = false;
                    gameManager.player.money -= duckBuyPrice;
                    duckTower.alive = true;
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                holdingDuck = false;
                Destroy(duckObj);
            }
        }
    }

    public void DuckButtonPressed()
    {
        if (gameManager.player.money > duckBuyPrice)
        {
            duckObj = unitFactory.SpawnDuck();
            duckTower = duckObj.GetComponent<BaseTower>();
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
        while (holdingDuck)
        {
            yield return new WaitForFixedUpdate();
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            duckObj.transform.position = new Vector3(pos.x, pos.y, 0.0f);
        }
    }
}



