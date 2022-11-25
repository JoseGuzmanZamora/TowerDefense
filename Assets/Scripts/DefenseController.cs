using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DefenseController : MonoBehaviour
{
    // TODO: this will be a list of the possible prefabs
    public PositionDefense defensePrefab;
    public List<PositionDefense> spawnedPrefabs;
    public Tilemap cityTilemap;
    public EnemySpawner enemySpawner;
    public CityInventoryManager inventory;
    public bool isPositioning = false;
    public Image buttonSelectionImage;

    void Update()
    {
        var canPosition = HasInventoryToInstantiate();
        if (Input.GetKeyDown(KeyCode.Space) && AllSpawnedArePositioned() && canPosition)
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            inventory.spawnedInventory[Guid.NewGuid().ToString()] = spawnedDefense;
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

    // Check the dictionary for defenses that have not been positioned yet
    public bool HasInventoryToInstantiate()
    {
        return inventory.totalInventory > inventory.spawnedInventory.Count;
    }

    public void SelectedDefenseToPosition()
    {
        isPositioning = !isPositioning;

        var canPosition = HasInventoryToInstantiate();
        if (isPositioning && AllSpawnedArePositioned() && canPosition)
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            inventory.spawnedInventory[Guid.NewGuid().ToString()] = spawnedDefense;
            spawnedDefense.cityTileMap = cityTilemap;
            spawnedDefense.transform.parent = transform;
            spawnedPrefabs.Add(spawnedDefense);
        }

        var tempColor = buttonSelectionImage.color;
        if (isPositioning)
        {
            buttonSelectionImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, 255);
        }
        else
        {
            buttonSelectionImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
        }
    }
}
