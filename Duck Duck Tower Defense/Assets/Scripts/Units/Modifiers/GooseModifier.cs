using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Modifiers/Goose Modifier")]
public class GooseModifier : BaseModifier
{
    public float health = 0;
    public float percentHealth = 0;
    public float speed = 0;
    public float percentSpeed = 0;
    public int lifeWorth = 0;
    public int goldWorth = 0;
    public float percentGoldWorth = 0;
    public string action = null;
}
