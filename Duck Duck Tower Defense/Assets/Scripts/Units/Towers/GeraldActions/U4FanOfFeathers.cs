using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U4FanOfFeathers : BaseAction
{
    Animator anim = null;

    float projectileSpeed = 9;
    float projectileAngleOffset = 10.0f;

    protected override void Init()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        DestroyAction("U1Splitshot");
        towerController.action = this;
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
            onCooldown = true;
            StartCoroutine("Cooldown");
        }
        if (targetGoose != null)
            towerController.RotateToPoint(targetGoose.Position);
    }

    private void AttackGoose(BaseGoose goose)
    {
        anim.SetTrigger("Attack");
        float damage = towerController.Damage;
        Vector3 moveVector1 = goose.transform.position - transform.position;

        for (float i = -3.5f; i < 3.5f; i++)
        {
            Vector3 moveVector2 = Quaternion.Euler(0, 0, projectileAngleOffset * i) * moveVector1;

            GameObject proj1 = gameManager.projectileFactory.CreateProjectile(
            "GeraldProjectile",
            damage,
            moveVector2.normalized,
            transform.position,
            towerController.projInfo);
        }

        
    }
}
