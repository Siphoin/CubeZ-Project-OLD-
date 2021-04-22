[System.Serializable]
public class ContainerObjectCacheData : CacheObjectData
{
 public   BaseInventoryContainer items = new BaseInventoryContainer();
      public ContainerObjectCacheData ()
    {

    }

    public ContainerObjectCacheData (string idContainerObject, BaseInventoryContainer conrtainerItems)
    {
        idObject = idContainerObject;
        items = conrtainerItems;
    }
    }