using System.Collections;
using UnityEngine;

    public class DoorObjectDataMono : CacheObjectDataMono
    {
    [Header("Дверь")]
    [SerializeField] private Door door;
        // Use this for initialization
        void Start()
        {
        if (door == null)
        {
            throw new CacheObjectException("door not seted");
        }
        Ini();
        }


    public DoorCacheData GetData ()
    {
        return new DoorCacheData(Id, door.DoorData);
    }
    }