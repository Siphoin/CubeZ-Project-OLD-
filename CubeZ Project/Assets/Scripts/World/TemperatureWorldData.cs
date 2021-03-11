using System.Collections;
using UnityEngine;
[System.Serializable]
public class TemperatureWorldData 
{
    [Header("Минимальная температура")]
    public int minTemperatureValue = -40;
    [Header("Минимальная температура")]
    public int maxTemperatureValue = 50;

    [Header("Коэфицент снижения темпеатуры при дожде")]
    public int rainTemperatureDecrementValue = 2;

    [Header("Коэфицент снижения темпеатуры при снеге")]
    public int snowTemperatureDecrementValue = 6;


    public TemperatureWorldData ()
    {

    }

    public TemperatureWorldData (TemperatureWorldData copyClass)
    {
        copyClass.CopyAll(this);
    }
}