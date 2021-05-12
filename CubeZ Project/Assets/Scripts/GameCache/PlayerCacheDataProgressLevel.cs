[System.Serializable]
    public class PlayerCacheDataProgressLevel
    {

    public int currentLevel = 1;
    public long currentXP = 0;
    public long currentXPForNextLevel = 100;


public PlayerCacheDataProgressLevel ()
    {

    }

    public PlayerCacheDataProgressLevel (PlayerCacheDataProgressLevel copyClass)
    {
        copyClass.CopyAll(this);
    }
    }