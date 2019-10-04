using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Towers/Stats")]
public class TowerStats : ScriptableObject
{
    public float damage = 0;
    public float attackSpeed = 0;
    public float attackRange = 0;
    public ETargetingType targetingType = ETargetingType.None;
}
