using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class BaseTower : MonoBehaviour
{
    public float damage = 0, attackRange = 0, attackSpeed = 0;

    protected bool onCooldown = false;
    public bool active = false;
    public ETargetingType targetingType = ETargetingType.Closest;

    GameManagerScript gameManager = null;
    Transform sprite;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        sprite = transform.GetChild(0);
    }

    private void Update()
    {
        if (active)
            Action();
    }

    protected abstract void Action();

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1 / attackSpeed);
        onCooldown = false;
    }

    protected List<BaseGoose> GetNearbyGeese()
    {
        List<BaseGoose> geese = new List<BaseGoose>();

        foreach (GameObject g in gameManager.geese)
        {
            float dst = (g.transform.position - transform.position).magnitude;
            if (dst <= attackRange)
                geese.Add(g.GetComponent<BaseGoose>());
        }

        return geese;
    }

    protected BaseGoose GetClosestGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;
        float dst = int.MaxValue;

        List<BaseGoose> geese = GetNearbyGeese();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            float mag = (goosePos - pos).magnitude;
            if (mag < dst)
            {
                dst = mag;
                goose = g;
            }
        }

        return goose;
    }

    protected BaseGoose GetFirstGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;

        List<BaseGoose> geese = GetNearbyGeese();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < attackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }

    protected BaseGoose GetLastGoose()
    {
        BaseGoose goose = null;
        Vector3 pos = transform.position;
        List<BaseGoose> geese = GetNearbyGeese();
        geese.Reverse();
        foreach (BaseGoose g in geese)
        {
            Vector3 goosePos = g.transform.position;
            if ((pos - goosePos).magnitude < attackRange)
            {
                goose = g;
                break;
            }
        }
        return goose;
    }

    protected void RotateToPoint(Vector3 p)
    {
        Vector3 dir = p - sprite.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sprite.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
