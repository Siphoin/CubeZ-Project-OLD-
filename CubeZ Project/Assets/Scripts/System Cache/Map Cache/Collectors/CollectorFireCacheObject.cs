using System.Collections.Generic;

public class CollectorFireCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
{
    // Use this for initialization
    void Start()
    {
        Ini();
    }
    public IEnumerable<CacheObjectData> GetCollection()
    {
        FireDataMono[] fires = FindObjectsOfType<FireDataMono>();

        List<FireCacheData> dataCollection = new List<FireCacheData>();


        for (int i = 0; i < fires.Length; i++)
        {
            dataCollection.Add(fires[i].GetData());
        }
        return dataCollection;
    }


    }