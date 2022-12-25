using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void createWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPos = findWallsInDirection(floorPos, Direction2D.cardinalDirectionList);
        var cornerWallPos = findWallsInDirection(floorPos, Direction2D.diagonalDirectionList);
        createBasicWall(tilemapVisualizer, basicWallPos, floorPos);
        createCornerWall(tilemapVisualizer, cornerWallPos, floorPos);
    }

    private static void createCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in cornerWallPos) {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionList) {
                var neighbourPos = pos + direction;
                if(floorPos.Contains(neighbourPos)) {
                    neighboursBinaryType += "1";
                } else {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.paintSingleCornerWall(pos, neighboursBinaryType);
        }
    }

    private static void createBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var position in basicWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var neighbourPos = position + direction;
                if(floorPos.Contains(neighbourPos)) {
                    neighboursBinaryType += "1";
                } else {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.paintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> findWallsInDirection(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var direction in directionList)
            {
                var neighbourPos = pos + direction;
                if(floorPos.Contains(neighbourPos) == false) {
                    wallPos.Add(neighbourPos);
                }
            }
            
        }
        return wallPos;
    }
}
