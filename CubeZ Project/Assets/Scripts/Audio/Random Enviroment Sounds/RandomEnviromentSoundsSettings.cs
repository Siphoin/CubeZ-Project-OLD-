using UnityEngine;
[CreateAssetMenu(menuName = "Random Sounds Enviroment/Random Sounds Enviroment Settings", order = 0)]
public class RandomEnviromentSoundsSettings : ScriptableObject
    {
    [Header("Радиус звука от игрока")]
    [SerializeField] private float radiusOfThePlayerSound = 30f;
    [Header("Манимальная задержка появления звука")]
    [SerializeField] private float minTimeOutSpawnSound = 10;
    [Header("Манимальная задержка появления звука")]
    [SerializeField] private float maxTimeOutSpawnSound = 15;

    public float RadiusOfThePlayerSound { get => radiusOfThePlayerSound; }
    public float MinTimeOutSpawnSound { get => minTimeOutSpawnSound; }
    public float MaxTimeOutSpawnSound { get => maxTimeOutSpawnSound; }
}