using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 0;
    public EnemySpawner enemySpawner;
    public bool isRunningWave = false;
    public TextMeshProUGUI waveText;

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

        waveText.text = @$"Wave<br>#{currentWave.ToString()}";
    }

    public void PlayReadyForWave()
    {
        isRunningWave = true;
        enemySpawner.isRunningWave = isRunningWave;
        currentWave++;
    }

    public void WaveFinished()
    {
        isRunningWave = false;
        enemySpawner.finishedWave = false;
    }
}
