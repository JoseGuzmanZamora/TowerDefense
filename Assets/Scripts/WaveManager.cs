using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public EnemySpawner enemySpawner;
    public bool isRunningWave = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (enemySpawner.finishedWave)
        {
            enemySpawner.enemyLimit += UnityEngine.Random.Range(3, 7);
            WaveFinished();
        }
    }

    public void PlayReadyForWave()
    {
        isRunningWave = true;
        enemySpawner.isRunningWave = isRunningWave;
    }

    public void WaveFinished()
    {
        isRunningWave = false;
        enemySpawner.finishedWave = false;
    }
}
