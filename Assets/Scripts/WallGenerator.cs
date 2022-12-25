using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void createWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer) {
        var basicWallPos = findWallsInDirection(floorPos, Direction2D.cardinalDirectionList);
        foreach(var position in basicWallPos) {
            tilemapVisualizer.paintSingleBasicWall(position);
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
