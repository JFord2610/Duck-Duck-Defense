using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDuckBase : BaseAction
{
    public override void Init(TowerController tc)
    {
        base.Init(tc);
        name = "AgentDuckBase";
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
        GameObject proj = gameManager.projectileFactory.CreateProjectile(towerController, "GeraldProjectile", towerController.Damage, (goose.transform.position - towerController.transform.position).normalized, towerController.transform.position, towerController.projInfo);
    }
}
