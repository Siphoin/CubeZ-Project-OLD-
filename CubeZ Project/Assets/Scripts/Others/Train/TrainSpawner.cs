using System.Collections;
using UnityEngine;

    public class TrainSpawner : MonoBehaviour
    {
    private const string PATH_PREFABS_TRAINS = "Prefabs/Others/Trains";

    private const string PATH_SETTINGS_SPAWNER = "Train/TrainSpawnerSettings";

    private const string TAG_TRAIN_POINT = "TrainPoint";

    private  Train[] trainsList;

    private TrainSpawnerSettings spawnerSettings;

    GameObject[] pointsSpawn;

        // Use this for initialization
        void Start()
        {

        spawnerSettings = Resources.Load<TrainSpawnerSettings>(PATH_SETTINGS_SPAWNER);

        if (spawnerSettings == null)
        {
            throw new TrainSpawnerException("spawner settings trains not found");
        }

        if (spawnerSettings.MinTimeOutSpawn >= spawnerSettings.MaxTimeOutSpawn)
        {
            throw new TrainSpawnerException("settings spawner trains not valid");
        }

        trainsList = Resources.LoadAll<Train>(PATH_PREFABS_TRAINS);

        if (trainsList.Length == 0)
        {
            throw new TrainSpawnerException("trains variants not found");
        }


        pointsSpawn = GameObject.FindGameObjectsWithTag(TAG_TRAIN_POINT);

        if (pointsSpawn.Length == 0)
        {
            throw new TrainSpawnerException("not found point for trains");
        }

        StartCoroutine(Spawn());
        }

    private IEnumerator Spawn ()
    {
        while (true)
        {
            float time = Random.Range(spawnerSettings.MinTimeOutSpawn, spawnerSettings.MaxTimeOutSpawn + 1.0f);

            yield return new WaitForSeconds(time);

            Train selectedTrain = trainsList[Random.Range(0, trainsList.Length)];
            Train newTrain = Instantiate(selectedTrain);

            GameObject point = pointsSpawn[Random.Range(0, pointsSpawn.Length)];

            newTrain.transform.position = point.transform.position;
            newTrain.transform.rotation = point.transform.rotation;
        }
    }

    }