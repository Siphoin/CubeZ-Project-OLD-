using UnityEngine;
[System.Serializable]
    public class WorldCacheData
    {
    public float fogIntensity;
    public float sunintensity;

    public int temperature;

    public SerializeColor colorSun;

    public Vector3 angleSun;

    public WeatherType weather;

    public DayTimeType dayType;

    public WorldCacheData ()
    {

    }

    public WorldCacheData (WorldManager worldManager)
    {
        Light sun = worldManager.DirectionLight;


        weather = worldManager.CurrentWeather;
        dayType = worldManager.CurrentDayTime;
        angleSun = sun.gameObject.transform.rotation.eulerAngles;
        colorSun = sun.color.Serialize();
        temperature = worldManager.TemperatureValue;
        fogIntensity = RenderSettings.fogDensity;
        sunintensity = sun.intensity;

    }
    
    }