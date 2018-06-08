using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Surface))]
public class SurfaceEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Regenerate")) {
            ((Surface)target).ResizeTiles();
        }
    }
}
