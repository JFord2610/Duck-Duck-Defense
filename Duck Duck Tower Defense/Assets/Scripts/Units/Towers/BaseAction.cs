using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseAction
{
    public string name;
    public bool onCooldown = false;

    protected TowerStats tStats;
    protected GameManagerScript gameManager = null;
    protected TowerController towerController = null;

    public virtual void Init(TowerController tc)
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        towerController = tc;
        tStats = towerController.towerInfo.towerStats;
    }

    public virtual void Action()
    {

    }

    public virtual void Destroy()
    {

    }
}
