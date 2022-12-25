using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;

    public void paintFloorTiles(IEnumerable<Vector2Int> floorPostions) {
        paintTiles(floorPostions, floorTilemap, floorTile);
    }

    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions) {
            paintSingleTile(tilemap, tile, position);
        }
    }

    private void paintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePos = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePos, tile);
    }

    public void clear() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void paintSingleBasicWall(Vector2Int position)
    {
        paintSingleTile(wallTilemap, wallTop, position);
    }
}
