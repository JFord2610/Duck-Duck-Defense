using UnityEngine;

public class Gerald : BaseTower
{
    protected override void Action()
    {
        BaseGoose targetGoose = null;
        switch (targetingType)
        {
            case ETargetingType.Closest:
                targetGoose = GetClosestGoose();
                break;
            case ETargetingType.First:
                targetGoose = GetFirstGoose();
                break;
            case ETargetingType.Last:
                targetGoose = GetLastGoose();
                break;
        }
        if (GetNearbyGeese().Count > 0 && !onCooldown)
        {
            onCooldown = true;
            AttackGoose(targetGoose);
            StartCoroutine("Cooldown");
        }
        if (targetGoose != null)
            RotateToPoint(targetGoose.Position);
    }

    private void AttackGoose(BaseGoose goose)
    {
        goose.Damage(damage);
    }
}
