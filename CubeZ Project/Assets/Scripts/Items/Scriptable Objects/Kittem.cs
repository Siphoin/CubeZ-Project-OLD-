using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Kit", order = 1)]
public class Kittem : ScriptableObject
    {
    [Header("Данные об аптечке")]
    public KitItemData data = new KitItemData();

    private void Awake()
    {
        data.typeItem = TypeItem.Kit;
    }
}