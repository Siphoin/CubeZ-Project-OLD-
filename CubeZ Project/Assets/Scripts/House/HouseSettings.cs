using UnityEngine;


[CreateAssetMenu(menuName = "Props Objects/House/House Settings", order = 0)]


public class HouseSettings : ScriptableObject
    {
    [Header("Вероятность появления зомби в доме (в %)")]
    [SerializeField, Range(0, 100)]
    private int probabilitySpawnZombie = 42;

    [Header("Максимальное кол-во зомби в домах")]
    [SerializeField]
    private int maxCountZombies = 3;

    public int ProbabilitySpawnZombie { get => probabilitySpawnZombie; }
    public int MaxCountZombies { get => maxCountZombies; }
}