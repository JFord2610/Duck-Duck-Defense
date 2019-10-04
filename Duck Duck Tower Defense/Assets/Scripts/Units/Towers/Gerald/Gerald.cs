using UnityEngine;

public class Gerald : BaseTowerType
{
    Animator anim = null;

    protected override void Init()
    {
        base.Init();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    public override void Action()
    {
        BaseGoose targetGoose = null;
        switch (tInfo.targetingType)
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
        goose.Damage(tInfo.damage);
    }
}
