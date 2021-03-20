using UnityEngine;
[System.Serializable]
public class TimeOfWeakData
{
    [Header("Цвета временного периода")]
    public Color colorMoming = Color.black;
    public Color colorDay = Color.black;
    public Color colorNight = Color.black;


    [Header("Наступление временного периода по часам")]

    public float incrementHourValue = 0.01f;

    public float mormingStartedHour = 6f;
    public float dayStartedHour = 11f;

    public float nightStartedHour = 16f;

    public float startedHour = 11f;

    [Header("Интенсивность солнца днем и утром")]
    public float intensityDay = 1;


    [Header("Интенсивность солнца ночью")]
    public float intensityNight = 0.1f;


    [Header("Коэфицент снижения/прибавления интенсивности солнца")]
    public float valueIntensityLerping = 60f;


    [Header("Параметры погоды")]
    public WeatherDataSettings weatherSettings = new WeatherDataSettings();
    [Header("Параметры температуры")]
    public TemperatureWorldData temperatureSettings = new TemperatureWorldData();

    [Header("Стартовый день")]
    [Header("Настройки дня")]
    public int startDay = 1;

    public TimeOfWeakData()
    {

    }

    public TimeOfWeakData(TimeOfWeakData copyClass)
    {
        copyClass.CopyAll(this);
    }
}