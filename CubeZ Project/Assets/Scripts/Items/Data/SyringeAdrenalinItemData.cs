using System.Collections;
using UnityEngine;
    public class SyringeAdrenalinItemData : ItemBaseData
    {
    [Header("Данные шприца адреналина")]
    public SyringeAdrenalinParams syringeAdrenalinData = new SyringeAdrenalinParams();
    public SyringeAdrenalinItemData()
    {
    }

    public SyringeAdrenalinItemData(ItemBaseData data)
    {
        data.CopyAll(this);
    }

    public SyringeAdrenalinItemData(SyringeAdrenalinItemData copyClass)
    {
        copyClass.CopyAll(this);
    }
}