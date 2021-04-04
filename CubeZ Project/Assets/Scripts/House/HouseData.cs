[System.Serializable]
    public class HouseData
    {
    public int countZombiesOnHouse = 0;
    public int countPlayersOnHouse = 0;

    public HouseData ()
    {

    }

    public HouseData (HouseData copyClass)
    {
        copyClass.CopyAll(this);
    }
}
