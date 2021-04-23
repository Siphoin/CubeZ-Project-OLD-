using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour, IInvokerMono
    {
    [SerializeField] private AudioClip[] musicList;

    [SerializeField] private AudioClip[] musicListCached;

    private AudioSource audioSource;

    private AudioDataManager audioManager;

    private bool lerping = false;

    private AudioClip lastAudioClip;
        // Use this for initialization
        void Start()
        {
        if (musicList.Length == 0)
        {
            throw new MusicPlayerException("music list is emtry");
        }
        if (AudioDataManager.Manager == null)
        {
            throw new MusicPlayerException("audio manager not found");
        }
        if (!TryGetComponent(out audioSource))
        {
            throw new MusicPlayerException("music list is emtry");
        }


        audioManager = AudioDataManager.Manager;
        AudioDataManager.Manager.onFXVolumeChanged += ChangeVolume;


        musicListCached = GetClipsWithArrayClips(musicListCached, musicList);
        NewTrack();


    }

    private void NewTrack()
    {
        AudioClip selectedTrack = null;
        if (musicList.Length < 2)
        {
            selectedTrack = musicList[0];
        }

        else
        {
            AudioClip[] tracks = musicList.Where(track => track != lastAudioClip).ToArray();
            selectedTrack = tracks[Random.Range(0, tracks.Length)];

        }
        StartCoroutine(LerpingVolume());
        PlayTrack(selectedTrack);
        CallInvokingMethod(NewTrack, selectedTrack.length + 0.1f);
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void ChangeVolume (float value)
    {
        if (!lerping)
        {
            audioSource.volume = value;
        }
    }

    private IEnumerator LerpingVolume()
    {
        lerping = true;
        float lerpValue = 0;
        while (true)
        {
            float fpsRate = 1.0f / 60.0f;

            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;
            audioSource.volume = Mathf.Lerp(0, audioManager.GetVolumeMusic(), lerpValue);

            if (lerpValue >= 1)
            {
                lerping = false;
                yield break;
            }
        }
    }

    private void PlayTrack (AudioClip track)
    {
        audioSource.clip = track;
        lastAudioClip = track;
        audioSource.Play();
    }

    public void ReplaceTrack (AudioClip track)
    {
        if (track == null)
        {
            throw new MusicPlayerException("track is null");
        }

        audioSource.Stop();
        CancelInvoke();
        StartCoroutine(LerpingVolume());


        musicList = new AudioClip[1];

        musicList[0] = track;

        PlayTrack(track);

        CallInvokingMethod(NewTrack, track.length + 1f);

    }

    public void ReturnOriginalListMusic ()
    {

        CancelInvoke();


        musicList = GetClipsWithArrayClips(musicList, musicListCached);


        audioSource.Stop();
        NewTrack();
    }

    private AudioClip[] GetClipsWithArrayClips (AudioClip[] arrayTarget, AudioClip[] arrayGet)
    {
        arrayTarget = new AudioClip[arrayGet.Length];

        for (int i = 0; i < arrayGet.Length; i++)
        {
           arrayTarget[i] = arrayGet[i];
        }

        return arrayTarget;
    }


}