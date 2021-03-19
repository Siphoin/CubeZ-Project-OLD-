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

    [HideInInspector]
    public bool inFastPanel = false;

    [HideInInspector]
    public int indexFastPanel = -1;

    [HideInInspector]
    public string id;


    public ItemBaseData()
    {
        inFastPanel = false;
    }


    public ItemBaseData(ItemBaseData copyClass)
    {
        copyClass.CopyAll(this);
        inFastPanel = false;

    }


    public void GenerateId()
    {
        id = shortid.ShortId.Generate(false, false, 20);

    }
}