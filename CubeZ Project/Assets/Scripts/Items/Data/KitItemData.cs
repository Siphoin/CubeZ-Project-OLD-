using UnityEngine;

[System.Serializable]
public class KitItemData : ItemBaseData
{
    [Header("Параметры предмета")]
    public KitParams itemParams = new KitParams();

    public KitItemData()
    {
    }

    public KitItemData(ItemBaseData data)
    {
        data.CopyAll(this);
    }

    public KitItemData(KitItemData copyClass)
    {
        copyClass.CopyAll(this);
    }
}