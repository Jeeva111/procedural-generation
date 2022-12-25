using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstGenerator : SimpleRandomWalk
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void runProceduralGeneration()
    {
        corridorFirstGenerator();
    }

    private void corridorFirstGenerator()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();
        createCorridors(floorPos, potentialRoomPos);
        HashSet<Vector2Int> roomPositions = createRooms(potentialRoomPos);
        List<Vector2Int> deadEnds = findAllDeadEnds(floorPos);
        createRoomsAtDeadEnds(deadEnds, roomPositions);
        floorPos.UnionWith(roomPositions);
        tilemapVisualizer.paintFloorTiles(floorPos);
        WallGenerator.createWalls(floorPos, tilemapVisualizer);
    }

    private void createRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false) {
                var room = runRandomWalk(randomWalkParams, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> findAllDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach(var pos in floorPos) {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if(floorPos.Contains(pos + direction)) neighboursCount++;
            }
            if(neighboursCount == 1) deadEnds.Add(pos);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> createRooms(HashSet<Vector2Int> potentialRoomPos) {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);
        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach (var roomPos in roomToCreate) {
            var roomFloor = runRandomWalk(randomWalkParams, roomPos);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void createCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        Vector2Int currPos = startPos;
        potentialRoomPos.Add(currPos);
        for (int i = 0; i < corridorCount; i++)
        {
            List<Vector2Int> corridor = ProceduralGenerationAlgorithm.randomWalkCorridor(currPos, corridorLength);
            currPos = corridor[corridor.Count - 1];
            potentialRoomPos.Add(currPos);
            floorPos.UnionWith(corridor);
        }
    }
}
