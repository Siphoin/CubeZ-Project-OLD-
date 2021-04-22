using System.Collections;
using UnityEngine;
[System.Serializable]
    public class DoorCacheData : CacheObjectData
    {
    public DoorData doorData;
public DoorCacheData ()
    {

    }

    public DoorCacheData (string idDoor, DoorData doorData)
    {
        idObject = idDoor;
        this.doorData = doorData;
    }
    }