using System.Collections;
using UnityEngine;

    public class BloodSpawner : MonoBehaviour
    {
    [SerializeField] private int maxCountBloodDecals = 100;

    private GameObject bloodPrefab;

    private const string PATH_BLOOD_PREFAB = "Prefabs/Others/blood_decal";

    private WorldManager worldManager;

        // Use this for initialization
        void Start()
        {
        if (WorldManager.Manager == null)
        {
            throw new BloodSpawnerException("world manager not found");
        }

        worldManager = WorldManager.Manager;

        bloodPrefab = Resources.Load<GameObject>(PATH_BLOOD_PREFAB);

        if (bloodPrefab == null)
        {
            throw new BloodSpawnerException("blood decal prefab not found");
        }


        StartCoroutine(SpawnBlood());
        }

    private IEnumerator SpawnBlood ()
    {
        GameObject parentBloods = new GameObject("bloodContainer");
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (count < maxCountBloodDecals)
            {
                GameObject blood = Instantiate(bloodPrefab, parentBloods.transform);
                blood.transform.position = worldManager.GetRandomPointWithRandomPlane();
                count++;
            }

            else
            {
                Destroy(gameObject);
            }
        }
    }
    }