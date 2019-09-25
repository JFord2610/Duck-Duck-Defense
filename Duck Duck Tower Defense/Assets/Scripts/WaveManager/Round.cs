using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Wave Manager/Round")]
public class Round : ScriptableObject
{
    public Wave[] waves = null;
    public float[] timeBetweenWaves = null;
}