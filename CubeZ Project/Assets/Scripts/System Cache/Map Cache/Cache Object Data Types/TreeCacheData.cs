public class TreeCacheData : CacheObjectData
    {
    public HealthData health;
    public TreeCacheData ()
    {

    }

    public TreeCacheData (string idTree, HealthData healthData)
    {
        idObject = idTree;
        health = healthData;

    }




}