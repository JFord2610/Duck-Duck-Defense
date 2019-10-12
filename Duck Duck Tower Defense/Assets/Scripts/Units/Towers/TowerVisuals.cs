using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName ="Towers/Visuals")]
public class TowerVisuals : ScriptableObject
{
    public Sprite sprite;
    public AnimatorController animController;

    internal TowerVisuals CreateCopy()
    {
        TowerVisuals newVis = CreateInstance<TowerVisuals>();

        newVis.sprite = sprite;
        newVis.animController = animController;

        return newVis;
    }
}
