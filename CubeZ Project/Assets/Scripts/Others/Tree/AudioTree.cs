using UnityEngine;
[RequireComponent(typeof(Tree))]
    public class AudioTree : MonoBehaviour
    {
   private AudioClip[] clipsDead;

    private const string NAME_FOLBER_SOUNDS_DEAD = "Audio/tree";

    private AudioDataManager audioManager;

    private Tree tree;
        // Use this for initialization
        void Start()
        {

        if (AudioDataManager.Manager == null)
        {
            throw new AudioTreeException("audio manager not found");
        }


        clipsDead = Resources.LoadAll<AudioClip>(NAME_FOLBER_SOUNDS_DEAD);


        if (clipsDead.Length == 0)
        {
            throw new AudioTreeException("clips dead tree not found");
        }

        if (!TryGetComponent(out tree))
        {
            throw new AudioTreeException($"{name} not have component Tree");
        }


        audioManager = AudioDataManager.Manager;
        tree.onDead += PlaySoundDead;
        }

    private void PlaySoundDead()
    {
        tree.onDead -= PlaySoundDead;
        AudioObject audioObject = audioManager.CreateAudioObject(transform.position, clipsDead[UnityEngine.Random.Range(0, clipsDead.Length)]);
        AudioSource audioSource = audioObject.GetAudioSource();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioObject.RemoveIFNotPlaying();
        audioSource.Play();

    }
}