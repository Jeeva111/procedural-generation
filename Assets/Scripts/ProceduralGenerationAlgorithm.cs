using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithm
{
    public static HashSet<Vector2Int> simpleRandomWalk(Vector2Int startPos, int walkLength) {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPos);
        var prevPos = startPos;
        for (int i = 0; i < walkLength; i++)
        {
            var newPos = prevPos + Direction2D.getRandomCardinalDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int> randomWalkCorridor(Vector2Int startPos, int corridorLength) {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.getRandomCardinalDirection();
        var currPos = startPos;
        corridor.Add(currPos);
        for (int i = 0; i < corridorLength; i++)
        {
            currPos += direction;
            corridor.Add(currPos);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartition(BoundsInt spaceToSplit, int minWidth, int minHeight) {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0) {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)  {
                if(Random.value < 0.5f) {
                    if(room.size.y >= minHeight * 2) {
                        splitHorizontally(minWidth, roomsQueue, room);
                    } else if(room.size.x >= minWidth * 2) {
                        splitVertically(minHeight, roomsQueue, room);
                    } else {
                        roomsList.Add(room);
                    }
                } else {
                    if(room.size.x >= minWidth * 2) {
                        splitVertically(minHeight, roomsQueue, room);
                    } else if(room.size.y >= minHeight * 2) {
                        splitHorizontally(minWidth, roomsQueue, room);
                    } else {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void splitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void splitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D {
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int> {
        new Vector2Int(0,1),    // up
        new Vector2Int(1,0),    // right
        new Vector2Int(0, -1),  // down
        new Vector2Int(-1, 0)   // up
    };

    public static List<Vector2Int> diagonalDirectionList = new List<Vector2Int> {
        new Vector2Int(1, 1),   // up-right
        new Vector2Int(1, -1),  // right-down
        new Vector2Int(-1, -1), // down-left
        new Vector2Int(-1, 1)   // left-up
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int> {
        new Vector2Int(0,1),    // up
        new Vector2Int(1, 1),   // up-right
        new Vector2Int(1,0),    // right
        new Vector2Int(1, -1),  // right-down
        new Vector2Int(0, -1),  // down
        new Vector2Int(-1, -1), // down-left
        new Vector2Int(-1, 0),  // up
        new Vector2Int(-1, 1)   // left-up
    };

    public static Vector2Int getRandomCardinalDirection() =>
        cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
}
