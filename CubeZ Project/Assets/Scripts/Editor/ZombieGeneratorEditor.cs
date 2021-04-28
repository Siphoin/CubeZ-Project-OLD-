using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(ZombieGenerator))]
    public class ZombieGeneratorEditor : Editor, ICustomGUIEditor
    {
    private ZombieGenerator manager;

    SerializedProperty zombieVariantsProperty;
    SerializedProperty countZombiesProperty;
    SerializedProperty countplanesProperty;

    SerializedProperty containerZombiesProperty;
    public override void OnInspectorGUI()
    {
        //   base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Zombie Generator v1.0");

        EditorGUILayout.EndVertical();

        EditorGUILayout.PropertyField(countZombiesProperty);
        GUI.enabled = false;
        EditorGUILayout.PropertyField(countplanesProperty);
        EditorGUILayout.PropertyField(zombieVariantsProperty);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(containerZombiesProperty);

        if (GUILayout.Button("Load planes", GUILayout.Height(50)))
        {
            manager.LoadPlanes();
        }


        if (GUILayout.Button("Load zombies variants", GUILayout.Height(50)))
        {
            manager.LoadZombieVariants();
        }

        GUI.enabled = manager.countPlanes > 0 && manager.zombiesVariants.Length > 0 && !manager.zombiesVariants.Any(z => z == null);

        if (GUILayout.Button("Create zombies in map", GUILayout.Height(50)))
        {
            manager.GenerateZombies();

            if (manager.countZombies > 0)
            {
                SetObjectDirty(manager.gameObject);
            }
        }
        if (GUI.changed)
        {
            SetObjectDirty(manager.gameObject);
        }
        serializedObject.ApplyModifiedProperties();
    }

    public void SetObjectDirty(GameObject go)
    {
        EditorUtility.SetDirty(go);
        EditorSceneManager.MarkSceneDirty(go.scene);
    }

    private void OnEnable()
    {
        manager = (ZombieGenerator)target;

        zombieVariantsProperty = serializedObject.FindProperty("zombiesVariants");
       countZombiesProperty = serializedObject.FindProperty("countZombies");
       countplanesProperty = serializedObject.FindProperty("countPlanes");
        containerZombiesProperty = serializedObject.FindProperty("containerZombies");

    }
}