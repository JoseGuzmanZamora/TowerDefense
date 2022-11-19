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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && AllSpawnedArePositioned())
        {
            var spawnedDefense = Instantiate(defensePrefab, new Vector3(0,0,0), Quaternion.identity);
            spawnedDefense.cityTileMap = cityTilemap;
            spawnedDefense.transform.parent = transform;
            spawnedPrefabs.Add(spawnedDefense);
        }
    }

    public bool AllSpawnedArePositioned()
    {
        return spawnedPrefabs.Any(p => !p.isPositioned) is false;
    }
}
