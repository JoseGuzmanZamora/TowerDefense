using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class PathDeterminator : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public List<Vector3> instructions = new List<Vector3>();
    private Tilemap tilemap;
    private TileBase[] allTiles;
    private List<Vector3> streetTiles = new List<Vector3>();
    private Grid tilemapGrid;
    void Start()
    {
        // Get all tiles from map
        tilemap = gameObject.GetComponent<Tilemap>();
        tilemapGrid = tilemap.layoutGrid;
        var mapBounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(mapBounds);

        // Identify specific kinds of tiles, in this case street tiles
        for (int x = mapBounds.xMin; x < mapBounds.xMax; x++) {
            for (int y = mapBounds.yMin; y < mapBounds.yMax; y++) {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                var specificTile = tilemap.GetTile(localPlace);
                if (specificTile != null && specificTile.name == "StreetRuleTile") {
                    streetTiles.Add(place);
                }
            }
        }
        
        // Find closest tile to start
        var closestToStart = FindClosestTile(start.transform);
        var closestToEnd = FindClosestTile(end.transform);

        // Start Constructing Path
        var nextFind = FindLongestPathFromPoint(closestToStart, PathDirections.Initial);
        instructions.Add(nextFind.tilePosition);
        var reachedEnd = ReachedEnd(closestToEnd, nextFind.tilePosition);
        var limitCounter = 0;
        while (reachedEnd is false)
        {
            nextFind = FindLongestPathFromPoint(nextFind.tilePosition, nextFind.previousDirection);
            reachedEnd = ReachedEnd(closestToEnd, nextFind.tilePosition);
            instructions.Add(nextFind.tilePosition);

            limitCounter ++;
            if (limitCounter > 1000) break;
        }
    }

    public bool ReachedEnd(Vector3 endPoint, Vector3 comparisonPoint)
    {
        return Mathf.Abs(endPoint.x - comparisonPoint.x) <= 3 && Math.Abs(endPoint.y - comparisonPoint.y) <= 3;
    }

    public Vector3 FindClosestTile(Transform referencePoint)
    {
        Vector3 minimumDifference = Vector3.positiveInfinity;
        Vector3 closestPosition = Vector3.zero;
        foreach (var tile in streetTiles)
        {
            var tempDifference = tile - referencePoint.position;
            tempDifference = new Vector3(Mathf.Abs(tempDifference.x), Mathf.Abs(tempDifference.y), tempDifference.z);
            if (tempDifference.x <= minimumDifference.x && tempDifference.y <= minimumDifference.y)
            {
                minimumDifference = tempDifference;
                closestPosition = tile;
            } 
        }
        return closestPosition;
    }

    public (int amount, Vector3 tilePosition, PathDirections previousDirection) FindLongestPathFromPoint(Vector3 initialPoint, PathDirections origin)
    {
        var initialTilePlace = tilemap.WorldToCell(initialPoint);

        // Figure out each side
        var possibleDirections = Enum.GetValues(typeof(PathDirections)).Cast<PathDirections>()
            .Where(e => e != PathDirections.Initial && e != origin).ToList();
        
        var maximumDistance = 0;
        (int, Vector3Int, PathDirections) selectedPoint = (0, new Vector3Int(), PathDirections.Initial);
        foreach (var direction in possibleDirections)
        {
            var tempData = SingleDirectionCounterHelper(initialTilePlace, direction);
            if (tempData.Item1 > maximumDistance)
            {
                maximumDistance = tempData.Item1;
                selectedPoint = new (tempData.Item1, tempData.Item2, GetDirectionOpposite(direction));
            }
        }
        return selectedPoint;
    }

    public (int, Vector3Int) SingleDirectionCounterHelper(Vector3Int initialPosition, PathDirections direction)
    {
        var genericCounter = 0;
        var limitCounter = 0;
        var continueSearching = true;
        var possibleObjective = new Vector3Int();
        while (continueSearching)
        {
            if (direction == PathDirections.West) initialPosition.x --;
            if (direction == PathDirections.North) initialPosition.y ++;
            if (direction == PathDirections.East) initialPosition.x ++;
            if (direction == PathDirections.South) initialPosition.y --;
            var tempTile = tilemap.GetTile(initialPosition);
            if (tempTile != null && tempTile.name == "StreetRuleTile")
            {
                genericCounter++;
                possibleObjective = initialPosition;
            }
            else
            {
                continueSearching = false;
                break;
            }
            limitCounter++;
            if (limitCounter > 1000) break;
        }
        
        return (genericCounter, possibleObjective);
    }

    public PathDirections GetDirectionOpposite(PathDirections direction)
    {
        switch (direction)
        {
            case PathDirections.North:
                return PathDirections.South;
            case PathDirections.South:
                return PathDirections.North;
            case PathDirections.East:
                return PathDirections.West;
            case PathDirections.West:
                return PathDirections.East;
            default:
                return PathDirections.Initial;
        }
    }

    public enum PathDirections
    {
        Initial,
        West,
        North,
        East,
        South
    }
}
