using UnityEngine;
using UnityEditor;

public class RandomUniformScaleEditor4 : EditorWindow
{
    [MenuItem("Tools/Randomize Uniform Scale 4")]
    static void RandomizeUniformScale4()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Randomize Uniform Scale 4");
            float scale = Random.Range(3f, 6f);
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
