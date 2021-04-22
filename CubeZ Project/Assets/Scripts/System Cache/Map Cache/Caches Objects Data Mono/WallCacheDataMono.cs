
public class WallCacheDataMono : CacheObjectDataMono
{
 private   Wall wall;
    // Use this for initialization
    void Start()
    {
        if (!TryGetComponent(out wall))
        {
            throw new CacheObjectException($"{name} not have component Wall");
        }

        Ini();
    }

    public WallCacheData GetData()
    {
        return new WallCacheData(Id, new HealthData(wall.CurrentHealth));

    }

}