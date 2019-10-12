using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName ="Modifiers/Tower Modifier")]
public class TowerModifier : BaseModifier
{
    //stats
    public float damage;
    public float attackRange;
    public float attackSpeed;

    //projectile
    public ProjectileInfo projectileInfo;

    //action
    public string action;

    //visual
    public Sprite towerSprite;
    public AnimatorController animController;
}
