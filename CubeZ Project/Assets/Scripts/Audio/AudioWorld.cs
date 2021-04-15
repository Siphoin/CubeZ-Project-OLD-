using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioWorld : MonoBehaviour
    {
    [Header("Звук дня")]
    [SerializeField] private AudioClip dayClip;

    [Header("Звук ночи")]
    [SerializeField] private AudioClip nightClip;

    private AudioSource audioSource;

    private bool lerping = false;

   private AudioDataManager audioManager;
        // Use this for initialization
        void Start()
        {
        if (WorldManager.Manager == null)
        {
            throw new AudioWorldException("world manager not found");
        }

        if (AudioDataManager.Manager == null)
        {
            throw new AudioWorldException("audio data manager not found");
        }


        audioManager = AudioDataManager.Manager;



        if (!TryGetComponent(out audioSource))
        {
            throw new AudioWorldException($"{name} not have component Audio Source");
        }

        WorldManager.Manager.onDayChanged += NewSound;
        AudioDataManager.Manager.onFXVolumeChanged += ChangeVolume;
        }

    private void ChangeVolume(float value)
    {
        if (!lerping)
        {
            audioSource.volume = value;
        }
    }

    private void NewSound(DayTimeType day)
    {
        audioSource.Stop();
        audioSource.clip = day == DayTimeType.Night ? nightClip : dayClip;
        StartCoroutine(LerpingVolume());
        audioSource.Play();
    }


    private IEnumerator LerpingVolume ()
    {
        lerping = true;
        float lerpValue = 0;
        while (true)
        {
            float fpsRate = 1.0f / 60.0f;

            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;
            audioSource.volume = Mathf.Lerp(0, audioManager.GetVolumeFX(), lerpValue);

            if (lerpValue >= 1)
            {
                lerping = false;
                yield break;
            }
        }
    }
    }