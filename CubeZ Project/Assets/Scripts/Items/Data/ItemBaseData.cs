using UnityEngine;

[System.Serializable]

public class ItemBaseData
{
    [Header("Тип предмета")]
    [ReadOnlyField] public TypeItem typeItem;
    [Header("Имя предмета")]
    public string ItemName;
    [Header("Описание предмета")]
    [TextArea]
    public string itemDecription;
    [Header("Иконка")]
    public Sprite icon;

    [Header("ID предмета")]
     public string idItem;

    public ItemBaseData ()
    {

    }


    public ItemBaseData (ItemBaseData copyClass)
    {
        copyClass.CopyAll(this);
    }

}