using UnityEngine;
[System.Serializable]
public class ZombieStats
{
    [Header("Здоровье")]
    public int health = 25;
    [Header("Урон")]
    public int damage = 5;
    [Header("Скорость")]
    public float speed = 1;
    [Header("Дальность видимости")]
    public float distanceVisible = 1;

    public ZombieStats()
    {

    }


    public ZombieStats(ZombieStats copyClass)
    {
        copyClass.CopyAll(this);
    }

    public override string ToString()
    {
        string result = "Stats zombie:\n";

        foreach (var prop in this.GetType().GetFields())
        {
            string str = "";
            str += string.Format("{0}: {1}", prop.Name, prop.GetValue(this)) + "\n";
            str = char.ToUpper(str[0]) + str.Substring(1);
            result += str;
        }
        return result;
    }


}