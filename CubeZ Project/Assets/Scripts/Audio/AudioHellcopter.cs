using System.Collections;
using UnityEngine;

    public class AudioHellcopter : MonoBehaviour
    {
    private const string PATH_AUDIO_HELLCOPTER = "Audio/hellcopter";

    private const float MAX_TIME_OUT_PLAY_SOUND_HELLCOPTER = 3f;

    private AudioClip[] clips;

    [SerializeField] private AudioObject audioObject;

    private AudioSource audioSource;
        // Use this for initialization
        void Start()
        {
        if (audioObject == null)
        {
            throw new AudioHellcopterException("audio object hellcopter not seted");
        }
        audioSource = audioObject.GetAudioSource();

        clips = Resources.LoadAll<AudioClip>(PATH_AUDIO_HELLCOPTER);

        if (clips.Length == 0)
        {
            throw new AudioHellcopterException("clips hellcopter not found");
        }

        StartCoroutine(Cliping());
        
        }

     private IEnumerator Cliping ()
    {
        if (ProbabilityUtility.GenerateProbalityInt() > 50)
        {
            PlayRandomClip();
        }


        while (true)
        {
            float time = Random.Range(MAX_TIME_OUT_PLAY_SOUND_HELLCOPTER / 2, MAX_TIME_OUT_PLAY_SOUND_HELLCOPTER + 1.0F);
            yield return new WaitForSeconds(time);

        }
    }

    private void PlayRandomClip()
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }


}