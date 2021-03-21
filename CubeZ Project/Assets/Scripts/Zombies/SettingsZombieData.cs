using UnityEngine;

[System.Serializable]
public class SettingsZombieData
{
    [Header("Максимальная дальность видимости зомби")]
    public float maxDistanceVisible = 10;
    [Header("Максимальная скорость зомби")]
    public float maxSpeed = 3;
    [Header("Максимальное значение здоровья зомби")]
    public int maxHealth = 100;
    [Header("Максимальный урон зомби")]
    public int maxDamage = 20;
    [Header("Максимальное число зомби в ново-созданной орде")]
    public int maxZombiesCountInHorde = 4;

    [Header("Минимальное время спавна зомби")]
    public float minTimeSpawnZombie = 10f;
    [Header("Максимальное время спавна зомби")]
    public float maxTimeSpawnZombie = 15f;
    [Header("Коэфицент численности зомби с каждым днем")]
    public int zombieIncrementEveryDay = 2;

    [Header("Время исчезновение трупа зомби")]
    public float timeRemove = 14;

    [Header("Время изчезновения зомби если он не виден основной камерой")]
    public float timeRemoveonGCZombie = 180.0f;


    [Header("Максиальное кол-во зомби в мире")]
    public int countZombiesWorld = 100;
    public SettingsZombieData()
    {

    }

    public SettingsZombieData(SettingsZombieData copyClass)
    {
        copyClass.CopyAll(this);
    }
}