using UnityEngine;
[CreateAssetMenu(menuName = "Car/Alarm System Settings", order = 0)]
public class CarAlarmSystemSettings : ScriptableObject
    {
    [Header("Вероятность сигнализации машины")]
    [Range(0, 100)]
    [SerializeField]
    private int probalityAlarmOn = 0;

    public int ProbalityAlarmOn { get => probalityAlarmOn; }
}