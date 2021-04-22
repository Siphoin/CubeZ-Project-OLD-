using System.Collections.Generic;

public class CollectorDoorCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
    {
    public IEnumerable<CacheObjectData> GetCollection()
    {
        DoorObjectDataMono[] doors = FindObjectsOfType<DoorObjectDataMono>();

        List<DoorCacheData> dataCollection = new List<DoorCacheData>();

        for (int i = 0; i < doors.Length; i++)
        {
            dataCollection.Add(doors[i].GetData());
        }

        return dataCollection;
    }

    // Use this for initialization
    void Start()
        {
        Ini();
    }

    }