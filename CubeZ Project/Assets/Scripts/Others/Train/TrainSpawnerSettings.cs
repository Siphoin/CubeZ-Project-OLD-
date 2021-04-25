using UnityEngine;
[CreateAssetMenu(menuName = "Train/Train Spawner Settings", order = 0)]
public class TrainSpawnerSettings : ScriptableObject
    {
    [Header("Минимальная задержка появления поезда")]
    [SerializeField] private float minTimeOutSpawn = 10;

    [Header("Максимальная задержка появления поезда")]
    [SerializeField] private float maxTimeOutSpawn = 10;

    public float MinTimeOutSpawn { get => minTimeOutSpawn; }
    public float MaxTimeOutSpawn { get => maxTimeOutSpawn; }
}