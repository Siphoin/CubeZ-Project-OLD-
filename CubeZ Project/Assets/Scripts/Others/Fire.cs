using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fire : MonoBehaviour
    {
    private const float DIVISION_LERP_NUMBERS = 1000f;
    private const int ROUND_VALUE = 2;
    [Header("Партиклы огня")]
    [SerializeField] private ParticleSystem particles;

    [Header("Скорость затуханиЯ огня")]
    [SerializeField] private float speedAttenuation = 0;

    [Header("Свет огня")]
    [SerializeField] private Light lightFire;

    [Header("Триггер огня")]
    [SerializeField] private SphereCollider triggerColiderFire;

    private int countParticlesStart = 0;


    [Header("Стартовая скорость частиц")]
    [SerializeField]
    private int startSpeedParticles = 5;

    [Header("Стартовое время жизни частиц")]
    [SerializeField]
    private int startLifeTimrParticles = 5;

    MainModule settingsPartivles;

    private float bonusIncrement = 1;

   public Action onRemove;

    public int CountParticles { get => settingsPartivles.maxParticles; }
    public float intensity { get => lightFire.intensity; }
    // Use this for initialization
    void Start()
        {


        if (WorldManager.Manager == null)
        {
            throw new FireException("world manager not found");
        }


        if (speedAttenuation <= 0)
        {
            throw new FireException("speed attenuation not must be <= 0");
        }

        if (lightFire == null)
        {
            throw new FireException("light fire not seted");
        }

        if (particles == null)
        {
            throw new FireException("particles not seted");
        }

        if (triggerColiderFire == null)
        {
            throw new FireException("trigger colider fire not seted");
        }
        settingsPartivles = particles.main;
        countParticlesStart = settingsPartivles.maxParticles;
        startSpeedParticles = ROUND_VALUE;
        StartCoroutine(LogicFire());

        WorldManager.Manager.onWeatherChanged += NewWeather;

        }

    private void NewWeather (WeatherType weather)
    {
        switch (weather)
        {
            case WeatherType.Rain:
                bonusIncrement = 2;
                break;
            case WeatherType.Snow:
                bonusIncrement = 2.5f;
                break;
            default:
                bonusIncrement = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
        {

        }

    private IEnumerator LogicFire ()
    {
        float lerpValue = 0;
        while (true)
        {
            float rate = speedAttenuation * bonusIncrement * Time.deltaTime ;
            yield return new WaitForSeconds(rate);
            lerpValue += rate;
            lightFire.intensity = MathfExtensions.LerpRound(lightFire.intensity, 0, lerpValue / 1000f, ROUND_VALUE);
            triggerColiderFire.radius = lightFire.intensity / 5;


            settingsPartivles.maxParticles = (int)MathfExtensions.LerpRound(countParticlesStart, 0, lerpValue, ROUND_VALUE);
            settingsPartivles.startSpeed = MathfExtensions.LerpRound(startSpeedParticles, 0, lerpValue, ROUND_VALUE);
            settingsPartivles.startLifetime = MathfExtensions.LerpRound(startLifeTimrParticles, 0, lerpValue, ROUND_VALUE);
            if (lerpValue >= 1)
            {
                onRemove?.Invoke();
                WorldManager.Manager.onWeatherChanged -= NewWeather;
                Destroy(gameObject);
                yield break;
            }
        }
    }

    }