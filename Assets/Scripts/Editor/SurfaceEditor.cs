using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Surface))]
public class SurfaceEditor : Editor {
    Surface surface;
    SerializedProperty placeholder;
    SerializedProperty tilePrefab;
    SerializedProperty placeholderSize;
    SerializedProperty tileSize;
    SerializedProperty maxTileHeight;
    SerializedProperty drawMode;
    SerializedProperty smoothness;
    SerializedProperty maxSteps;

    void OnEnable() {
        surface = (Surface)target;
        placeholder = serializedObject.FindProperty("placeholder");
        tilePrefab = serializedObject.FindProperty("tilePrefab");
        placeholderSize = serializedObject.FindProperty("placeholderSize");
        tileSize = serializedObject.FindProperty("tileSize");
        maxTileHeight = serializedObject.FindProperty("maxTileHeight");
        drawMode = serializedObject.FindProperty("drawMode");
        smoothness = serializedObject.FindProperty("smoothness");
        maxSteps = serializedObject.FindProperty("maxSteps");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.ObjectField(placeholder, typeof(Transform));
        EditorGUILayout.ObjectField(tilePrefab, typeof(GameObject));
        EditorGUILayout.PropertyField(placeholderSize);
        EditorGUILayout.IntSlider(tileSize, 0, 20);
        EditorGUILayout.IntSlider(maxTileHeight, 0, 10);
        EditorGUILayout.PropertyField(drawMode);
        EditorGUILayout.Slider(smoothness, 0, 10);

        if (surface.drawMode == Surface.DrawMode.Steps) {
            EditorGUILayout.IntSlider(maxSteps, 0, 10);
        }

        if (GUILayout.Button("Regenerate")) {
            surface.ResizeTiles();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
