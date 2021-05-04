using shortid;
using System;
using UnityEngine;

public class CacheObject : MonoBehaviour, IRemoveObject
    {
    private const string PREFIX_CLONE_PREFAB = "(Clone)";


    [Header("Путь до оригинала (префаб)")]
    [SerializeField] private string prefabPath = "Not_Path";
    [Header("ID объекта")]
    [SerializeField, ReadOnlyField] private string id;

    [Header("Является клоном объекта")]
    [SerializeField, ReadOnlyField] private bool isClone;

    public event Action<string> onRemove;

    public event Action<string> onChangeId;

    public string Id { get => id; }
    public bool IsClone { get => isClone; }
    public string PrefabPath { get => prefabPath; }

    // Use this for initialization
    void Awake()
        {
        isClone = name.Contains(PREFIX_CLONE_PREFAB);
        if (string.IsNullOrEmpty(prefabPath))
        {
            throw new CacheObjectException($"prefab path is emtry ({name})");
        }
        if (string.IsNullOrEmpty(id))
        {
            id = ShortId.Generate(true, false, name.Replace(PREFIX_CLONE_PREFAB, string.Empty).Length * 2);
        }

        }


    public void SetId (string id)
    {
        this.id = id;
        onChangeId?.Invoke(this.id);

#if UNITY_EDITOR
        Debug.Log($"new id for object {name}: Id: {this.id}");
#endif
    }

    private void OnDestroy()
    {
            onRemove?.Invoke(id);
        
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}