using System.Collections;
using System.Linq;
using UnityEngine;

    public class HellcopterSpawner : MonoBehaviour
    {

    private const string PATH_SETTINGS_SPAWNER = "HellCopter_Data/HellCopterSpawnerSettings";


    private const string PATH_PREFAB_HELLCOPTER = "Prefabs/Others/mi_17";


    private HellCopterSpawnerSettings hellCopterSpawnerSettings;

    private Character[] players;

    private GameObject hellcopterPrefab;

    



    // Use this for initialization
    void Start()
        {
        hellCopterSpawnerSettings = Resources.Load<HellCopterSpawnerSettings>(PATH_SETTINGS_SPAWNER);

        if (hellCopterSpawnerSettings == null)
        {
            throw new HellcopterSpawnerException("hellcopter spawner settings not found");
        }

        if (hellCopterSpawnerSettings.MinTimeOutSpawn >= hellCopterSpawnerSettings.MaxTimeOutSpawn)
        {
            throw new HellcopterSpawnerException("hellcopter spawner settings data not found");
        }

        hellcopterPrefab = Resources.Load<GameObject>(PATH_PREFAB_HELLCOPTER);

        if (hellcopterPrefab == null)
        {
            throw new HellcopterSpawnerException("prefab hellcopter not found");
        }


        players = FindObjectsOfType<Character>();
        if (players == null || players.Length == 0)
        {
            throw new HellcopterSpawnerException("not found players");
        }

        StartCoroutine(Spawn());
        }

    private void CreateHellCopter ()
    {


        Character player = players[Random.Range(0, players.Length)];
        Vector3 vectorRandom = Random.insideUnitSphere * hellCopterSpawnerSettings.RadiusOfThePlayer + player.transform.position;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(planes, vectorRandom.ToBounds()))
        {
            GameObject hellcopter = Instantiate(hellcopterPrefab);
            hellcopter.transform.position = vectorRandom;
        }

        else
        {
            CreateHellCopter();
            return;
        }
    }

    private IEnumerator Spawn ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(hellCopterSpawnerSettings.MinTimeOutSpawn, hellCopterSpawnerSettings.MaxTimeOutSpawn + 1.0f));


            if (players.Length > 0 && players.Any(pl => pl == null))
            {
            players = players.Where(player => player != null).ToArray();
            }


            if (players.Length == 0)
            {
                players = FindObjectsOfType<Character>();
            }
          else  if (players.Length > 0)
            {
                CreateHellCopter();
            }

        }
    }

    }