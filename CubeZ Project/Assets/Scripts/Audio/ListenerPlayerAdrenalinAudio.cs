using System;
using System.Collections;
using UnityEngine;

    public class ListenerPlayerAdrenalinAudio : MonoBehaviour, IRemoveObject
    {
    [Header("Музыка при адреналине игрока")]
    [SerializeField] private AudioClip clipAdrenalinMusic;

    private MusicPlayer musicPlayer;

    private AudioClip[] oldTrackListMusicPlayer;


    private void Start()
    {

        if (clipAdrenalinMusic == null)
        {
            throw new ListenerPlayerAdrenalinAudioException("clip adrenalin music not seted");
        }


        if (PlayerManager.Manager == null || PlayerManager.Manager.Player == null)
        {
            Remove();
        }

        else
        {
            musicPlayer = FindObjectOfType<MusicPlayer>();

            if (musicPlayer == null)
            {
                throw new ListenerPlayerAdrenalinAudioException("music player not found");
            }


            PlayerManager.Manager.Player.onAdrenalin += SetAdrenalinMusic;
            PlayerManager.Manager.Player.onDead += Remove;


            oldTrackListMusicPlayer = musicPlayer.GetActiveTrackList();
        }
    }

    private void SetAdrenalinMusic(bool adrenalin)
    {
        if (adrenalin)
        {
            oldTrackListMusicPlayer = musicPlayer.GetActiveTrackList();
            musicPlayer.ReplaceTrack(clipAdrenalinMusic);
        }

        else
        {
            musicPlayer.SetTrackList(oldTrackListMusicPlayer);
        }




    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    
}
