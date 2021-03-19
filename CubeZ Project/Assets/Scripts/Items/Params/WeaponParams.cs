using UnityEngine;

[System.Serializable]

public class WeaponParams : BaseParamsItem
{
    [Header("Коэфицент прибавления урона")]
    public int damageBonus = 1;

    [Header("Коэфицент прочности")]
    public int strength = 10;
    public WeaponParams()
    {

    }

    public WeaponParams(WeaponParams copyClass)
    {
        copyClass.CopyAll(this);
    }
}