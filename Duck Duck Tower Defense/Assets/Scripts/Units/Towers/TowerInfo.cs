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

    public TowerInfo CreateCopy()
    {
        TowerInfo newInfo = CreateInstance<TowerInfo>();

        newInfo.towerName = towerName;
        newInfo.towerStats = towerStats;
        newInfo.towerVisuals = towerVisuals;
        newInfo.upgradeTree = upgradeTree.CreateCopy();
        newInfo.towerCost = towerCost;
        newInfo.towerType = towerType;

        return newInfo;
    }
}
