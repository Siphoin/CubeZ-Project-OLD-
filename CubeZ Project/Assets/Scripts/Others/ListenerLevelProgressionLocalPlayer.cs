using System;
using UnityEngine;

public class ListenerLevelProgressionLocalPlayer : MonoBehaviour
    {

    private const string PATH_AUDIO_LEVEL_UP = "Audio/level/level_up";

    private const string PATH_VFX_LEVEK_UP = "Prefabs/VFX_2/level_up_effect";


    private static ListenerLevelProgressionLocalPlayer manager;


    public event Action onProgressXP;
    public event Action onLevelUp;

    private AudioDataManager audioManager;

    private AudioClip clipLevelUp;

    private VFXLevelUp fXLevelUpPrefab;

    public static ListenerLevelProgressionLocalPlayer Manager { get => manager; }



    // Use this for initialization
    void Start()
        {
        if (AudioDataManager.Manager == null)
        {
            throw new ListenerLevelProgressionLocalPlayerException("audio manager");
        }
        if (PlayerManager.Manager == null)
        {
            throw new ListenerLevelProgressionLocalPlayerException("player manager not found");
        }

        else if (PlayerManager.Manager.Player == null)
        {
            throw new ListenerLevelProgressionLocalPlayerException("local player not found");
        }

        clipLevelUp = Resources.Load<AudioClip>(PATH_AUDIO_LEVEL_UP);

        if (clipLevelUp == null)
        {
            throw new ListenerLevelProgressionLocalPlayerException("clip level up not found");
        }

       fXLevelUpPrefab = Resources.Load<VFXLevelUp>(PATH_VFX_LEVEK_UP);

        if (fXLevelUpPrefab == null)
        {
            throw new ListenerLevelProgressionLocalPlayerException("prefab fx level not found");
        }
        manager = this;

        audioManager = AudioDataManager.Manager;

        PlayerManager.Manager.Player.onXPAdded += XPProgress;
        }

    private void XPProgress(long value)
    {
        PlayerCacheDataProgressLevel dataLevel = GameCacheManager.gameCache.levelProgressPlayer;

        long currentXP = dataLevel.currentXP;
        long maxXPOnLevel = dataLevel.currentXPForNextLevel;
        dataLevel.currentXP = (long)Mathf.Clamp(currentXP + value, 0, maxXPOnLevel);
        onProgressXP?.Invoke();


        if (dataLevel.currentXP >= maxXPOnLevel)
        {

            dataLevel.currentLevel++;
            dataLevel.currentXP = 0;
            dataLevel.currentXPForNextLevel *= 2;


            PlaySoundLevelUp();

            CreateVFXLevelUp();

            onLevelUp?.Invoke();
        }
    }

    private void PlaySoundLevelUp()
    {
        AudioObject audioObject = audioManager.CreateAudioObject(GetPositionLocalPlayer(), clipLevelUp);
        audioObject.RemoveIFNotPlaying();
        audioObject.GetAudioSource().Play();
    }

    private void CreateVFXLevelUp ()
    {
        Instantiate(fXLevelUpPrefab, PlayerManager.Manager.Player.transform).transform.position = GetPositionLocalPlayer();
    }

    private Vector3 GetPositionLocalPlayer ()
    {
        return PlayerManager.Manager.Player.transform.position;
    }
}