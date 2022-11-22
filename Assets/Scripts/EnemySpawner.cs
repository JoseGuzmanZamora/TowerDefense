using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<PathExecutor> enemiesPrefabs;
    public PathDeterminator pathInstructions;
    public CityEconomyManager economyManager;
    public float spawnerInterval = 1f;
    public int enemyLimit = 10;
    public List<GameObject> availableEnemies;
    private float spawnerCounter = 0;
    private int enemyAmount = 0;

    void Update()
    {
       spawnerCounter += Time.deltaTime;
       if (
        spawnerCounter >= spawnerInterval && 
        pathInstructions.instructionsReady &&
        enemyAmount < enemyLimit
        )
       {
            var enemySelected = enemiesPrefabs[0];
            var newEnemy = Instantiate(enemySelected, transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            enemyAmount ++;
            availableEnemies.Add(newEnemy.gameObject);
            newEnemy.pathInstructionsSetup = pathInstructions;
            spawnerCounter = 0;

            // Get the enemies life controllers
            var lifeController = newEnemy.gameObject.GetComponent<EnemyLifeController>();
            lifeController.economyManager = economyManager;
       }
    }
}
