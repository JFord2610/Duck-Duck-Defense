using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GUI/Tower GUI Info")]
public class TowerGUIInfo : ScriptableObject
{
    public string towerName;
    public TowerStats towerStats;
    public UpgradeGUIInfo upgradeInfo1;
    public UpgradeGUIInfo upgradeInfo2;
}
