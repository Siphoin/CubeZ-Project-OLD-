using System.Collections.Generic;
[System.Serializable]
    public class ContainerDataObjects
    {
    public Dictionary<string, IEnumerable<CacheObjectData>> objectsData = new Dictionary<string, IEnumerable<CacheObjectData>>();

    public ContainerDataObjects ()
    {

    }
    }