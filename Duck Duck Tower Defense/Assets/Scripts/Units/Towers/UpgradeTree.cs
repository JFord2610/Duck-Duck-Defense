using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Upgrades/Upgrade Tree")]
public class UpgradeTree : ScriptableObject
{
    public string towerName = null;
    public TowerUpgrade[] line1 = new TowerUpgrade[4];
    public TowerUpgrade[] line2 = new TowerUpgrade[4];

    internal int numberOfUpgrades
    {
        get
        {
            int num = 0;
            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i].hasUpgrade)
                    num++;
                if (line2[i].hasUpgrade)
                    num++;
            }
            return num;
        }
    }
    internal bool atMaxUpgrades { get { return numberOfUpgrades >= 6; } }

    public TowerUpgrade GetNextInLine(int line)
    {
        if (line == 1)
            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i].hasUpgrade)
                    continue;
                else
                    return line1[i];
            }
        else if (line == 2)
            for (int i = 0; i < line2.Length; i++)
            {
                if (line1[i].hasUpgrade)
                    continue;
                else
                    return line2[i];
            }
        return null;
    }
}
