using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Food", order = 0)]
public class FoodItem : ScriptableObject
    {
    [Header("Данные о еде")]
    [SerializeField] FoodItemData data = new FoodItemData();

   

    private void Awake()
    {
        data.typeItem = TypeItem.Food;
    }
}