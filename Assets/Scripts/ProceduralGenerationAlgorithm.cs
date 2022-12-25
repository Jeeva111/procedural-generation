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
}

public static class Direction2D {
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int> {
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    public static Vector2Int getRandomCardinalDirection() =>
        cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
}
