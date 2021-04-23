using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CollectorMaterialCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
    {
        public IEnumerable<CacheObjectData> GetCollection()
        {
        MaterialDataMono[] materials = FindObjectsOfType<MaterialDataMono>();

        List<MaterialCacheData> dataCollection = new List<MaterialCacheData>();

        for (int i = 0; i < materials.Length; i++)
        {
            dataCollection.Add(materials[i].GetData());
        }

        return dataCollection;
        }

        // Use this for initialization
        void Start()
        {
        Ini();
        }

    }