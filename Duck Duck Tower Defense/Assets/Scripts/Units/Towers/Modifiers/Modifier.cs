using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName ="Modifier")]
public class Modifier : ScriptableObject
{
    //base
    [EnumFlags]
    public EModifierType modType;
    public string modName;
    [TextArea]
    public string modDescription;
    public Sprite modSprite;

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
