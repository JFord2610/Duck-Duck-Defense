using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Wave Manager/Wave")]
public class Wave : ScriptableObject
{
    public List<GooseInfo> Geese = null;
    public float timeDelta = 0;
}
