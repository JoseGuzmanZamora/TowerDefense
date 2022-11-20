using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PositionDefense : MonoBehaviour
{
    public Tilemap cityTileMap;
    public bool isPositioning = true;
    public List<TileBase> allTiles;
    public List<Vector3> tilesPositions;
    public Vector3 referenceSpriteSize = Vector3.zero;
    private GameObject positionGreen;
    private GameObject positionRed;
    private SpriteRenderer greenRenderer;
    private SpriteRenderer redRenderer;
    public Vector2 baseSize = new Vector2(2,2);
    public bool isPositioned = false;

    void Start()
    {
        // Get all tiles from map
        var mapBounds = cityTileMap.cellBounds;
        allTiles = cityTileMap.GetTilesBlock(mapBounds).ToList();

        // Identify specific kinds of tiles, in this case street tiles
        for (int x = mapBounds.xMin; x < mapBounds.xMax; x++) {
            for (int y = mapBounds.yMin; y < mapBounds.yMax; y++) {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)cityTileMap.transform.position.y));
                Vector3 place = cityTileMap.CellToWorld(localPlace);
                var specificTile = cityTileMap.GetTile(localPlace);
                if (specificTile != null) {
                    tilesPositions.Add(place);
                }
            }
        }

        // Define sprite size
        var childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name == "PositionGreen")
            {
                // We found the positioning square
                var spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();
                referenceSpriteSize = spriteRenderer.bounds.size;
                positionGreen = child.gameObject;
                greenRenderer = positionGreen.gameObject.GetComponent<SpriteRenderer>();
            }
            else if (child.name == "PositionRed")
            {
                positionRed = child.gameObject;
                redRenderer = positionRed.gameObject.GetComponent<SpriteRenderer>();
            }
        }
    }

    void Update()
    {
        var isValidPosition = false;
        if (isPositioning)
        {
            // get mouse position
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var lowerLeftCorner = GetLowerLeftCorner(mousePosition);
            
            // Now find closest tile to match the area
            var possibleClosest = FindClosestTile(lowerLeftCorner);
            transform.position = new Vector3(possibleClosest.x + (referenceSpriteSize.x / 2), possibleClosest.y + (referenceSpriteSize.y / 2), transform.position.z);

            // Before setting it, review if position is valid
            isValidPosition = ValidatePosition(lowerLeftCorner);
            greenRenderer.enabled = isValidPosition;
            redRenderer.enabled = !isValidPosition;
        }

        if (Input.GetMouseButton(0) && isValidPosition)
        {
            isPositioning = false;
            isPositioned = true;
        }
    }

    public Vector3 GetLowerLeftCorner(Vector3 referencePoint)
    {
        return new Vector3(referencePoint.x - (referenceSpriteSize.x / 2), referencePoint.y - (referenceSpriteSize.y / 2), 0);
    }

    public Vector3 FindClosestTile(Vector3 referencePoint)
    {
        Vector3 minimumDifference = Vector3.positiveInfinity;
        Vector3 closestPosition = Vector3.zero;
        foreach (var tile in tilesPositions)
        {
            var tempDifference = tile - referencePoint;
            tempDifference = new Vector3(Mathf.Abs(tempDifference.x), Mathf.Abs(tempDifference.y), tempDifference.z);
            if (tempDifference.x <= minimumDifference.x && tempDifference.y <= minimumDifference.y)
            {
                minimumDifference = tempDifference;
                closestPosition = tile;
            } 
        }
        return closestPosition;
    }

    public bool ValidatePosition(Vector3 referencePoint)
    {
        var closestTile = FindClosestTile(referencePoint);
        var xCount = baseSize.x;
        var yCount = baseSize.y;
        var valid = true;

        for (int i = 0; i < yCount; i++)
        {
            for (int y = 0; y < xCount; y++)
            {
                var possibleTilePosition = new Vector3Int((int) closestTile.x + y, (int) closestTile.y + i, 0);
                var possibleTile = cityTileMap.GetTile(possibleTilePosition);
                if (possibleTile != null)
                {
                    // there is a tile, check
                    if (possibleTile.name == "StreetRuleTile")
                    {
                        return false;
                    }
                    else
                    {
                        valid = true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return valid;
    }
}
