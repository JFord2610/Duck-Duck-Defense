using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L2U2SecretFiles : BaseAction
{
    TowerModifier secretIntelligenceModifier = null;
    List<TowerController> towersWithMod = null;
    public override void Init(TowerController tc)
    {
        base.Init(tc);
        secretIntelligenceModifier = Resources.Load<TowerModifier>("Scriptables/Towers/AgentQuack/Modifiers/SecretIntelligence");
        towersWithMod = new List<TowerController>();
    }

    public override void Action()
    {
        List<TowerController> newTowerList = GetNearbyTowers();
        for (int i = 0; i < towersWithMod.Count; i++)
        {
            TowerController t = towersWithMod[i];
            if(t == null)
            {
                newTowerList.Remove(t);
                continue;
            }
            float dst = (t.transform.position - towerController.transform.position).magnitude;
            if (dst > towerController.AttackRange)
                RemoveTower(t);
        }
        List<TowerController> except = newTowerList.Except(towersWithMod).ToList();
        except.ForEach(t =>
        {
            AddTower(t);
        });
    }

    private void AddTower(TowerController tc)
    {
        tc.AddModifier(secretIntelligenceModifier);
        towersWithMod.Add(tc);
    }

    private void RemoveTower(TowerController tc)
    {
        tc.RemoveModifier(secretIntelligenceModifier);
        towersWithMod.Remove(tc);
    }

    private List<TowerController> GetNearbyTowers()
    {
        List<TowerController> list = new List<TowerController>();
        gameManager.towers.ForEach(obj =>
        {
            float dst = (obj.transform.position - towerController.transform.position).magnitude;
            if (dst < towerController.AttackRange)
                list.Add(obj.GetComponent<TowerController>());
        });
        return list;
    }

    public override void Destroy()
    {
        base.Destroy();
        towersWithMod.ForEach(t =>
        {
            RemoveTower(t);
        });
    }

}
