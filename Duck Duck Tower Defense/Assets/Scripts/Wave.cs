using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int Geese;
    public float geeseHealth;
    public float geeseSpeed;
    public float timeDelta;
}
