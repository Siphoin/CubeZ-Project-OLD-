using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/New zombie stats", order = 1)]
public class ZombieStatsSettings : ScriptableObject
{
    [SerializeField] ZombieStats data = new ZombieStats();

    public ZombieStats GetData()
    {
        return data;
    }

    public ZombieStatsSettings()
    {

    }
}