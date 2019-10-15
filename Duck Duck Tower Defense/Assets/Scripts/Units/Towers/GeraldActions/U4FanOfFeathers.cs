using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U4FanOfFeathers : BaseAction
{
    float projectileAngleOffset = 10.0f;

    public override void Init(TowerController tc)
    {
        base.Init(tc);
        name = "U4FanOfFeathers";
        towerController.RemoveAction("U1Splitshot");
    }

    public override void Action()
    {
        BaseGoose targetGoose = null;
        switch (towerController.TargetingType)
        {
            case ETargetingType.Closest:
                targetGoose = towerController.GetClosestGoose();
                break;
            case ETargetingType.First:
                targetGoose = towerController.GetFirstGoose();
                break;
            case ETargetingType.Last:
                targetGoose = towerController.GetLastGoose();
                break;
        }
        if (towerController.GetNearbyGeese().Count > 0 && !onCooldown)
        {
            AttackGoose(targetGoose);
            towerController.StartCooldown(this);
        }
        if (targetGoose != null)
            towerController.RotateToPoint(targetGoose.Position);
    }

    private void AttackGoose(BaseGoose goose)
    {
        towerController.SetAttackAnimTrigger();
        float damage = towerController.Damage;
        Vector3 moveVector1 = goose.transform.position - towerController.transform.position;

        for (float i = -3.5f; i < 3.5f; i++)
        {
            Vector3 moveVector2 = Quaternion.Euler(0, 0, projectileAngleOffset * i) * moveVector1;

            GameObject proj1 = gameManager.projectileFactory.CreateProjectile(
            towerController,
            "GeraldProjectile",
            damage,
            moveVector2.normalized,
            towerController.transform.position,
            towerController.projInfo);
        }
    }
}
