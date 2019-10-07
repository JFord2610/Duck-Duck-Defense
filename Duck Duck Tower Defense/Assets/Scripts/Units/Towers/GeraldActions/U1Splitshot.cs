using UnityEngine;

public class U1Splitshot : BaseAction
{
    Animator anim = null;

    float projectileSpeed = 9;

    protected override void Init()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
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
        Vector3 moveVector2 = goose.transform.position - transform.position;
        Vector3 moveVector3 = goose.transform.position - transform.position;
        moveVector2 = Quaternion.Euler(0, 0, 30.0f) * moveVector2;
        moveVector3 = Quaternion.Euler(0, 0, -30.0f) * moveVector3;

        GameObject proj1 = gameManager.projectileFactory.CreateProjectile("GeraldProjectile", damage, projectileSpeed, moveVector1.normalized, transform.position, 0);
        GameObject proj2 = gameManager.projectileFactory.CreateProjectile("GeraldProjectile", damage, projectileSpeed, moveVector2.normalized, transform.position, 0);
        GameObject proj3 = gameManager.projectileFactory.CreateProjectile("GeraldProjectile", damage, projectileSpeed, moveVector3.normalized, transform.position, 0);
    }
}
