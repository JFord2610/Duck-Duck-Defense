using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Towers/Tower Info")]
public class TowerInfo : ScriptableObject
{
    public string towerName = null;
    public TowerStats towerStats = null;
    public ProjectileInfo projectileInfo = null;
    public TowerVisuals towerVisuals = null;
    public UpgradeTree upgradeTree = null;
    public int towerCost = 0;
    public string action = null;

    public TowerInfo CreateCopy()
    {
        TowerInfo newInfo = CreateInstance<TowerInfo>();

        newInfo.towerName = towerName;
        newInfo.towerStats = towerStats.CreateCopy();
        newInfo.projectileInfo = projectileInfo.CreateCopy();
        newInfo.towerVisuals = towerVisuals.CreateCopy();
        newInfo.upgradeTree = upgradeTree.CreateCopy();
        newInfo.towerCost = towerCost;
        newInfo.action = action;

        return newInfo;
    }
}
