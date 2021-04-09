
using UnityEngine;
[CreateAssetMenu(menuName = "Wisps/Wisps Settings", order = 0)]
public class WispSpawnerSettings : ScriptableObject
    {
        [Header("Максимальное количество светлячков в мире")]
        [SerializeField] int maxCountWisps = 10;

}