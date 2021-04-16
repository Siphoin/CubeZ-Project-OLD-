using UnityEngine;


[RequireComponent(typeof(MusicPlayer))]
    public class AudioNightMusic : MonoBehaviour
    {
    private MusicPlayer musicPlayer;

    [Header("Трек ночной музыки")]
    [SerializeField] AudioClip clipNightMusic;
        // Use this for initialization
        void Start()
        {

        if (WorldManager.Manager == null)
        {
            throw new AudioNightMusicException("world manager not found");
        }

        if (clipNightMusic == null)
        {
            throw new AudioNightMusicException("clip night music not seted");
        }


        if (!TryGetComponent(out musicPlayer))
        {
            throw new AudioNightMusicException($"{name} not have component Music Player");
        }

        WorldManager.Manager.onDayChanged += NewDayTime;
        }

    private void NewDayTime(DayTimeType dayTime)
    {
        if (dayTime == DayTimeType.Night)
        {
            musicPlayer.ReplaceTrack(clipNightMusic);
        }

       else if (dayTime == DayTimeType.Morming)
        {
            musicPlayer.ReturnOriginalListMusic();
        }
    }

    }