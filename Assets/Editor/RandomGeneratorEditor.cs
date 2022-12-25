using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractGenerator), true)]
public class RandomGeneratorEditor : Editor {
    AbstractGenerator generator;
    private void Awake() {
        generator = (AbstractGenerator) target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Geneate")) {
            generator.generate();
        }
    }
}