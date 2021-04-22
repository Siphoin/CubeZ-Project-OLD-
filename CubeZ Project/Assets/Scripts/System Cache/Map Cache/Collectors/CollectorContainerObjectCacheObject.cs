using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CollectorContainerObjectCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
    {
        public IEnumerable<CacheObjectData> GetCollection()
        {
        ContainerObjectDataMono[] objects = FindObjectsOfType<ContainerObjectDataMono>();

        List<ContainerObjectCacheData> dataCollection = new List<ContainerObjectCacheData>();

        for (int i = 0; i < objects.Length; i++)
        {
            ContainerObjectDataMono obj = objects[i];
            ContainerObjectCacheData data = obj.GetData();
            dataCollection.Add(data);
        }

        return dataCollection;
    }

        // Use this for initialization
        void Start()
        {
        Ini();
        }

    }