using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Upgrades/Tower Upgrade")]
public class TowerUpgrade : ScriptableObject
{
    public string upgradeName = null;
    [TextArea]
    public string upgradeDescription = null;
    public Sprite upgradeSprite = null;
    public float damage = 0;
    public float attackSpeed = 0;
    public float attackRange = 0;
    public ETargetingType targetingType = ETargetingType.First;
    public bool hasUpgrade = false;
}
