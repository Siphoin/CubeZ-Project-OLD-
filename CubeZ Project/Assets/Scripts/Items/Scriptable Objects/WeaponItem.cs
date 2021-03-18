using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Weapon", order = 2)]
public class WeaponItem : BaseItem
{
    [Header("Данные об оружии")]
     public WeaponParams dataWeapon = new WeaponParams();



    private void Awake()
    {
        data.typeItem = TypeItem.Weapon;
    }

    public WeaponItem ()
    {

    }

    public WeaponItem (int damageBonus, int strength)
    {
        dataWeapon.damageBonus = damageBonus;
        dataWeapon.strength = strength;
    }

    public WeaponItem(WeaponItem copyClass)
    {
        copyClass.CopyAll(this);
    }

    

}