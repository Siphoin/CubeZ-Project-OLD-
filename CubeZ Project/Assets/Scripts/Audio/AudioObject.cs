using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
    public class AudioObject : MonoBehaviour, IRemoveObject, IInvokerMono
    {
    [SerializeField] private AudioType typeAudio = AudioType.FX;

    private AudioSource audioSource;

    private AudioDataManager dataManager;

    public bool RemoveIfNotPlaying { get; set; } = false;


    // Use this for initialization
    void Start()
    {
        Ini();
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }
    private void Ini()
    {
        if (AudioDataManager.Manager == null)
        {
            throw new AudioObjecException("Audio manager not found");
        }


        dataManager = AudioDataManager.Manager;
        audioSource = GetComponent<AudioSource>();
        switch (typeAudio)
        {
            case AudioType.FX:
                dataManager.onFXVolumeChanged += ChangeVolume;

                ChangeVolume(dataManager.GetVolumeFX());
                break;
            case AudioType.Music:
                dataManager.onMusicVolumeChanged += ChangeVolume;

                ChangeVolume(dataManager.GetVolumeFX());
                break;
            default:
                throw new AudioObjecException($"invalid type audio: {typeAudio}");
        }

        if (RemoveIfNotPlaying)
        {
            CallInvokingMethod(Remove, audioSource.clip.length + 0.01f);
        }

        
    }

    


    private void ChangeVolume (float value)
    {
        audioSource.volume = value;
    }

    public void Remove()
    {
        Uncribe();

        Destroy(gameObject);

    }

    private void Uncribe()
    {
        if (dataManager != null)
        {
            switch (typeAudio)
            {
                case AudioType.FX:
                    dataManager.onFXVolumeChanged -= ChangeVolume;
                    break;
                case AudioType.Music:
                    dataManager.onMusicVolumeChanged -= ChangeVolume;
                    break;
                default:
                    throw new AudioObjecException($"invalid type audio: {typeAudio}");
            }
        }
    }

    public AudioSource GetAudioSource ()
    {
        if (audioSource == null)
        {
            Ini();
        }

        return audioSource;
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        try
        {
            Uncribe();
        }
        catch
        {
        }
    }
}