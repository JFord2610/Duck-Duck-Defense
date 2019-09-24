using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Path")]
public class Path : ScriptableObject
{
    public Vector2[] Waypoints;
}
