using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Modifiers/Goose Modifier")]
public class GooseModifier : BaseModifier
{
    public float health;
    public float percentHealth;
    public float speed;
    public float percentSpeed;
    public int lifeWorth;
    public int goldWorth;
    public float percentGoldWorth;
    public string action;
}
