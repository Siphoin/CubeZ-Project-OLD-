[System.Serializable]
public class ZombieCacheData : CacheObjectData
    {
    public ZombieStats stats;
    public ZombieCacheData ()
    {

    }

    public ZombieCacheData (string idZombie, ZombieStats zombieStats)
    {
        idObject = idZombie;
        stats = zombieStats;

    }
    }