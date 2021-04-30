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

    private WorldManager worldManager;

    private int maxCountTypesSpawn = 0;
    private int zombieinWorld = 0;
    private int maxCountZombieinWorld = 0;

    [SerializeField, ReadOnlyField]
    private int currentMaxCountZombies = 0;

    private GameObject zombiesContainer;

    
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
        worldManager = WorldManager.Manager;

        settingsZombie = WorldManager.Manager.SettingsZombie;


        zombieData = new SettingsZombieData(settingsZombie.GetData());


        maxCountTypesSpawn = (int)Enum.GetValues(typeof(TypeSpawnZombie)).Cast<TypeSpawnZombie>().Max();


        maxCountZombieinWorld = zombieData.countZombiesWorld;

        worldManager.onDayChanged += WorldManager_onDayChanged;


        Debug.Log($"spawner zombies on. Count types zombies as {zombiesVariants.Length}");
        zombiesContainer = new GameObject("zombiesContainer");
        CachingExitedsZombies();


        IncrementValueZombiesCount();

        StartCoroutine(Spawn());

    }


    private void CachingExitedsZombies()
    {
        BaseZombie[] baseZombies = FindObjectsOfType<BaseZombie>();
        zombieinWorld += baseZombies.Length;


        for (int i = 0; i < baseZombies.Length; i++)
        {
            baseZombies[i].onRemove += ZombieRemoved;
            baseZombies[i].transform.SetParent(zombiesContainer.transform);
        }
    }



    private IEnumerator Spawn ()
    {
        yield return new WaitForSeconds(6);
        while (true)
        {
            
            float time = Random.Range(zombieData.minTimeSpawnZombie, zombieData.maxTimeSpawnZombie + 1.0f);
            yield return new WaitForSeconds(time);


            int countZombies = Random.Range(0, currentMaxCountZombies - zombieinWorld + 1);
            TypeSpawnZombie typeSpawn = TypeSpawnZombie.One;
            Vector3 center = worldManager.GetRandomPointWithRandomPlane();
            if (countZombies > 1)
            {
               typeSpawn = (TypeSpawnZombie)Random.Range(0, maxCountTypesSpawn + 1);
            }

            for (int i = 0; i < countZombies; i++)
            {
                yield return new WaitForSeconds(Random.Range(MIN_TIME_SPAWN_ONE_ZOMBIE, MAX_TIME_SPAWN_ONE_ZOMBIE + 1));

                switch (typeSpawn)
                {
                    case TypeSpawnZombie.One:
                        Vector3 posZombie = worldManager.GetRandomPointWithRandomPlane(true);
                        CreateZombie(posZombie);
                    break;
                    case TypeSpawnZombie.Horde:
                        var posZombieHorde = center + RADIUS_HORDE * Random.insideUnitSphere;
                        posZombieHorde.y = -center.y;
                        CreateZombie(posZombieHorde);
                        break;
                    default:
                            throw new ZombieSpawnerException("Type spawn is invalid");
                }
            }
            if (countZombies > 0)
            {
            Debug.Log($"Spawned {countZombies} zombies. Count zombies in World {zombieinWorld}");
            }

            }
           

        
    }

    private void CreateZombie (Vector3 position)
    {
        BaseZombie selectedPrefab = zombiesVariants[Random.Range(0, zombiesVariants.Length)];

        BaseZombie newZombie = Instantiate(selectedPrefab);

        AddZombieToContainer(newZombie);

        SetPosionZombie(position, newZombie);


        float maxAngle = 180.0f;
        float selectedAngle = Random.Range(maxAngle * -1.0f, maxAngle + 1.0f);
        var quaternion = Quaternion.identity;


        quaternion.y = selectedAngle;
        SetAngleZombie(quaternion, newZombie);
        SubcribeRemoveZombie(newZombie);
        IncrementZombieCountCurrent();


    }

    private void AddZombieToContainer(BaseZombie newZombie)
    {
        newZombie.transform.SetParent(zombiesContainer.transform);
    }

    private void ZombieRemoved ()
    {
        zombieinWorld--;
    }

    private void IncrementZombieCountCurrent ()
    {
        zombieinWorld++;
    }

    public BaseZombie CreateZombie(Vector3 position, Quaternion quaternion)
    {
        BaseZombie selectedPrefab = zombiesVariants[Random.Range(0, zombiesVariants.Length)];
        BaseZombie newZombie = Instantiate(selectedPrefab);

        AddZombieToContainer(newZombie);

        SetPosionZombie(position, newZombie);
        SetAngleZombie(quaternion, newZombie);
        SubcribeRemoveZombie(newZombie);


        IncrementZombieCountCurrent();
        return newZombie;


    }

    private void SubcribeRemoveZombie(BaseZombie newZombie)
    {
        newZombie.onRemove += ZombieRemoved;
    }

    private  void SetAngleZombie(Quaternion quaternion, BaseZombie newZombie)
    {
        newZombie.transform.rotation = quaternion;
    }

    private  void SetPosionZombie(Vector3 position, BaseZombie newZombie)
    {
        newZombie.transform.position = position;
    }

   private void IncrementValueZombiesCount ()
    {
        currentMaxCountZombies = Mathf.Clamp(currentMaxCountZombies + zombieData.zombieIncrementEveryDay, zombieData.zombieIncrementEveryDay, maxCountZombieinWorld);
    }

    private void WorldManager_onDayChanged(DayTimeType dayTime)
    {
        if (dayTime == DayTimeType.Night)
        {
            currentMaxCountZombies *= zombieData.zombieIncrementNight;
        }

        else if (dayTime == DayTimeType.Morming)
        {
            currentMaxCountZombies  /= zombieData.zombieIncrementNight;


            IncrementValueZombiesCount();
        }
    }
}