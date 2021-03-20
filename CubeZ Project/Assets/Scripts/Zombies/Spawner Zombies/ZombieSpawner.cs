using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
    {
    private BaseZombie[] zombiesVariants;

    private const string PATH_ZOMBIES_PREFABS_FOLBER = "Prefabs/Zombies";

    private const float MIN_TIME_SPAWN_ONE_ZOMBIE = 0.5f;
    private const float MAX_TIME_SPAWN_ONE_ZOMBIE = 1.0F;
    private const float RADIUS_HORDE = 1.5f;

    private SettingsZombieData zombieData;
    private SettingsZombie settingsZombie;

    private int maxCountTypesSpawn = 0;
    // Use this for initialization
    void Start()
        {
        if (MAX_TIME_SPAWN_ONE_ZOMBIE < 0)
        {
            throw new ZombieSpawnerException("max time spawn one zombie < 0");
        }

        if (MIN_TIME_SPAWN_ONE_ZOMBIE < 0)
        {
            throw new ZombieSpawnerException("min time spawn one zombie < 0");
        }
        zombiesVariants = Resources.LoadAll<BaseZombie>(PATH_ZOMBIES_PREFABS_FOLBER);

        if (zombiesVariants.Length == 0)
        {
            throw new ZombieSpawnerException("zombies not found");
        }
        if (WorldManager.Manager == null)
        {
            throw new ZombieSpawnerException("world manager not found");
        }
        settingsZombie = WorldManager.Manager.SettingsZombie;
        zombieData = new SettingsZombieData(settingsZombie.GetData());
        maxCountTypesSpawn = (int)Enum.GetValues(typeof(TypeSpawnZombie)).Cast<TypeSpawnZombie>().Max();
        Debug.Log($"spawner zombies on. Count types zombies as {zombiesVariants.Length}");
        StartCoroutine(Spawn());

       
    }

        // Update is called once per frame
        void Update()
        {

        }

    private IEnumerator Spawn ()
    {
        while (true)
        {
            float time = Random.Range(zombieData.minTimeSpawnZombie, zombieData.maxTimeSpawnZombie + 1.0f);
            yield return new WaitForSeconds(time);
            int countZombies = Random.Range(1, zombieData.maxZombiesCountInHorde + 1);
            TypeSpawnZombie typeSpawn = TypeSpawnZombie.One;
            Vector3 center = NavMeshManager.GenerateRandomPath(transform.position);
            if (countZombies > 1)
            {
               typeSpawn = (TypeSpawnZombie)Random.Range(0, maxCountTypesSpawn);
            }

            Debug.Log(typeSpawn);
            for (int i = 0; i < countZombies; i++)
            {
                yield return new WaitForSeconds(Random.Range(MIN_TIME_SPAWN_ONE_ZOMBIE, MAX_TIME_SPAWN_ONE_ZOMBIE + 1));

                switch (typeSpawn)
                {
                    case TypeSpawnZombie.One:

                        Vector3 posZombie = NavMeshManager.GenerateRandomPath(transform.position);
                        CreateZombie(posZombie);


                    break;
                    case TypeSpawnZombie.Horde:
                        float maxAngle = 180.0f;
                        float x = center.x + RADIUS_HORDE * (float)Math.Cos(Random.Range(maxAngle * -1, maxAngle + 1));
                        float z = center.z + RADIUS_HORDE * (float)Math.Tan(Random.Range(maxAngle * -1, maxAngle + 1));
                        Vector3 posZombieHorde = new Vector3(x, center.y, z);
                        CreateZombie(posZombieHorde);
                        break;
                    default:
                        break;
                }
            }

            Debug.Log($"Spawned {countZombies} zombies");
        }
    }

    private void CreateZombie (Vector3 position)
    {
        BaseZombie selectedPrefab = zombiesVariants[Random.Range(0, zombiesVariants.Length)];
        BaseZombie newZombie = Instantiate(selectedPrefab);
        newZombie.transform.position = position;
        float maxAngle = 180.0f;
        float selectedAngle = Random.Range(maxAngle * -1.0f, maxAngle + 1.0f);
        var quaternion = Quaternion.identity;
       quaternion.y = selectedAngle;
        newZombie.transform.rotation = quaternion;

    
    }

    public void CreateZombie(Vector3 position, Quaternion quaternion)
    {
        BaseZombie selectedPrefab = zombiesVariants[Random.Range(0, zombiesVariants.Length)];
        BaseZombie newZombie = Instantiate(selectedPrefab);
        newZombie.transform.position = position;
        float maxAngle = 180.0f;
        float selectedAngle = Random.Range(maxAngle * -1.0f, maxAngle + 1.0f);
        newZombie.transform.rotation = quaternion;


    }


}