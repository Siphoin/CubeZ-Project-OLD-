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

 [SerializeField, ReadOnlyField]   private Character character;

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
        CallInvokingEveryMethod(ProgressiveSleep, sleep.speedNeed);
        CallInvokingEveryMethod(ProgressiveSleep, sleep.speedNeed);
        //
        StartCoroutine(RunStatsControl());
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

    public  void CallInvokingMethod(Action method, float time)
    {
       Invoke(method.Method.Name, time);
    }

    public  void CallInvokingEveryMethod( Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    // needs mechanims

    private void ProgressiveHunger ()
    {
        ProgressiveNeed(hunger);
    }

    private void ProgressiveSleep()
    {
        ProgressiveNeed(sleep);
    }



    private void ProgressiveNeed (CharacterStatsDataNeed statsTarget)
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

    private IEnumerator RunStatsControl ()
    {
        while (true)
        {
            float coFSpeed = character.Speed;
            yield return new WaitForSeconds(coFSpeed * Time.deltaTime * 7);
            if (run.value < run.GetDefaultValue())
            {
                if (!character.IsRunning())
                {
                    run.value += (int)coFSpeed;
                    run.CallOnValueChanged();
                }
            }
            if (run.value > 0)
            {
                if (character.IsRunning())
                {
                    run.value -= (int)coFSpeed;
                    run.CallOnValueChanged();
                }
                
            }

            else
            {
                run.value = 0;
                run.CallOnValueChanged();
            }
           


            
        }
    }


    //

      
    }