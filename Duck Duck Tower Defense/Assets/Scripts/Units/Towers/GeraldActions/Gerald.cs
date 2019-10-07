using UnityEngine;

public class Gerald : BaseAction
{
    Animator anim = null;

    float projectileSpeed = 3;

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
        GameObject proj = gameManager.projectileFactory.CreateProjectile("GeraldProjectile", towerController.Damage, projectileSpeed, (goose.transform.position - transform.position).normalized, transform.position, 0);
    }
}
