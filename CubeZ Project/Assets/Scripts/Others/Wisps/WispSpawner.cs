using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WispSpawner : MonoBehaviour
    {

    private const string PATH_SETTINGS_SPAWNER = "Wisps/WispsSettings";

    private const string PATH_PREFAB_WISP = "Prefabs/Others/wisp";

    private const float TIME_OUT_SPAWN = 0.5f;

    private Wisp wispPrefab;

 private   WispSpawnerSettings spawnerSettings;



 private   WorldManager worldManager;

    private List<Wisp> wisps = new List<Wisp>(0);
        // Use this for initialization
        void Start()
    {
        if (WorldManager.Manager == null)
        {
            throw new WispSpawnerException("world manager not found");
        }

        spawnerSettings = Resources.Load<WispSpawnerSettings>(PATH_SETTINGS_SPAWNER);

        if (spawnerSettings == null)
        {
            throw new WispSpawnerException("settings wisps spawner not found");
        }

        wispPrefab = Resources.Load<Wisp>(PATH_PREFAB_WISP);

        if (wispPrefab == null)
        {
            throw new WispSpawnerException("prefab wisp not found");
        }

        worldManager = WorldManager.Manager;

        worldManager.onDayChanged += NewDayDetect;

        RequestWisps();


    }

    void RequestWisps()
    {
        Wisp[] wisps = FindObjectsOfType<Wisp>();


        for (int i = 0; i < wisps.Length; i++)
        {
            Wisp wisp = wisps[i];
            wisp.onRemove += WispRemove;
            this.wisps.Add(wisp);
        }
    }

    private void NewDayDetect(DayTimeType dayType)
    {
        StartCoroutine(NewOperation(dayType == DayTimeType.Night ? WispSpawnerOperationType.Create : WispSpawnerOperationType.Destroy));
    }

    // Update is called once per frame
    void Update()
        {

        }

    private IEnumerator NewOperation (WispSpawnerOperationType operationType)
    {

        switch (operationType)
        {
            case WispSpawnerOperationType.Create:

                for (int i = 0; i < Random.Range(1, spawnerSettings.MaxCountWisps + 1); i++)
                {
                    yield return new WaitForSeconds(TIME_OUT_SPAWN);
                    Wisp wisp = CreateWisp();
                    wisps.Add(wisp);
                }


                break;
            case WispSpawnerOperationType.Destroy:

                for (int i = 0; i < wisps.Count; i++)
                {
                    yield return new WaitForSeconds(TIME_OUT_SPAWN);
                    Wisp wisp = wisps[i];
                    wisp.Remove();
                }


                break;
            default:
                throw new WispSpawnerException($"not found variant operation for {operationType.ToString()}");
        }
    }

    private Wisp CreateWisp ()
    {
        Wisp newWisp = Instantiate(wispPrefab);
        newWisp.transform.position = worldManager.GetRandomPointWithRandomPlane();
        newWisp.onRemove += WispRemove;
        return newWisp;
    }

    private void WispRemove(Wisp wisp)
    {
        wisp.onRemove -= WispRemove;
        wisps.Remove(wisp);
    }
}