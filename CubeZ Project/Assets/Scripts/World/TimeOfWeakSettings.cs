using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Time of Weak/Time Of Weak Settings", order = 0)]
public class TimeOfWeakSettings : ScriptableObject
    {

    [SerializeField] TimeOfWeakData data = new TimeOfWeakData();

    public TimeOfWeakData GetData()
    {
        return data;
    }

    public TimeOfWeakSettings()
    {

    }
}