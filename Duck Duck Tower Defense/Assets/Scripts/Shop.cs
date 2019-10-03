﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shop : MonoBehaviour
{
    public Camera cam;
    public GameManagerScript gameManager;
    public UnitFactory unitFactory;
    public Collider2D Obstacles;
    public int duckBuyPrice;

    private bool holdingDuck;

    GameObject duckObj;
    BaseTower duckTower;
    SpriteRenderer duckSprite;

    private void FixedUpdate()
    {
        if (holdingDuck)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            duckObj.transform.position = new Vector3(pos.x, pos.y, 0.0f);
        }
    }
    private void LateUpdate()
    {
        if (holdingDuck)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!duckTower.colliding)
                {
                    holdingDuck = false;
                    gameManager.player.money -= duckBuyPrice;
                    duckTower.alive = true;
                    duckObj.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                holdingDuck = false;
                Destroy(duckObj);
            }
            if (duckTower.colliding)
            {
                duckSprite.color = new Color(1, 0.7f, 0.7f, 1);
            }
            else
            {
                duckSprite.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void DuckButtonPressed()
    {
        if (gameManager.player.money > duckBuyPrice)
        {
            duckObj = unitFactory.SpawnDuck();
            duckTower = duckObj.GetComponent<BaseTower>();
            duckSprite = duckTower.GetComponentInChildren<SpriteRenderer>();
            holdingDuck = true;
        }
        else
        {
            //To Do: maybe play notification that player cant afford item
        }
    }
}



