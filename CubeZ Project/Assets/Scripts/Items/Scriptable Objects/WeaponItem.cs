using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Weapon", order = 2)]
public class WeaponItem : BaseItem
{
    [Header("Данные об оружии")]
    [SerializeField] WeaponItemData data = new WeaponItemData();



    private void Awake()
    {
        data.typeItem = TypeItem.Weapon;
    }
}