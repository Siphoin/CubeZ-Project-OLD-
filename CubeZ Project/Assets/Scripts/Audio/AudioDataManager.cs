﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AudioDataManager : MonoBehaviour
    {
    private static AudioDataManager manager;

    private AudioData audioData = new AudioData();


    public event Action<float> onFXVolumeChanged;
    public event Action<float> onMusicVolumeChanged;
    public event Action<bool> onMusicEnabled;

    private const string PATH_PREFAB_AUDIO_OBJECT = "Prefabs/Audio/AudioObject";

    private AudioObject audioObjectPrefab;

    public static AudioDataManager Manager { get => manager; }

    // Use this for initialization
    void Awake()
    {
        if (manager == null)
        {
            manager = this;
            audioObjectPrefab = Resources.Load<AudioObject>(PATH_PREFAB_AUDIO_OBJECT);

            if (audioObjectPrefab == null)
            {
                throw new AudioDataManagerEException("audio object prefab not found");
            }
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }


    public float GetVolumeFX ()
    {
     return   audioData.fxVolume;
    }

    public float GetVolumeMusic ()
    {
        return audioData.musicVolume;
    }

    public bool GetMusicEnabled ()
    {
        return audioData.musicEnabled;
    }

    public void SetVolumeFX (float value)
    {
        audioData.fxVolume = ClampingVolume(value);
        onFXVolumeChanged?.Invoke(audioData.fxVolume);
    }

    public void SetVolumeMusic (float value)
    {
        audioData.musicVolume = ClampingVolume(value);
        onMusicVolumeChanged?.Invoke(audioData.musicVolume);
    }

    private float ClampingVolume (float value)
    {
        return Mathf.Clamp(value, 0.0f, 1.0f);
    }

    public void SetEnabledMusic (bool status)
    {
        audioData.musicEnabled = status;
        onMusicEnabled?.Invoke(audioData.musicEnabled);
    }

    public AudioObject CreateAudioObject (Vector3 position, AudioClip clip = null)
    {
        AudioObject audioObject = Instantiate(audioObjectPrefab);
        audioObject.transform.position = position;
        audioObject.GetAudioSource().clip = clip;
        

        return audioObject;
    }


}