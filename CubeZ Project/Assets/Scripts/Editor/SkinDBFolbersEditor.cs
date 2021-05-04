using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;


[CustomEditor(typeof(SkinDBFolbers))]
    public class SkinDBFolbersEditor : Editor, ICustomGUIEditor
    {
    private SkinDBFolbers manager;

    SerializedProperty folbersProperty;
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();


        EditorGUILayout.LabelField("Skin DB Folbers v1.0");

        GUI.enabled = false;

        EditorGUILayout.PropertyField(folbersProperty);

        GUI.enabled = true;

        if (GUILayout.Button("Refresh List", GUILayout.Height(25)))
        {
            string[] folbers = Directory.GetDirectories(Application.dataPath + "/Resources/Textures/");
            for (int i = 0; i < folbers.Length; i++)
            {
                folbers[i] = folbers[i].Replace(Application.dataPath + "/Resources/Textures/", string.Empty);
            }
            manager.SetListFolbers(folbers);
        }

        if (GUILayout.Button("Clear", GUILayout.Height(25)))
        {         
            manager.SetListFolbers(new string[0]);
            
        }




        EditorGUILayout.EndVertical();
    }

    public void SetObjectDirty(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        manager = (SkinDBFolbers)target;

        folbersProperty = serializedObject.FindProperty("folbersList");

        serializedObject.ApplyModifiedProperties();
    }

}