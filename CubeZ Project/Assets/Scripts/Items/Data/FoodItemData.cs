using UnityEngine;

[System.Serializable]

public class FoodItemData : ItemBaseData
{
    [Header("Параметры предмета")]
    public FoodParams paramsItem = new FoodParams();
    public FoodItemData ()
    {

    }

    public FoodItemData (FoodItemData copyClass)
    {
        copyClass.CopyAll(this);
    }

}