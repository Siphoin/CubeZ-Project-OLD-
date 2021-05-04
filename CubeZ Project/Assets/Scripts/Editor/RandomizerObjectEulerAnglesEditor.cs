using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
[CustomEditor(typeof(RandomizerObjectEulerAngles))]
public class RandomizerObjectEulerAnglesEditor : Editor, ICustomGUIEditor
{
    private const int COUNT_VERTICAL_DRAW = 3;


    private RandomizerObjectEulerAngles manager;

    private void OnEnable()
    {
        manager = (RandomizerObjectEulerAngles)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUI.enabled = manager.objects.Length > 0;

        if (manager.objects.Length > 0)
        {
            GUI.enabled = !manager.objects.Any(item => item == null);
        }
        for (int i = 0; i < COUNT_VERTICAL_DRAW; i++)
        {
            EditorGUILayout.BeginVertical("box");
        }
        if (GUILayout.Button("Randomize", GUILayout.Width(500), GUILayout.Height(50)))
        {
            for (int i = 0; i < manager.objects.Length; i++)
            {
                manager.objects[i].transform.rotation = GenerateAngle(manager.objects[i]);
            }
        }
        for (int i = 0; i < COUNT_VERTICAL_DRAW; i++)
        {
            EditorGUILayout.EndVertical();
        }

        if (GUI.changed)
        {
            SetObjectDirty(manager.gameObject);
        }
    }

    private Quaternion GenerateAngle (GameObject go)
    {
        float y = Random.Range(0, manager.maxAngle + 1);
        float x = go.transform.rotation.eulerAngles.x;
        float z = go.transform.rotation.eulerAngles.z;
        return Quaternion.Euler(new Vector3(x, y, z));
    }

    public  void SetObjectDirty (GameObject go)
    {
        EditorUtility.SetDirty(go);
        EditorSceneManager.MarkSceneDirty(go.scene);
    }

}