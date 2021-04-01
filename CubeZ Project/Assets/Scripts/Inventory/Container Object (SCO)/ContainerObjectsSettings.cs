using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Container Objecct/New Container Object Settings", order = 0)]
public class ContainerObjectsSettings : ScriptableObject
    {
    [Header("Максимальное количество предметов в контейнере объекта")]
    [SerializeField] private int maxCountItemsOfContainer = 12;

    public int MaxCountItemsOfContainer { get => maxCountItemsOfContainer; }
}