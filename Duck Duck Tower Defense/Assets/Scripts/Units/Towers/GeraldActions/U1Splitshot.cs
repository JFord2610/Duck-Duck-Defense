using UnityEngine;

public class U1Splitshot : BaseAction
{
    float projectileAngleOffset = 30.0f;

    public override void Init(TowerController tc)
    {
        base.Init(tc);
        name = "U1Splitshot";
        towerController.RemoveAction("GeraldBase");
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
        Vector3 moveVector2 = goose.transform.position - towerController.transform.position;
        Vector3 moveVector3 = goose.transform.position - towerController.transform.position;
        moveVector2 = Quaternion.Euler(0, 0, projectileAngleOffset) * moveVector2;
        moveVector3 = Quaternion.Euler(0, 0, -projectileAngleOffset) * moveVector3;

        GameObject proj1 = gameManager.projectileFactory.CreateProjectile(towerController, "GeraldProjectile", damage, moveVector1.normalized, towerController.transform.position, towerController.projInfo);
        GameObject proj2 = gameManager.projectileFactory.CreateProjectile(towerController, "GeraldProjectile", damage, moveVector2.normalized, towerController.transform.position, towerController.projInfo);
        GameObject proj3 = gameManager.projectileFactory.CreateProjectile(towerController, "GeraldProjectile", damage, moveVector3.normalized, towerController.transform.position, towerController.projInfo);
    }
}
