using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CollectorZombieCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
    {
    public IEnumerable<CacheObjectData> GetCollection()
    {
        ZombieCacheDataMono[] zombies = FindObjectsOfType<ZombieCacheDataMono>();

        List<ZombieCacheData> dataCollection = new List<ZombieCacheData>();

        for (int i = 0; i < zombies.Length; i++)
        {
            ZombieCacheDataMono zombie = zombies[i];
            ZombieCacheData data = new ZombieCacheData(zombie.Id, zombie.GetStats());
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