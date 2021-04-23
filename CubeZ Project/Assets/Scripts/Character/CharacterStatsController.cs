using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Character))]
public class CharacterStatsController : MonoBehaviour, IInvokerMono
{
    private Dictionary<NeedCharacterType, CharacterStatsDataNeed> needs = new Dictionary<NeedCharacterType, CharacterStatsDataNeed>();

    // stats fields

    private CharacterStatsDataNeed hunger;

    private CharacterStatsDataNeed sleep;

    private CharacterStatsDataNeed run;

    private CharacterStatsDataNeed temperatureBody;

    private CharacterStatsDataNeed health;

    [SerializeField, ReadOnlyField] private Character character;

    private WorldManager worldManager;

    public CharacterStatsDataNeed Hunger { get => hunger; }
    public CharacterStatsDataNeed Sleep { get => sleep; }
    public CharacterStatsDataNeed Run { get => run; }
    public CharacterStatsDataNeed TemperatureBody { get => temperatureBody; }
    public CharacterStatsDataNeed Health { get => health; }

    // Use this for initialization
    void Awake()
    {
        character = GetComponent<Character>();
        needs = character.CharacterStats.GetDictonaryNeeds();

        // ini fields
        sleep = needs[NeedCharacterType.Sleep];
        hunger = needs[NeedCharacterType.Eat];
        run = needs[NeedCharacterType.Run];
        temperatureBody = needs[NeedCharacterType.Temperature];
        health = needs[NeedCharacterType.Health];


        //

        // start system needs
        CallInvokingEveryMethod(ProgressiveHunger, hunger.speedNeed);

        //
        StartCoroutine(RunStatsControl());
        StartCoroutine(TemperatureStatsControl());
        StartCoroutine(SleepStatsControl());
    }


    private void Start()
    {
        worldManager = WorldManager.Manager;
    }

    // Update is called once per frame
    void Update()
    {
        if (character.IsDead)
        {
            CancelInvoke();
            StopAllCoroutines();
            enabled = false;
        }
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    // needs mechanims

    private void ProgressiveHunger()
    {
        ProgressiveNeed(hunger);
    }




    private void ProgressiveNeed(CharacterStatsDataNeed statsTarget)
    {
        if (statsTarget.value > 0)
        {
            statsTarget.value -= 1;
        }
;
        if (statsTarget.value <= 0)
        {
            switch (statsTarget.needType)
            {
                case NeedCharacterType.Eat:
                    DamageCharacter();

                    break;
                case NeedCharacterType.Sleep:
                    break;
                case NeedCharacterType.Temperature:
                    DamageCharacter();
                    break;
                case NeedCharacterType.Run:
                    break;
                case NeedCharacterType.Health:
                    break;
                default:
                    break;
            }
        }

        statsTarget.CallOnValueChanged();


    }

    private void DamageCharacter()
    {
        character.Hit(1, false);
    }

    private IEnumerator RunStatsControl()
    {
        while (true)
        {
            float coFSpeed = character.Speed;
            yield return new WaitForSeconds(coFSpeed * Time.deltaTime * 7);
            if (run.value < run.GetDefaultValue())
            {
                if (!character.IsRunning())
                {
                    run.value = Mathf.Clamp(run.value + (int)coFSpeed, 0, run.GetDefaultValue());
                    run.CallOnValueChanged();
                }
            }
            if (run.value > 0)
            {
                if (character.IsRunning())
                {
                    run.value = Mathf.Clamp(run.value - (int)coFSpeed, 0, run.GetDefaultValue());
                    run.CallOnValueChanged();
                }

            }

        }
    }

    private IEnumerator TemperatureStatsControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(temperatureBody.speedNeed);
            if (worldManager.TemperatureValue < character.CharacterStats.optimalTemperatureBody && !character.InFireArea)
            {
                if (temperatureBody.value > 0 && character)
                {
                    temperatureBody.value -= 1;
                    temperatureBody.CallOnValueChanged();
                }

                else
                {
                    DamageCharacter();
                }


            }

            else
            {
                if (temperatureBody.value < temperatureBody.GetDefaultValue())
                {
                    temperatureBody.value += 1;
                    temperatureBody.CallOnValueChanged();
                }
            }

        }
    }

    private IEnumerator SleepStatsControl ()
    {
        while (true)
        {
            yield return new WaitForSeconds(sleep.speedNeed);

            if (sleep.value > 0)
            {
                if (!character.IsSleeping)
                {
                   sleep.value -= 1;
                    sleep.CallOnValueChanged();
                }
            }



            if (sleep.value < sleep.GetDefaultValue())
            {
                if (character.IsSleeping)
                {
                    sleep.value += 2;
                    sleep.CallOnValueChanged();
                }
            }

            character.SetFatigue(sleep.value <= 0);
        }
    }

    public void AddValueToNeed(NeedCharacterType type, int value)
    {
        CharacterStatsDataNeed targetNeed = null;

        switch (type)
        {
            case NeedCharacterType.Eat:
                targetNeed = hunger;
                break;
            case NeedCharacterType.Sleep:
                targetNeed = sleep;
                break;
            case NeedCharacterType.Temperature:
                targetNeed = temperatureBody;
                break;
            case NeedCharacterType.Run:
                targetNeed = run;
                break;
            case NeedCharacterType.Health:
                targetNeed = health;
                break;
            default:
                throw new CharacterStatsControllerException("invalid type need");
        }

        targetNeed.value = Mathf.Clamp(targetNeed.value += value, 0, targetNeed.GetDefaultValue());
        targetNeed.CallOnValueChanged();
    }


    //


}