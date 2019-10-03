using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="Tower Info")]
public class TowerInfo : ScriptableObject
{
    public float damage = 0;
    public float attackSpeed = 0;
    public float attackRange = 0;
    public ETargetingType targetingType = ETargetingType.None;
}
