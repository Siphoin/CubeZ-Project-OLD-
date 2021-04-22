[System.Serializable]
public class WallCacheData : CacheObjectData
    {

        public HealthData health;
        public WallCacheData()
        {

        }

        public WallCacheData(string idWall, HealthData healthData)
        {
            idObject = idWall;
            health = healthData;

        }
    }