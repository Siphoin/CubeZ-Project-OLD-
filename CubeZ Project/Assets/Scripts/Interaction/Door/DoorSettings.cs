using UnityEngine;

    [CreateAssetMenu(menuName = "Props Objects/Door/Door Settings", order = 0)]
    public class DoorSettings : ScriptableObject
    {
    [Header("Стартовое значение дверей")]
    [SerializeField] private int startHealth = 60;

    public int StartHealth { get => startHealth; }
}