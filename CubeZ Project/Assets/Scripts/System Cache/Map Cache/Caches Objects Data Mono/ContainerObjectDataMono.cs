using System.Collections;
using UnityEngine;

    public class ContainerObjectDataMono : CacheObjectDataMono
{
    private ContainerObject containerObject;
    private void Start()
    {
        if (!TryGetComponent(out containerObject))
        {
            throw new CacheObjectException($"({name}) not have component Container Object");
        }

        Ini();
    }

    public ContainerObjectCacheData GetData ()
    {
        return new ContainerObjectCacheData(Id, containerObject.ContainerItems);
    }
}