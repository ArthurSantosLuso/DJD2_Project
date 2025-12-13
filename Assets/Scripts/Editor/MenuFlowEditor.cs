using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(MenuFlow))]
public class MenuFlowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MenuFlow script = (MenuFlow)target;

        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        string[] scenesNames = new string[scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
            scenesNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);

        int index = Mathf.Max(0, System.Array.IndexOf(scenesNames, script.name));
        index = EditorGUILayout.Popup("Scene", index, scenesNames);

        serializedObject.FindProperty("sceneName").stringValue = scenesNames[index];

        serializedObject.ApplyModifiedProperties();
    }
}
