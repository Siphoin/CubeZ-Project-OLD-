using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioObject))]
    public class NewDayAudio : MonoBehaviour
    {
    private AudioSource audioSource;

    private AudioObject audioObject;

    [Header("Звук петуха")]
    [SerializeField] private AudioClip clipNewDay;

    [Header("Звук волка")]
    [SerializeField] private AudioClip clipNight;
    // Use this for initialization
    void Start()
        {


        if (WorldManager.Manager == null)
        {
            throw new NewDayAudioException("world manager not found");
        }

        if (clipNight == null)
        {
            throw new NewDayAudioException("clip night not seted");
        }

        if (clipNewDay == null)
        {
            throw new NewDayAudioException("clip day not seted");
        }

        if (!TryGetComponent(out audioObject))
        {
            throw new NewDayAudioException($"{name} not have component Audio Object");
        }

        audioSource = audioObject.GetAudioSource();


        WorldManager.Manager.onDayChanged += NewDay;
        }

    private void NewDay(DayTimeType day)
    {
        switch (day)
        {
            case DayTimeType.Morming:
                PlaySound(clipNewDay);
                break;
            case DayTimeType.Night:
                PlaySound(clipNight);
                break;
        }
    }


    private void PlaySound (AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    }