using System.Collections;
using UnityEngine;
[System.Serializable]
    public class SyringeAdrenalinParams : BaseParamsItem
    {
    [Header("Коэфицент прибавления урона персонажу")]
    public int bonusDamage;
    [Header("Коэфицент прибавления скорости бега персонажу")]
    public int bonusSpeed;

    [Header("Длительность действия")]
    public int duration;

    public SyringeAdrenalinParams ()
    {

    }

    public SyringeAdrenalinParams (SyringeAdrenalinParams copyClass)
    {
        copyClass.CopyAll(this);
    }
}