using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseAction : MonoBehaviour
{
    protected TowerStats tStats;

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
        tStats = towerController.towerInfo.towerStats;
        Init();
    }

    protected abstract void Init();
    protected void DestroyAction(string action)
    {
        Destroy(gameObject.GetComponent(System.Type.GetType(action)));
    }

    public abstract void Action();

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1 / towerController.AttackSpeed);
        onCooldown = false;
    }
}
