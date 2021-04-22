using System.Collections.Generic;

public class CollectorWallCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
    {

        // Use this for initialization
        void Start()
        {
        Ini();
    }

    public IEnumerable<CacheObjectData> GetCollection()
    {
        WallCacheDataMono[] walls = FindObjectsOfType<WallCacheDataMono>();

        List<WallCacheData> dataCollection = new List<WallCacheData>();

        for (int i = 0; i < walls.Length; i++)
        {
            dataCollection.Add(walls[i].GetData());
        }

        return dataCollection;
    }

}