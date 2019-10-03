using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public UnitFactory unitFactory = null;
    
    public Round[] Rounds = null;
    private int roundIndex;
    
    private bool inProgress;

    private void Awake()
    {
        inProgress = false;
    }

    public void StartNextRound()
    {
        if (!inProgress)
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
            GooseData g = w.Geese[i];
            unitFactory.SpawnGoose(g.speed, g.health, g.lifeWorth, g.goldWorth, g.color);

            yield return new WaitForSeconds(w.timeDelta);
        }
    }
}
