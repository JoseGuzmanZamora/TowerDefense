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
    public CityInventoryManager inventory;

    void Update()
    {
        var canPosition = HasInventoryToInstantiate();
        if (Input.GetKeyDown(KeyCode.Space) && AllSpawnedArePositioned() && canPosition is not null)
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            spawnedDefense.cityTileMap = cityTilemap;
            spawnedDefense.transform.parent = transform;
            spawnedPrefabs.Add(spawnedDefense);
            inventory.defenseInventoryStatus[canPosition] = true;
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

    // Check the dictionary for defenses that have not been positioned yet
    public string HasInventoryToInstantiate()
    {
        var next = inventory.defenseInventoryStatus.Where(s => s.Value is false).FirstOrDefault();
        return next.Key;
    }
}
