using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DefenseController : MonoBehaviour
{
    // TODO: this will be a list of the possible prefabs
    public PositionDefense defensePrefab;
    public List<PositionDefense> spawnedPrefabs;
    public Tilemap cityTilemap;
    public EnemySpawner enemySpawner;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && AllSpawnedArePositioned())
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            spawnedDefense.cityTileMap = cityTilemap;
            spawnedDefense.transform.parent = transform;
            spawnedPrefabs.Add(spawnedDefense);
        }
        
        // set the objective to all children
        var childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            var child = transform.GetChild(i);
            var childAttackController = child.gameObject.GetComponent<AttackController>();
            childAttackController.objective = GetEnemy();
        }
    }

    public GameObject GetEnemy()
    {
        if (enemySpawner.availableEnemies.Count > 0)
        {
            return enemySpawner.availableEnemies[0];
        }
        return null;
    }

    public bool AllSpawnedArePositioned()
    {
        return spawnedPrefabs.Any(p => !p.isPositioned) is false;
    }
}
