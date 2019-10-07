using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Upgrades/Tower Upgrade")]
public class TowerUpgrade : ScriptableObject
{
    public string upgradeName = null;
    [TextArea]
    public string upgradeDescription = null;
    public Sprite upgradeSprite = null;
    public Modifier modifier = null;
    public int cost = 0;
    public bool hasUpgrade = false;

    internal TowerUpgrade CreateCopy()
    {
        TowerUpgrade newTowerUpgrade = CreateInstance<TowerUpgrade>();

        newTowerUpgrade.upgradeName = upgradeName;
        newTowerUpgrade.upgradeDescription = upgradeDescription;
        newTowerUpgrade.upgradeSprite = upgradeSprite;
        newTowerUpgrade.modifier = modifier;
        newTowerUpgrade.cost = cost;
        newTowerUpgrade.hasUpgrade = false;

        return newTowerUpgrade;
    }
}
