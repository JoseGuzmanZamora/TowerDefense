using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingScreen : MonoBehaviour
{
    public GameObject blockingScreen;
    public bool isActive = false;
    public string type = "";
    public DefenseController defenseController;
    public EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        blockingScreen.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == "gameplay")
        {
            // block if is positioning is true
            if (defenseController.isPositioning || enemySpawner.isRunningWave)
            {
                blockingScreen.SetActive(true);
            }
            else
            {
                blockingScreen.SetActive(false);
            }
        }
        else if (type == "inventory")
        {
            if (enemySpawner.isRunningWave)
            {
                blockingScreen.SetActive(true);
            }
            else
            {
                blockingScreen.SetActive(false);
            }
        }
    }
}
