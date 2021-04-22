using System;
using System.Collections.Generic;

[System.Serializable]
public class GameCache
{
    public int day = 1;

    public int zombieKils = 0;

    public  DateTime timeSession = new DateTime();

    public string mapName = null;

    public string versionClient = null;

    public WorldCacheData worldData = new WorldCacheData();

    public PlayerStatsCache playerStats = new PlayerStatsCache();

    public InventoryContainerPlayer inventory = new InventoryContainerPlayer();


    #region Caching Objects
    public ContainerCacheObjects containerCacheObjects = new ContainerCacheObjects();

    public ContainerDataObjects dataContainerObjects = new ContainerDataObjects();
    #endregion
    public GameCache()
    {

    }

    public GameCache (GameCache copyClass)
    {
        copyClass.CopyAll(this);
    }
}