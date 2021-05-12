using UnityEngine;
[CreateAssetMenu(menuName = "Character/Character Settings Level Up", order = 1)]
public class CharacterLevelUpSettings : ScriptableObject
    {
    [Header("Максимальный уровень персонажа")]
    [SerializeField] private long maxLevelPlayer = 10;

    [Header("Увеличение скорости бега с каждым уровнем")]
    [SerializeField] private float buffSpeed = 0.1f;

    [Header("Увеличение урона с каждым уровнем")]
    [SerializeField] private int buffDamage = 1;

    public long MaxLevelPlayer { get => maxLevelPlayer; }
    public float BuffSpeed { get => buffSpeed; }
    public int BuffDamage { get => buffDamage; }
}
