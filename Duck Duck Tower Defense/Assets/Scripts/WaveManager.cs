using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public UnitFactory factory;

    public List<GeeseScript> geese;
    public Wave[] Waves;
    public int currentWaveIndex;
    public bool startWave;

    private Wave currentWave;

    private void Update()
    {
        if (startWave)
        {
            startWave = false;
            StartWaves();
        }
    }

    void StartWaves()
    {
        foreach (Wave w in Waves)
        {
            currentWave = w;
            currentWaveIndex = 0;
            StartCoroutine("DoWave");
        }
    }

    IEnumerator DoWave()
    {
        while (currentWaveIndex < currentWave.Geese)
        {
            factory.SpawnGeese(currentWave.geeseSpeed, currentWave.geeseHealth);
            currentWaveIndex++;
            yield return new WaitForSeconds(currentWave.timeDelta);
        }
    }

    public void StartRound()
    {
        startWave = true;
    }
}
