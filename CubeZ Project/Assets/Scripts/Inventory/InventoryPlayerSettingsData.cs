using UnityEngine;

[System.Serializable]
public class InventoryPlayerSettingsData
{
    [Header("Максимальное кол-во предметов в инвентаре")]
    public int maxCountItems = 25;
    public InventoryPlayerSettingsData()
    {

    }

    public InventoryPlayerSettingsData(InventoryPlayerSettingsData copyClass)
    {
        copyClass.CopyAll(this);
    }
}