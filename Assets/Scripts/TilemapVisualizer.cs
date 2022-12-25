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
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, 
    wallInnerCornerDownLeft, wallInnerCornerDownRight, 
    wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

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

    internal void paintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallTypesHelper.wallTop.Contains(typeAsInt)) {
            tile = wallTop;
        } else if(WallTypesHelper.wallSideRight.Contains(typeAsInt)) {
            tile = wallSideRight;
        } else if(WallTypesHelper.wallSideLeft.Contains(typeAsInt)) {
            tile = wallSideLeft;
        } else if(WallTypesHelper.wallBottom.Contains(typeAsInt)) {
            tile = wallBottom;
        } else if(WallTypesHelper.wallFull.Contains(typeAsInt)) {
            tile = wallFull;
        }
        if(tile != null) paintSingleTile(wallTilemap, tile, position);
    }

    internal void paintSingleCornerWall(Vector2Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) {
            tile = wallInnerCornerDownLeft;
        } else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)) {
            tile = wallInnerCornerDownRight;
        } else if(WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) {
            tile = wallDiagonalCornerDownLeft;
        } else if(WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) {
            tile = wallDiagonalCornerDownRight;
        } else if(WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) {
            tile = wallDiagonalCornerUpRight;
        } else if(WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) {
            tile = wallDiagonalCornerUpLeft;
        } else if(WallTypesHelper.wallFullEightDirections.Contains(typeAsInt)) {
            tile = wallFull;
        } else if(WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt)) {
            tile = wallBottom;
        }
        if(tile != null) paintSingleTile(wallTilemap, tile, pos);
    }
}
