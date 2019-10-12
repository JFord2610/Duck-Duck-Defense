using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Geese/Goose Stats")]
public class GooseStats : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public int lifeWorth;
    public int goldWorth;
    
    public GooseStats CreateCopy()
    {
        GooseStats newStats = CreateInstance<GooseStats>();
        newStats.maxHealth = maxHealth;
        newStats.speed = speed;
        newStats.lifeWorth = lifeWorth;
        newStats.goldWorth = goldWorth;
        return newStats;
    }
}
