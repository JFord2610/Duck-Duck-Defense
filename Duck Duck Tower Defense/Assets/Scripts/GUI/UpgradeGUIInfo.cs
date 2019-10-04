using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GUI/Upgrade Info GUI")]
public class UpgradeGUIInfo : ScriptableObject
{
    [Header("Upgrade Info")]
    [Tooltip("Unique name of the tower used for identification")]
    public string towerName;
    [Tooltip("Image to represent upgrade in GUI")]
    public Sprite upgradeSprite;
    [Tooltip("Name of the upgrade")]
    public string upgradeHeader;
    [Tooltip("Short description of upgrade")]
    [Multiline]
    public string upgradeDescription;
}
