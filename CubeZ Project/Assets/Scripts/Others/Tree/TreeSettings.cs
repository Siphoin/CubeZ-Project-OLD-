using UnityEngine;
[CreateAssetMenu(menuName = "Trees/Tree Settings", order = 0)]
public class TreeSettings : ScriptableObject
    {
    [Header("Настройки деревьев")]
    [SerializeField] private TreeData data;

    public TreeData Data { get => data; }
}