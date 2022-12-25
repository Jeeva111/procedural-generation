using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstGenerator : SimpleRandomWalk {
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void runProceduralGeneration()
    {
        createRooms();
    }

    private void createRooms()
    {
        var roomsList = ProceduralGenerationAlgorithm.BinarySpacePartition(
            new BoundsInt((Vector3Int) startPos, 
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), 
            minRoomWidth, 
            minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        if(randomWalkRooms) {
            floor = createRandomRooms(roomsList);
        } else {
            floor = createSimpleRooms(roomsList);
        }
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList) {
            roomCenters.Add((Vector2Int) Vector3Int.RoundToInt(room.center));
        }
        HashSet<Vector2Int> corridors = connectRooms(roomCenters);
        floor.UnionWith(corridors);
        tilemapVisualizer.paintFloorTiles(floor);
        WallGenerator.createWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> createRandomRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = runRandomWalk(randomWalkParams, roomCenter);
            foreach(var pos in roomFloor) {
                if(pos.x >= (roomBounds.xMin + offset) && 
                    pos.x <= (roomBounds.xMax - offset) && 
                    pos.y >= (roomBounds.yMin - offset) && 
                    pos.y <= (roomBounds.yMax - offset)) {
                        floor.Add(pos);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> connectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currRoomCenter);
        while(roomCenters.Count > 0) {
            Vector2Int closest = findClosestPointTo(currRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = createCorridor(currRoomCenter, closest);
            currRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> createCorridor(Vector2Int currRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var pos = currRoomCenter;
        corridor.Add(pos);
        while (pos.y != destination.y)
        {
            if(destination.y > pos.y) {
                pos += Vector2Int.up;
            } else if(destination.y < pos.y) {
                pos += Vector2Int.down;
            }
            corridor.Add(pos);
        }
        
        while (pos.x != destination.x)
        {
            if(destination.x > pos.x) {
                pos += Vector2Int.right;
            } else if(destination.x < pos.x) {
                pos += Vector2Int.left;
            }
            corridor.Add(pos);
        }
        return corridor;
    }

    private Vector2Int findClosestPointTo(Vector2Int currRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var pos in roomCenters)
        {
            float currDistance = Vector2.Distance(pos, currRoomCenter);
            if(currDistance < distance) {
                distance = currDistance;
                closest = pos;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> createSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = 0; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int) room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }
}
