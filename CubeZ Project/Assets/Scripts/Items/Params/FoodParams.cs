using UnityEngine;

[System.Serializable]
public class FoodParams 
{
    [Range(1, 100)]
    [Header("Коэфицент повышения сытости")]
    public int satietyRange = 1;
 public   FoodParams ()
    {

    }

    public FoodParams (FoodParams copyClass)
    {
        copyClass.CopyAll(this);
    }
}