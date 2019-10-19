using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2U3CovertOperations : BaseAction
{
    GameObject depthChargePrefab;

    LayerMask river;
    float depthChargeCooldown = 0.333f;
    float cdCount = 0;
    public override void Init(TowerController tc)
    {
        base.Init(tc);
        river = LayerMask.GetMask("Obstacles");
        depthChargePrefab = Resources.Load<GameObject>("Prefabs/Projectiles/DepthCharge");
    }

    public override void Action()
    {
        if(cdCount > depthChargeCooldown)
        {
            Vector2 point;
            Collider2D hit = null;
            do
            {
                point = GetPoint();
                hit = Physics2D.OverlapPoint(point, river);
            } while (hit == null);
            GameObject depthCharge = Object.Instantiate(depthChargePrefab);
            depthCharge.transform.position = towerController.transform.position;
            depthCharge.GetComponent<DepthChargeController>().TravelToPoint(point);
            cdCount = 0;
        }
        else
        {
            cdCount += Time.deltaTime;
        }
    }

    private Vector2 GetPoint()
    {
        float angle = Random.value * 2 * Mathf.PI;
        float r = towerController.AttackRange * Mathf.Sqrt(Random.value);
        float x = (Mathf.Cos(angle) * r) + towerController.transform.position.x;
        float y = (Mathf.Sin(angle) * r) + towerController.transform.position.y;
        Vector2 point = new Vector2(x, y);
        return point;
    }
}
