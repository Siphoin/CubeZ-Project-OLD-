using UnityEngine;

[System.Serializable]
public class WeaponItemData : ItemBaseData
{
    [Header("Параметры предмета")]
    public WeaponParams data = new WeaponParams();
    public WeaponItemData()
    {

    }

    public WeaponItemData(WeaponItemData copyClass)
    {
        copyClass.CopyAll(this);
    }
}