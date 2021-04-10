using UnityEngine;

    [CreateAssetMenu(menuName = "Props Objects/Door/Door Settings", order = 0)]
    public class DoorSettings : ScriptableObject
    {
    [Header("Стартовое значение дверей")]
    [SerializeField] private int startHealth = 60;

    [Header("Шанс того, что дверь будет заперта")]
    [SerializeField, Range(0, 100)] private int probabilityDoorBlocked = 70;

    public int StartHealth { get => startHealth; }
    public int ProbabilityDoorBlocked { get => probabilityDoorBlocked; }
}