using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="Geese/Base Goose")]
public class GooseData : ScriptableObject
{
    public float health = 0;
    public float speed = 0;
    public int lifeWorth = 0;
    public int goldWorth = 0;
}
