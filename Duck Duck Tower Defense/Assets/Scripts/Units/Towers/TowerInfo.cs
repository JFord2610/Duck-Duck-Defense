using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Towers/Tower Info")]
public class TowerInfo : ScriptableObject
{
    public string towerName = null;
    public TowerStats towerStats = null;
    public TowerVisuals towerVisuals = null;
    public UpgradeTree upgradeTree = null;
    public int towerCost = 0;
    public string towerType = null;
}
