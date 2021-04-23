using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
    public class MaterialDataMono : CacheObjectDataMono
    {
    private Renderer renderer;
        // Use this for initialization
        void Start()
        {
        if (!TryGetComponent(out renderer))
        {
            throw new CacheObjectException($"({name}) not have component Renderer");
        }

        Ini();


        }

    public MaterialCacheData GetData ()
    {
        return new MaterialCacheData(Id, renderer.material);
    }

    }