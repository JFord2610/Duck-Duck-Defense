using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Towers/Stats")]
public class TowerStats : ScriptableObject
{
    public float damage = 0;
    public float attackSpeed = 0;
    public float attackRange = 0;
    public ETargetingType targetingType = ETargetingType.None;

    internal TowerStats CreateCopy()
    {
        TowerStats newStats = CreateInstance<TowerStats>();

        newStats.damage = damage;
        newStats.attackSpeed = attackSpeed;
        newStats.attackRange = attackRange;
        newStats.targetingType = targetingType;

        return newStats;
    }
}
