using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public UnitFactory unitFactory = null;

    public List<GooseModifier> modifiers = null;

    public Round[] Rounds = null;
    private int roundIndex;
    
    private bool inProgress;

    private void Awake()
    {
        modifiers = new List<GooseModifier>();
        inProgress = false;
    }

    public void StartNextRound()
    {
        if (!inProgress && roundIndex < Rounds.Length)
        {
            if (Rounds == null || unitFactory == null)
                return;
            inProgress = true;
            StartCoroutine("RoundLoop", Rounds[roundIndex]);
            roundIndex++;
        }
    }

    IEnumerator RoundLoop(Round r)
    {
        for (int i = 0; i < r.waves.Length; i++)
        {
            yield return StartCoroutine("WaveLoop", r.waves[i]);

            if (r.timeBetweenWaves.Length == 1)
                yield return new WaitForSeconds(r.timeBetweenWaves[0]);
            else
                yield return new WaitForSeconds(r.timeBetweenWaves[i]);
        }
        inProgress = false;
    }

    IEnumerator WaveLoop(Wave w)
    {
        for (int i = 0; i < w.Geese.Count; i++)
        {
            GameObject goose = unitFactory.SpawnGoose(w.Geese[i]);
            if (modifiers.Count > 0)
                modifiers.ForEach(mod => { goose.GetComponent<BaseGoose>().AddModifier(mod); });

            yield return new WaitForSeconds(w.timeDelta);
        }
    }
}
