using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRandomEnviromentSounds : MonoBehaviour
    {
    private const string PATH_ENVIROMENT_RANDOM_SOUNDS = "Audio/enviroment_effect";

    private const string PATH_SETTINGS_RANDOM_SOUNDS = "RandomEnviromentSounds/RandomSoundsSettings";

    private AudioClip[] soundsVariants;

    private AudioDataManager audioManager;

    private RandomEnviromentSoundsSettings randomEnviromentSoundsSettings;

    private WorldManager worldManager;
        // Use this for initialization
        void Start()
        {
        if (AudioDataManager.Manager == null)
        {
            throw new SpawnerRandomEnviromentSoundsException("Audio data manager not found");
        }

        audioManager = AudioDataManager.Manager;

        if (WorldManager.Manager == null)
        {
            throw new SpawnerRandomEnviromentSoundsException("World manager not found");
        }

        worldManager = WorldManager.Manager;

        randomEnviromentSoundsSettings = Resources.Load<RandomEnviromentSoundsSettings>(PATH_SETTINGS_RANDOM_SOUNDS);

        if (randomEnviromentSoundsSettings == null)
        {
            throw new SpawnerRandomEnviromentSoundsException("random sounds enviroment settings not found");
        }


        soundsVariants = Resources.LoadAll<AudioClip>(PATH_ENVIROMENT_RANDOM_SOUNDS);

        if (soundsVariants == null || soundsVariants.Length == 0)
        {
            throw new SpawnerRandomEnviromentSoundsException("audio sounds variants enviroment not found");
        }

        StartCoroutine(PlayRandomSound());


        }
    private IEnumerator PlayRandomSound ()
    {
        while (true)
        {
            float time = Random.Range(randomEnviromentSoundsSettings.MinTimeOutSpawnSound, randomEnviromentSoundsSettings.MaxTimeOutSpawnSound);
            yield return new WaitForSeconds(time);

            Character[] players = FindObjectsOfType<Character>();
            Vector3 pos = new Vector3();
            if (players != null && players.Length > 0)
            {
                pos = Random.insideUnitSphere * randomEnviromentSoundsSettings.RadiusOfThePlayerSound + players[Random.Range(0, players.Length)].transform.position;
            }

            else
            {
                pos = worldManager.GetRandomPointWithRandomPlane();
            }

            AudioObject audioObject = audioManager.CreateAudioObject(pos, soundsVariants[Random.Range(0, soundsVariants.Length)]);
            audioObject.RemoveIFNotPlaying();
            audioObject.GetAudioSource().Play();
        }
    }


    }