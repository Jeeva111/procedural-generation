using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalk : AbstractGenerator
{

    [SerializeField]
    protected SimpleRandomWalkData randomWalkParams;

    protected override void runProceduralGeneration() {
        HashSet<Vector2Int> floorPos = runRandomWalk(randomWalkParams, startPos);
        tilemapVisualizer.clear();
        tilemapVisualizer.paintFloorTiles(floorPos);
        WallGenerator.createWalls(floorPos, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> runRandomWalk(SimpleRandomWalkData parameters, Vector2Int pos)
    {
        var currPos = pos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithm.simpleRandomWalk(currPos, parameters.walkLength);
            floorPos.UnionWith(path);
            if(parameters.startRandomEachIteration) {
                currPos = floorPos.ElementAt(UnityEngine.Random.Range(0, floorPos.Count));
            }
        }
        return floorPos;
    }
}
