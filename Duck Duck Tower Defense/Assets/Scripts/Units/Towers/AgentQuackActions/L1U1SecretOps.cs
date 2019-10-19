using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1U1SecretOps : BaseAction
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] GooseModifier goldWorthModifier;

    public override void Init(TowerController tc)
    {
        base.Init(tc);
        name = "L1U1SecretOps";
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        goldWorthModifier = Resources.Load<GooseModifier>("Scriptables/Geese/Modifiers/+5GoldWorth");
        waveManager.modifiers.Add(goldWorthModifier);
    }

    public override void Destroy()
    {
        waveManager.modifiers.Remove(goldWorthModifier);
    }
}
