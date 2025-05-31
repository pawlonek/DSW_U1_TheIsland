using UnityEngine;
using UnityEditor;

public class RandomYRotationEditor : EditorWindow
{
    [MenuItem("Tools/Randomize Y Rotation")]
    static void RandomizeYRotation()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Randomize Y Rotation");
            Vector3 rotation = obj.transform.eulerAngles;
            rotation.y = Random.Range(0f, 360f);
            obj.transform.eulerAngles = rotation;
        }
    }
}
