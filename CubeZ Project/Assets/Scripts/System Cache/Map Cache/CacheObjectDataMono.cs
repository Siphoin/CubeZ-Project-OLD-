using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CacheObject))]
    public class CacheObjectDataMono : MonoBehaviour
    {
    protected CacheObject cacheObjectParent;
    [Header("ID родительского объекта")]
   [SerializeField, ReadOnlyField] private string id;

    public string Id { get => id; set => id = value; }

    // Use this for initialization
    void Start()
    {
        Ini();
    }

    public virtual void Ini()
    {
        if (!TryGetComponent(out cacheObjectParent))
        {
            throw new CacheObjectException($"not found component Cache Object ({name}");
        }

        cacheObjectParent.onChangeId += NewId;

        id = cacheObjectParent.Id;
    }

    private void NewId(string id)
    {
        this.id = id;
    }
}