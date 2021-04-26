using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Hellcopter/HellCopter Spawner Settings", order = 0)]
public class HellCopterSpawnerSettings : ScriptableObject
    {
    [Header("Минимальная задержка появления вертолета")]
    [SerializeField] private float minTimeOutSpawn = 5;

    [Header("Максимальная задержка появления вертолета")]
    [SerializeField] private float maxTimeOutSpawn = 10;

    [Header("Радиус от точки где находится случайно выбранный игрок")]
    [SerializeField] private float radiusOfThePlayer = 15;

    public float MinTimeOutSpawn { get => minTimeOutSpawn; }
    public float MaxTimeOutSpawn { get => maxTimeOutSpawn; }
    public float RadiusOfThePlayer { get => radiusOfThePlayer; }
}