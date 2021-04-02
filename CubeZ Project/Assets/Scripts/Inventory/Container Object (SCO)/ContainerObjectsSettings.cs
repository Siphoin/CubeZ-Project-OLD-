using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Container Objecct/New Container Object Settings", order = 0)]
public class ContainerObjectsSettings : ScriptableObject
    {
    [Header("Максимальное количество предметов в контейнере объекта")]
    [SerializeField] private int maxCountItemsOfContainer = 12;

    [Header("Вероятность генерации новых предметов в контейнере каждый день (в %)")]
    [SerializeField] private int probabilitynewGenerationValue = 50;

    public int MaxCountItemsOfContainer { get => maxCountItemsOfContainer; }
    public int ProbabilitynewGenerationValue { get => probabilitynewGenerationValue; }
}