using System.Collections;
using UnityEngine;


    public class InfectedCorpseSpawner : MonoBehaviour
    {
    private const string PATH_VARIANTS_INFECTED_CORPSES = "Prefabs/InfectedCorpes";

    private InfectedCorpse[] corpsesVariants;

    private SettingsZombieData zombieStats;

    private WorldManager worldManager;
        // Use this for initialization
        void Start()
        {
        if (WorldManager.Manager == null)
        {
            throw new InfectedCorpseSpawnrtException("world manager not found");
        }

        corpsesVariants = Resources.LoadAll<InfectedCorpse>(PATH_VARIANTS_INFECTED_CORPSES);

        if (corpsesVariants == null || corpsesVariants.Length == 0)
        {
            throw new InfectedCorpseSpawnrtException("not found corpses variants");
        }
        worldManager = WorldManager.Manager;
      zombieStats = worldManager.SettingsZombie.GetData();

        StartCoroutine(SpawnCorpses());

#if UNITY_EDITOR
        Debug.Log($"corpse spawner activated. count variants corpses as {corpsesVariants.Length}");
#endif
    }

    private IEnumerator SpawnCorpses ()
    {
        while (true)
        {
            float time = Random.Range(zombieStats.minInfeectedCorpsesSpawn, zombieStats.maxInfeectedCorpsesSpawn);
            yield return new WaitForSeconds(time);
            InfectedCorpse corpse = Instantiate(corpsesVariants[Random.Range(0, corpsesVariants.Length)]);
            corpse.transform.position = worldManager.GetRandomPointWithRandomPlane(true);
        }
    }

    }