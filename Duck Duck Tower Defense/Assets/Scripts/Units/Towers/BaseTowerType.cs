using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseTowerType : MonoBehaviour
{
    protected TowerStats tInfo;

    protected GameManagerScript gameManager = null;
    protected TowerController towerController = null;

    protected bool onCooldown;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        towerController = gameObject.GetComponent<TowerController>();
    }

    private void Start()
    {
        tInfo = towerController.towerStats;
        Init();
    }

    protected virtual void Init()
    {

    }

    public abstract void Action();

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1 / tInfo.attackSpeed);
        onCooldown = false;
    }
}
