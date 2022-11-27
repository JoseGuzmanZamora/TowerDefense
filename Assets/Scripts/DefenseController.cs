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
    public Tilemap cityTilemap;
    public EnemySpawner enemySpawner;
    public CityInventoryManager inventory;
    public bool isPositioning = false;
    public Image buttonSelectionImage;
    public bool canPosition = false;

    void Update()
    {
        if (AllSpawnedArePositioned() && isPositioning && HasInventoryToInstantiate())
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            inventory.spawnedInventory.Add(spawnedDefense);
            spawnedDefense.cityTileMap = cityTilemap;
            spawnedDefense.transform.parent = transform;
        }
        // set the objective to all children
        var childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            var child = transform.GetChild(i);
            var childAttackController = child.gameObject.GetComponent<AttackController>();
            childAttackController.objective = GetEnemy();
        }

        if (HasInventoryToInstantiate() is false && AllSpawnedArePositioned())
        {
            var tempColor = buttonSelectionImage.color;
            buttonSelectionImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
            isPositioning = false;
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
        return inventory.spawnedInventory.Any(p => p.isPositioned is false) is false;
    }

    // Check the dictionary for defenses that have not been positioned yet
    public bool HasInventoryToInstantiate()
    {
        return inventory.totalInventory > inventory.spawnedInventory.Count;
    }

    public void SelectedDefenseToPosition()
    {
        canPosition = HasInventoryToInstantiate();
        var tempColor = buttonSelectionImage.color;
        if (isPositioning is false && AllSpawnedArePositioned() && canPosition)
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            inventory.spawnedInventory.Add(spawnedDefense);
            spawnedDefense.cityTileMap = cityTilemap;
            spawnedDefense.transform.parent = transform;

            isPositioning = true;
            buttonSelectionImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, 255);
        }
        else if (isPositioning is true)
        {
            buttonSelectionImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
            isPositioning = false;

            if (AllSpawnedArePositioned() is false)
            {
                var extraSpawned = inventory.spawnedInventory[inventory.spawnedInventory.Count - 1];
                Destroy(extraSpawned.gameObject);
                inventory.spawnedInventory.Remove(extraSpawned);
            }

            //var lastSpawned = inventory.spawnedInventory[inventory.spawnedInventory.Count - 1];
            //Destroy(lastSpawned.gameObject);
            //inventory.spawnedInventory.Remove(lastSpawned);
        }
    }
}
