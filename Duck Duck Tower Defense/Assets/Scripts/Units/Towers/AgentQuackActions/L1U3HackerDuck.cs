using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L1U3HackerDuck : BaseAction
{
    List<BaseGoose> SlowedGeese = null;
    GooseModifier slowModifier = null;

    public override void Init(TowerController tc)
    {
        base.Init(tc);
        SlowedGeese = new List<BaseGoose>();
        slowModifier = (GooseModifier)Resources.Load("Scriptables/Geese/Modifiers/40%Slow");
    }

    public override void Action()
    {
        List<BaseGoose> newGeeseList = towerController.GetNearbyGeese();
        for (int i = 0; i < SlowedGeese.Count; i++)
        {
            BaseGoose g = SlowedGeese[i];
            if (g == null)
            {
                SlowedGeese.Remove(g);
                continue;
            }
            float dst = (g.transform.position - towerController.transform.position).magnitude;
            if (dst > towerController.AttackRange)
                RemoveGoose(g);
        }
        List<BaseGoose> except = newGeeseList.Except(SlowedGeese).ToList();
        except.ForEach(g =>
        {
            AddGoose(g);
        });
    }

    private void AddGoose(BaseGoose goose)
    {
        goose.AddModifier(slowModifier);
        SlowedGeese.Add(goose);
    }

    private void RemoveGoose(BaseGoose goose)
    {
        goose.RemoveModifier(slowModifier);
        SlowedGeese.Remove(goose);
    }

    public override void Destroy()
    {
        base.Destroy();
        SlowedGeese.ForEach(g =>
        {
            RemoveGoose(g);
        });
    }
}
