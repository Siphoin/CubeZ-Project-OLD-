using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldManager : MonoBehaviour
{
    [SerializeField, ReadOnlyField] private SettingsZombie settingsZombie;
    [SerializeField, ReadOnlyField] private TimeOfWeakSettings settingsTimeOfWeak;
    private TimeOfWeakData timeOfWeakData;

    private static WorldManager manager;

    private GameObject[] planes;

    private const string PATH_SETTINGS_ZOMBIE = "Zombie/Zombie Settings";

    private const string PATH_SETTINGS_TIME_OF_WEAK = "World/TimeOfWeakSettings";

    public static WorldManager Manager { get => manager; }
    public SettingsZombie SettingsZombie { get => settingsZombie; }




    // Time of weak

    [SerializeField, ReadOnlyField] private Light directionLight;

    private Color colorActive;
    private Color colorNext;

    private float lerpingTimeValue = 0;


    private float rotateSpeedSun;

    private int currentDay = 1;

    private bool temperatureisLerping = false;

    private const string TAG_DIRECTION_LIGHT = "DirectionLight";
    private const string FOLBER_VFX_WEATHER = "Prefabs/VFX/";
    private const string TAG_PLANE = "Plane";


    [SerializeField, ReadOnlyField] private DayTimeType dayTimeType;

    [Header("Weathers")]
    [SerializeField, ReadOnlyField] private ParticleSystem rainWeather;
    [SerializeField, ReadOnlyField] private ParticleSystem fogWeather;
    [SerializeField, ReadOnlyField] private ParticleSystem snowWeather;

    private ParticleSystem activeWeather = null;

    int maxRangeWeatherType = Enum.GetValues(typeof(WeatherType)).Length;

    WeatherType currentWeather = WeatherType.Sun;

    int currentTemperature = 20;

    int oldTemperature = 20;

    public event Action<DayTimeType> onDayChanged;
    public event Action<WeatherType> onWeatherChanged;
    public event Action onTemperatureChanged;
    public string TemperatureString { get => currentTemperature + " °C"; }
    public int TemperatureValue { get => currentTemperature; }
    public int CurrentDay { get => currentDay; }
    public WeatherType CurrentWeather { get => currentWeather; }
    public DayTimeType CurrentDayTime { get => dayTimeType; }
    public Light DirectionLight { get => directionLight; }

    //

    // Use this for initialization
    void Awake()
    {
        planes = GameObject.FindGameObjectsWithTag(TAG_PLANE);


        if (manager == null)
        {
            manager = this;
        }

        else
        {
            Destroy(gameObject);
        }
        LoadWeathers();
        try
        {
        directionLight = GameObject.FindGameObjectWithTag(TAG_DIRECTION_LIGHT).GetComponent<Light>();
        }
        catch
        {

            throw new WorldManagerException("Direction Light not found");
        }

        // load settings list


        LoadSettingsZombies();
        LoadTimeOfWeakSettings();

        //
        rotateSpeedSun = timeOfWeakData.incrementHourValue * 100;
        SetDayTime();

        StartCoroutine(SunLerpingColor());

        StartCoroutine(NewWeather());

        SetFirstWeather();

        RandomizeTemperature();

        StartCoroutine(RandomTemperature());

        SetStartDay();

    }


    private void LoadTimeOfWeakSettings()
    {
        settingsTimeOfWeak = Resources.Load<TimeOfWeakSettings>(PATH_SETTINGS_TIME_OF_WEAK);

        if (settingsTimeOfWeak == null)
        {
            throw new WorldManagerException("time of weak settings is null");
        }

        timeOfWeakData = new TimeOfWeakData(settingsTimeOfWeak.GetData());
    }

    private void SetStartDay ()
    {
        currentDay = timeOfWeakData.startDay;
    }

    private void IncrementDay ()
    {
        currentDay++;
    }

    private ParticleSystem LoadWeatherVFX(WeatherType weatherType)
    {
        return Resources.Load<ParticleSystem>($"{FOLBER_VFX_WEATHER}{weatherType}");
    }

    private void LoadSettingsZombies()
    {
        settingsZombie = Resources.Load<SettingsZombie>(PATH_SETTINGS_ZOMBIE);
        if (settingsZombie == null)
        {
            throw new WorldManagerException("zombie settings is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        directionLight.transform.Rotate(new Vector3(0, rotateSpeedSun * Time.deltaTime, 0), Space.World);
    }

    #region Time Of Weak System
    private void SetDayTime()
    {
        DayTimeType selectedTypeDay = DayTimeType.Day;
        Color selectedColorLight = new Color();

        if (timeOfWeakData.startedHour <= 6f)
        {
            directionLight.intensity = timeOfWeakData.intensityNight;
            selectedTypeDay = DayTimeType.Morming;
            selectedColorLight = timeOfWeakData.colorMoming;
        }

        else if (timeOfWeakData.startedHour >= 11 && timeOfWeakData.startedHour <= 16)
        {
            directionLight.intensity = timeOfWeakData.intensityDay;
            selectedTypeDay = DayTimeType.Day;
            selectedColorLight = timeOfWeakData.colorDay;
        }

        else if (timeOfWeakData.startedHour <= 4 || timeOfWeakData.startedHour > 16)
        {
            directionLight.intensity = timeOfWeakData.intensityNight;
            selectedTypeDay = DayTimeType.Night;
            selectedColorLight = timeOfWeakData.colorNight;

        }

        dayTimeType = selectedTypeDay;
        directionLight.color = selectedColorLight;


    }

    private IEnumerator SunLerpingColor()
    {
        NextColorSun();

        while (true)
        {
            yield return new WaitForSeconds(timeOfWeakData.incrementHourValue);
            lerpingTimeValue += timeOfWeakData.incrementHourValue / timeOfWeakData.valueIntensityLerping * Time.timeScale;
            //   Debug.Log(lerpingTimeValue);

            directionLight.color = Color.Lerp(colorActive, colorNext, lerpingTimeValue);
            //      RenderSettings.ambientLight = Color.Lerp(colorActive, colorNext, lerpingTimeValue);

            if (lerpingTimeValue >= 1)
            {
                dayTimeType = (DayTimeType)(int)dayTimeType + 1;
                if ((int)dayTimeType > 2)
                {
                    dayTimeType = DayTimeType.Morming;
                }
                lerpingTimeValue = 0;
                NextColorSun();
            }
        }
    }

    private IEnumerator SunLerpingIntensity(float endValue)
    {
        float lerp = 0;
        float startValue = directionLight.intensity;
        if (endValue == startValue)
        {
            yield return null;
        }
        while (true)
        {
            yield return new WaitForSeconds(timeOfWeakData.incrementHourValue);
            lerp += timeOfWeakData.incrementHourValue / timeOfWeakData.valueIntensityLerping * Time.timeScale;
            //  Debug.Log(lerp);

            directionLight.intensity = Mathf.Lerp(startValue, endValue, lerp);


            if (lerp >= 1)
            {
                yield break;
            }
        }
    }



    private void NextColorSun()
    {
        colorNext = new Color();
        colorActive = new Color();
        switch (dayTimeType)
        {
            case DayTimeType.Morming:
                colorActive = timeOfWeakData.colorMoming;
                StartCoroutine(SunLerpingIntensity(timeOfWeakData.intensityDay));
                colorNext = timeOfWeakData.colorDay;
                IncrementDay();

                break;
            case DayTimeType.Day:
                colorActive = timeOfWeakData.colorDay;
                colorNext = timeOfWeakData.colorNight;
                StartCoroutine(SunLerpingIntensity(timeOfWeakData.intensityNight));
                break;
            case DayTimeType.Night:
                colorActive = timeOfWeakData.colorNight;
                colorNext = timeOfWeakData.colorMoming;
                StartCoroutine(SunLerpingIntensity(timeOfWeakData.intensityDay));
                break;
            default:
                break;
        }

        onDayChanged?.Invoke(dayTimeType);
    }

    #endregion

    #region Weather System

    private void SetFirstWeather()
    {
        WeatherType newWeather = (WeatherType)Random.Range(0, maxRangeWeatherType);
        SetNewWeather(newWeather);
    }

    private void LoadWeathers()
    {
        rainWeather = LoadWeatherVFX(WeatherType.Rain);
        snowWeather = LoadWeatherVFX(WeatherType.Snow);
        fogWeather = LoadWeatherVFX(WeatherType.Fog);
        if (fogWeather == null)
        {
            throw new WorldManagerException("fog weather vfx is null!");
        }

        if (rainWeather == null)
        {
            throw new WorldManagerException("rain weather vfx is null!");
        }

        if (snowWeather == null)
        {
            throw new WorldManagerException("snow weather vfx is null!");
        }
    }


    private IEnumerator NewWeather()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeOfWeakData.weatherSettings.minValueNewWeatherTime, timeOfWeakData.weatherSettings.maxValueNewWeatherTime + 1));

            if (ProbabilityUtility.GenerateProbalityInt() > 50)
            {
WeatherType newWeather = (WeatherType)Random.Range(0, maxRangeWeatherType);
            if (currentWeather != newWeather)
            {
                DestroyLastCreatedwWeather();
                yield return new WaitForSeconds(6);
                SetNewWeather(newWeather);
            }
            }
            
        }
    }

    private void DestroyLastCreatedwWeather()
    {
        if (activeWeather != null)
        {
            activeWeather.Stop();
            activeWeather.gameObject.AddComponent<TimerDestroy>().timeDestroy = 6;
            activeWeather = null;
        }
    }

    private void SetNewWeather(WeatherType weatherType)
    {
        if (weatherType != WeatherType.Fog && currentWeather == WeatherType.Fog)
        {
            SetActiveFog(false);
        }


        currentWeather = weatherType;


        if (weatherType == WeatherType.Sun)
        {
            onWeatherChanged?.Invoke(weatherType);
            return;
        }
        ParticleSystem newWeather = null;

        switch (weatherType)
        {
            case WeatherType.Rain:
                newWeather = rainWeather;
                DecrementTemperature(timeOfWeakData.temperatureSettings.rainTemperatureDecrementValue);
                break;
            case WeatherType.Snow:

                newWeather = snowWeather;

                int newTemperature = timeOfWeakData.temperatureSettings.snowTemperatureDecrementValue;


                if (currentTemperature - timeOfWeakData.temperatureSettings.snowTemperatureDecrementValue > 0)
                {
                    newTemperature = (currentTemperature - currentTemperature - Random.Range(timeOfWeakData.temperatureSettings.snowTemperatureDecrementValue, timeOfWeakData.temperatureSettings.snowTemperatureDecrementValue * 2));
                }
                DecrementTemperature(newTemperature);


                break;
            case WeatherType.Fog:
                newWeather = fogWeather;
                SetActiveFog(true);
                break;
            default:
                break;
        }

        activeWeather = Instantiate(newWeather);
        onWeatherChanged?.Invoke(currentWeather);
    }

    private void SetActiveFog(bool active)
    {
        StartCoroutine(LerpingFog(active));
    }

    IEnumerator LerpingFog(bool enable)
    {

        float lerpValue = 0;

        float lerpAValue = enable == true ? 0 : 0.1f;
        float lerpBValue = enable == true ? 0.1f : 0;
        while (true)
        {
            yield return new WaitForSeconds(timeOfWeakData.weatherSettings.fogIntensityFPS / 60.0f * Time.deltaTime);
            float speed = timeOfWeakData.weatherSettings.fogIntensityFPS / 60.0f * Time.deltaTime;
            lerpValue += speed;
            RenderSettings.fogDensity = Mathf.Lerp(lerpAValue, lerpBValue, lerpValue);

            if (lerpValue >= 1)
            {
                yield break;
            }

        }
    }

    IEnumerator LerpingTemperature()
    {
        if (temperatureisLerping)
        {
            yield break;
        }

        temperatureisLerping = true;
        float lerpValue = 0;
        float startedTemperature = currentTemperature;
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 20) * Time.deltaTime);
            lerpValue += 0.03f;
            currentTemperature = (int)Mathf.Lerp(oldTemperature, startedTemperature, lerpValue);
            CallChangededTemperature();

            if (lerpValue >= 1)
            {
                temperatureisLerping = false;
                yield break;
            }


        }
    }

    IEnumerator RandomTemperature()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5F, timeOfWeakData.weatherSettings.maxValueNewWeatherTime + 1 / 2));
            RandomizeTemperature();
        }
    }

    private void RandomizeTemperature()
    {
        SaveOldTemperature();
        currentTemperature = Random.Range(timeOfWeakData.temperatureSettings.minTemperatureValue, timeOfWeakData.temperatureSettings.maxTemperatureValue + 1);
        StartCoroutine(LerpingTemperature());
    }

    private void DecrementTemperature(int value)
    {
        SaveOldTemperature();
        currentTemperature -= value;
        StartCoroutine(LerpingTemperature());

    }

    private void IncrementTemperature(int value)
    {
        SaveOldTemperature();
        currentTemperature += value;
        StartLerpingTemperature();

    }

    public void NewTimeScale (float value)
    {
        Time.timeScale = value;
    }

    private void StartLerpingTemperature()
    {
        StartCoroutine(LerpingTemperature());
    }

    private void SaveOldTemperature()
    {
        oldTemperature = currentTemperature;
    }

    private void CallChangededTemperature()
    {
        onTemperatureChanged?.Invoke();
    }
    #endregion

    #region Path Find Mechanim 

    public Vector3 GetRandomPointWithRandomPlane ()
    {
        Transform randomPlane = planes[Random.Range(0, planes.Length)].transform;
        return NavMeshManager.GenerateRandomPath(randomPlane.position);
    }


    

    #endregion

    private int ClampingTemperature (float value)
    {
        return (int)Mathf.Clamp(value, timeOfWeakData.temperatureSettings.minTemperatureValue, timeOfWeakData.temperatureSettings.minTemperatureValue);
    }

}