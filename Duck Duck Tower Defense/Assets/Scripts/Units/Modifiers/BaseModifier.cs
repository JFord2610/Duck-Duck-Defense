using UnityEngine;
using System.Collections;

public abstract class BaseModifier : ScriptableObject
{
    [EnumFlags]
    public EModifierType modType;
    public string modName;
    [TextArea]
    public string modDescription;
    public Sprite modSprite;
}
