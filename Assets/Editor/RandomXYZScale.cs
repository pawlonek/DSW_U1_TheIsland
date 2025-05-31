using UnityEngine;
using UnityEditor;

public class RandomUniformScaleEditor : EditorWindow
{
    [MenuItem("Tools/Randomize Uniform Scale")]
    static void RandomizeUniformScale()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Randomize Uniform Scale");
            float scale = Random.Range(0.9f, 1.1f);
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
