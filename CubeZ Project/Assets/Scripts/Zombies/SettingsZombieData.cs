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
    [Header("Коэфицент численности зомби с каждым днем")]
    public int zombieIncrementEveryDay = 2;

    [Header("Время исчезновение трупа зомби")]
    public float timeRemove = 7;

    public SettingsZombieData()
    {

    }

    public SettingsZombieData(SettingsZombieData copyClass)
    {
        this.CopyAll(copyClass);
    }
}