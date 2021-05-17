using System;
using UnityEngine;

public class CarAlarmSystem : MonoBehaviour, IInvokerMono
    {

    private const string PATH_SETTINGS_ALARM_SYSTEM = "Cars/AlarmSystem/AlarmSystemCarSeetings";

    private const string PATH_SOUND_ALARM = "Audio/car/car_collision_env";


    private AudioObject activeAlarmSound = null;

    private CarAlarmSystemSettings carAlarmSystemSettings = null;

    private AudioDataManager audioManager;

    private AudioClip clipAlarmSound;


    [Header("Фары машины")]
  [SerializeField]  private Light[] headlights;

    [Header("Триггер сигнализации")]
    [SerializeField] private GameObject triggerAlarm;
        // Use this for initialization
        void Start()
        {


        if (triggerAlarm == null)
        {
            throw new CarAlarmSystemException("trigger alarm not seted");
        }


        if (AudioDataManager.Manager == null)
        {
            throw new CarAlarmSystemException("audio data manager not found");
        }

        audioManager = AudioDataManager.Manager;

        carAlarmSystemSettings = Resources.Load<CarAlarmSystemSettings>(PATH_SETTINGS_ALARM_SYSTEM);

        if (carAlarmSystemSettings == null)
        {
            throw new CarAlarmSystemException("car alarm system settings not found");
        }

       clipAlarmSound = Resources.Load<AudioClip>(PATH_SOUND_ALARM);

        if (clipAlarmSound == null)
        {
            throw new CarAlarmSystemException("car alarm system clip not found");
        }


        OffTriggerAlarm();

    }



        private void OnCollisionEnter(Collision collision)
        {
        if (activeAlarmSound == null && ProbabilityUtility.GenerateProbalityInt() >= carAlarmSystemSettings.ProbalityAlarmOn)
        {
            activeAlarmSound = audioManager.CreateAudioObject(transform.position, clipAlarmSound);
            activeAlarmSound.GetAudioSource().Play();
            activeAlarmSound.RemoveIFNotPlaying();

            SetStateAlarmTrigger(true);


            if (headlights.Length > 0)
            {
                SetStateVisibleHeadlights(true);
                CallInvokingMethod(OffHeadLights, clipAlarmSound.length + 0.1f);
            }

            CallInvokingMethod(OffTriggerAlarm, clipAlarmSound.length + 0.1f);

        }
        }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void SetStateVisibleHeadlights (bool status)
    {
        for (int i = 0; i < headlights.Length; i++)
        {
            headlights[i].gameObject.SetActive(status);
        }
    }

    private void OffHeadLights ()
    {
        SetStateVisibleHeadlights(false);
    }

    private void OffTriggerAlarm ()
    {
        SetStateAlarmTrigger(false);
    }

    private void SetStateAlarmTrigger (bool status)
    {
        triggerAlarm.SetActive(status);
    }

}