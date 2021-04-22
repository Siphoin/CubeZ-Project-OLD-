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

    protected void Ini()
    {
        if (!TryGetComponent(out cacheObjectParent))
        {
            throw new CacheObjectException($"not found component Cache Object ({name}");
        }

        id = cacheObjectParent.Id;
    }

    // Update is called once per frame
    void Update()
        {

        }
    }