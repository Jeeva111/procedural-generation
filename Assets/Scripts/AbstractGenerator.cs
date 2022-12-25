using UnityEngine;

public abstract class AbstractGenerator : MonoBehaviour {
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void generate() {
        tilemapVisualizer.clear();
        runProceduralGeneration();
    }

    protected abstract void runProceduralGeneration();
}
