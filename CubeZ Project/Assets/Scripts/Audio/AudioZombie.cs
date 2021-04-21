using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BaseZombie))]
    public class AudioZombie : MonoBehaviour, IInvokerMono
    {
    private BaseZombie zombie;

    private const float minSecondsPlayVoice = 7.0f;
    private const float maxSecondsPlayVoice = 25.0f;

    private const float minPitchVoice = 0.9f;
    private const float maxPitchVoice = 1.5f;

    private const string NAME_FOLBER_SOUNDS = "Audio/zombie/";

    private const string NAME_AGRESSIVE_SOUNDS = "agressive";
    private const string NAME_WALKING_SOUNDS = "walking";
    private const string NAME_ATTACK_SOUNDS = "attack";


    private const string NAME_DEATH_SOUND = "death/zombie_death";

    private const string PATH_CHARACTER_AUDIO_WALK = "Audio/character/character_walking";

    private AudioClip[] agressiveClips;
    private AudioClip[] walkingClips;
    private AudioClip[] attackClips;

    private AudioClip deathClip;

    private AudioClip walkClip;

    private AudioObject audioObjectWalk;



    [SerializeField] private AudioSource audioSource;

    private AudioDataManager audioManager;

    private int countCallCorotinueWalkingSound = 0;

    private IStateBehavior lastBehaviorZombie;


    // Use this for initialization
    void Start()
        {

        if (AudioDataManager.Manager == null)
        {
            throw new AudioZombieException("audio manager not found");
        }

        audioManager = AudioDataManager.Manager;


        if (audioSource == null)
        {
            throw new AudioZombieException("audio source not seted");
        }


        walkingClips = GetSounds(NAME_WALKING_SOUNDS);

        if (walkingClips.Length == 0)
        {
            throw new AudioZombieException("walking clips not found");
        }

       agressiveClips = GetSounds(NAME_AGRESSIVE_SOUNDS);

        if (agressiveClips.Length == 0)
        {
            throw new AudioZombieException("agressuive clips not found");
        }

        attackClips = GetSounds(NAME_ATTACK_SOUNDS);

        if (attackClips.Length == 0)
        {
            throw new AudioZombieException("attack clips not found");
        }

        deathClip = GetSound(NAME_DEATH_SOUND);

        if (deathClip == null)
        {
            throw new AudioZombieException("death clip not found");
        }

        walkClip = Resources.Load<AudioClip>(PATH_CHARACTER_AUDIO_WALK);

        if (walkClip == null)
        {
            throw new AudioZombieException("walk clip not found");
        }


        if (!TryGetComponent(out zombie))
        {
            throw new AudioZombieException("not found component Base Zombie");
        }


        zombie.onNewBehavior += NewBehavior;
        zombie.onDeath += ZombieDead;
        zombie.onAttack += PlayAttackSound;

        audioSource.pitch = Random.Range(minPitchVoice, maxPitchVoice);

        CreateWalkAudioObject();

        CallInvokingMethod(RandomVoice, Random.Range(minSecondsPlayVoice, maxSecondsPlayVoice + 1.0f));
    }

    private void PlayAttackSound()
    {
        AudioObject audioObject = audioManager.CreateAudioObject(transform.position, attackClips[Random.Range(0, attackClips.Length)]);
        audioObject.GetAudioSource().Play();
        audioObject.RemoveIfNotPlaying = true;
    }

    private void ZombieDead()
    {
        zombie.onNewBehavior -= NewBehavior;
        zombie.onDeath -= ZombieDead;
        zombie.onAttack -= PlayAttackSound;

            audioSource.clip = deathClip;
            audioSource.Play();
        
    }

    private void NewBehavior(IStateBehavior behavior)
    {

        if (lastBehaviorZombie == behavior)
        {
            return;
        }

        CancelInvoke();

        lastBehaviorZombie = behavior;


        if (behavior is WalkingStateZombie)
        {
            CallInvokingMethod(RandomVoice, Random.Range(minSecondsPlayVoice, maxSecondsPlayVoice + 1.0f));
            
        }

        if (behavior is AggresiveStateZombie)
        {


            if (!audioSource.isPlaying)
            {
                PlayRandomSoundWithArrayClips(agressiveClips);
            }
        }


    }

    private AudioClip[] GetSounds (string nameFolber)
    {
        return Resources.LoadAll<AudioClip>(NAME_FOLBER_SOUNDS + nameFolber);
    }

    private AudioClip GetSound (string nameFolber)
    {
        return Resources.Load<AudioClip>(NAME_FOLBER_SOUNDS + nameFolber);
    }

    private void CreateWalkAudioObject ()
    {
        audioObjectWalk = audioManager.CreateAudioObject(transform.position, walkClip);
        audioObjectWalk.transform.SetParent(transform);
        audioObjectWalk.GetAudioSource().loop = true;

        audioObjectWalk.name = "AudioZombieWalk";

    }

    private void Update()
    {
        if (zombie.IsWalking)
        {
            if (!audioObjectWalk.GetAudioSource().isPlaying)
            {
                audioObjectWalk.GetAudioSource().Play();
            }
        }

        else
        {
            if (audioObjectWalk.GetAudioSource().isPlaying)
            {
                audioObjectWalk.GetAudioSource().Stop();
            }
        }
    }

    private void PlayRandomSoundWithArrayClips (AudioClip[] clips)
    {
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        audioSource.clip = clip;

        audioSource.Play();
    }

    private void RandomVoice ()
    {

        if (zombie.CurrentStateBehavior is WalkingStateZombie == false)
        {
            return;
        }
        PlayRandomSoundWithArrayClips(walkingClips);

        if (zombie.CurrentStateBehavior is WalkingStateZombie)
        {
            CallInvokingMethod(RandomVoice, Random.Range(minSecondsPlayVoice, maxSecondsPlayVoice + 1.0f));
        }


    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }
}