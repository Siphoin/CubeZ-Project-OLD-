using UnityEngine;

[System.Serializable]
public class KitParams : BaseParamsItem
{
    [Header("Коэфицент лечения злоровья персонажа")]
    [Range(1, 100)]
    public int regenRange = 1;
    public KitParams()
    {

    }

    public KitParams(KitParams copyClass)
    {
        copyClass.CopyAll(this);
    }
}