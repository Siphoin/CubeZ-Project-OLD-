using System.Collections;
using UnityEngine;
[System.Serializable]
    public class WeatherDataSettings
    {
    [Header("Минимальное значение смены погоды")]
    public float minValueNewWeatherTime = 15;

    [Header("Максимальное значение смены погоды")]
    public float maxValueNewWeatherTime = 120;

    [Header("Скорость интерполюции интенсивности тумана")]
    [Header("Туман")]
    public float fogIntensityFPS = 5.0f;

    public WeatherDataSettings ()
    {

    }

    public WeatherDataSettings (WeatherDataSettings copyClass)
    {
        copyClass.CopyAll(this);
    }
}