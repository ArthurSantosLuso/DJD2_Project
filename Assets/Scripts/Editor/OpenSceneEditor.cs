using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(OpenScene))]
public class OpenSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        OpenScene loader = (OpenScene)target;

        
        var scenes = EditorBuildSettings.scenes;
        string[] sceneNames = new string[scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);
        }

        
        loader.sceneIndex = EditorGUILayout.Popup(
            "Scene",
            loader.sceneIndex,
            sceneNames
        );

        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(loader);
        }
    }
}
