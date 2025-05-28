using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuzzleInput))]
public class BoolGridEditor : Editor
{
    private const int GridSize = 5;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PuzzleInput grid = (PuzzleInput)target;

        if (grid.solution == null || grid.solution.Length != GridSize * GridSize)
        {
            grid.solution = new bool[GridSize * GridSize];
        }

        EditorGUILayout.LabelField("Solution", EditorStyles.boldLabel);

        for (int y = 0; y < GridSize; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < GridSize; x++)
            {
                int i = y * GridSize + x;
                grid.solution[i] = GUILayout.Toggle(grid.solution[i], GUIContent.none, GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(grid);
        }
    }
}
