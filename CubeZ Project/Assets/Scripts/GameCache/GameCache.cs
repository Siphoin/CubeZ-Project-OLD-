using System;

[System.Serializable]
public class GameCache
{
    public InventoryContainerPlayer inventory = new InventoryContainerPlayer();
    public int day = 1;

    public int zombieKils = 0;

    public  DateTime timeSession = new DateTime();
    public GameCache()
    {

    }

    public GameCache (GameCache copyClass)
    {
        copyClass.CopyAll(this);
    }
}