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
    public AudioType TypeAudio { get => typeAudio; }

    public event Action<AudioObject> onRemove;

    private Character localPlayer;


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

                ChangeVolume(dataManager.GetVolumeMusic());
                break;
            default:
                throw new AudioObjecException($"invalid type audio: {typeAudio}");
        }


        if (RemoveIfNotPlaying)
        {
            CallInvokingMethod(Remove, audioSource.clip.length + 0.01f);
        }

        FindLocalPlayer();

    }

    private void FindLocalPlayer()
    {
        if (typeAudio != AudioType.FX)
        {
            return;
        }



        if (PlayerManager.Manager == null)
        {
            return;
        }

        else
        {
            localPlayer = PlayerManager.Manager.Player;

            if (localPlayer != null)
            {
                localPlayer.onSleep += VolumeToZero;

            }
        }
    }



    private void ChangeVolume (float value)
    {
        if (localPlayer == null && typeAudio == AudioType.FX)
        {
        audioSource.volume = value;
        }

        else if (localPlayer != null)
        {
            audioSource.volume = localPlayer.IsSleeping == false ? value : 0;
        }

       

    }

   private void VolumeToZero (bool sleepCharacter)
    {
        Ini();
        audioSource.volume = sleepCharacter == false ? dataManager.GetVolumeFX() : 0;
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


    private void OnDestroy()
    {
        try
        {
            Uncribe();
            onRemove?.Invoke(this);
        }
        catch
        {
        }
    }

}