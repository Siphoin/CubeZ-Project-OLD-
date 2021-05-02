using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class LoaderMap : MonoBehaviour
    {
        public event Action<float> onProgressLoading;
    public event Action<string> onNewStepLoading;
    private BigInteger countOperations = 0;
    private BigInteger countOperationsTotal = 0;

    private List<GameObject> mapComponents = new List<GameObject>();

    private List<GameObject> mapComponentsLoaded = new List<GameObject>();

    private GameObject[] playerVariants;

    private GameObject mainCanvasPrefab;

    private PlayerManager playerManagerPrefab;

    private PlayerManager playerManagerActive;

    private LoadingMapUI loadingMapPrefabUI;

    private const string PATH_PREFABS_MAP_COMPONENTS = "System/mapComponents";

    private const string PATH_PREFABS_MAIN_CANVAS = "Prefabs/UI/MainCanvas";

    private const string PATH_PREFABS_PLAYER_VARIANTS = "Prefabs/Players";

    private const string PATH_PREFAB_UI_LOADING = "Prefabs/UI/ProgressLoadMapUI";

    private const string PATH_PLAYER_MANAGER = "System/mapComponents/main/PlayerManager";

    [SerializeField] private float timeOutNewOperation = 0.2f;

    // Use this for initialization
    void Awake()
    {
        Ini();

        Time.timeScale = 0;
        CalсulateCountOperations();

        if (!LoaderGameCache.IsLoaded)
        {
            StartCoroutine(LoadingMapComponents());
        }

    }

    private void Ini()
    {

        loadingMapPrefabUI = Resources.Load<LoadingMapUI>(PATH_PREFAB_UI_LOADING);

        if (loadingMapPrefabUI == null)
        {
            throw new LoaderMapException("loading map ui prefab not found");
        }


        mainCanvasPrefab = Resources.Load<GameObject>(PATH_PREFABS_MAIN_CANVAS);


        if (mainCanvasPrefab == null)
        {
            throw new LoaderMapException("player manager prefab not found");
        }
        playerManagerPrefab = Resources.Load<PlayerManager>(PATH_PLAYER_MANAGER);

        if (playerManagerPrefab == null)
        {
            throw new LoaderMapException("player manager prefab not found");
        }


        if (mainCanvasPrefab == null)
        {
            throw new LoaderMapException("main canvas prefab not found");
        }
        GameObject[] components = Resources.LoadAll<GameObject>(PATH_PREFABS_MAP_COMPONENTS);

        for (int i = 0; i < components.Length; i++)
        {
            mapComponents.Add(components[i]);
        }

        if (components == null || components.Length == 0)
        {
            throw new LoaderMapException("map xomponents not found");
        }

        playerVariants = Resources.LoadAll<GameObject>(PATH_PREFABS_PLAYER_VARIANTS);

        if (playerVariants == null || playerVariants.Length == 0)
        {
            throw new LoaderMapException("player prefab variants not found");
        }

        playerManagerActive = Instantiate(playerManagerPrefab);
        Instantiate(loadingMapPrefabUI, transform);
    }

    private void CalсulateCountOperations ()
    {
        countOperationsTotal += (long)GameCacheManager.gameCache.containerCacheObjects.objectsClones.Count;
        countOperationsTotal += (long)GameCacheManager.gameCache.containerCacheObjects.objectsInstances.Count;
        foreach (var item in GameCacheManager.gameCache.dataContainerObjects.objectsData)
        {
            IEnumerable<CacheObjectData> list = item.Value;
         countOperationsTotal += (long)list.OfType<CacheObjectData>().Count();
        }

        countOperationsTotal += (long)mapComponents.Count;

        if (!LoaderGameCache.IsLoaded)
        {
            countOperationsTotal++;
        }


#if UNITY_EDITOR
        Debug.Log($"calculated {countOperationsTotal} operations for loading map");
#endif


    }

    private IEnumerator LoadingMapComponents ()
    {
        NewStepLoading("Loading map components...");
        int indexComponent = 0;
        GameObject containerComponentsMap = new GameObject("containerComponentsMap");

        while (true)
        {
            yield return new WaitForSecondsRealtime(1.0f / 60.0f);
            if (indexComponent < mapComponents.Count)
            {
                GameObject component = Instantiate(mapComponents[indexComponent]);
                component.SetActive(false);
                component.transform.SetParent(containerComponentsMap.transform);
                mapComponentsLoaded.Add(component);
                indexComponent++;
                IncrementFinishedCountOperations();
            }

            else
            {
                NewStepLoading("creating player...");
                CreatePlayerOnRandomPoint();
                yield return new WaitForSecondsRealtime(1.0f / 60.0f);
                for (int i = 0; i < mapComponentsLoaded.Count; i++)
                {
                    try
                    {
                        GameObject component = mapComponentsLoaded[i];
                        component.SetActive(true);

                    }
                    catch
                    {

                    }
                
                }
                NewStepLoading("creating main UI...");
                CreateMainCanvas();
                Time.timeScale = 1;
                Destroy(gameObject);
                yield break;
            }
        }
    }

    private void IncrementFinishedCountOperations ()
    {
        countOperations++;
        float result = ((float)countOperations / (float)countOperationsTotal) * 100;

        result = (float)Math.Round(result, 0);
        onProgressLoading?.Invoke(result);
    }

    private void CreateMainCanvas ()
    {
        Instantiate(mainCanvasPrefab);
    }

    private void CreatePlayerOnRandomPoint ()
    {
        GameObject selectedPlayer = playerVariants[Random.Range(0, playerVariants.Length)];

        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");

        GameObject selectedPlane = planes[Random.Range(0, planes.Length)];

        Vector3 pos = NavMeshManager.GenerateRandomPath(selectedPlane.transform.position);
        pos.y = selectedPlayer.transform.position.y;

        GameObject player = Instantiate(selectedPlayer);
        player.transform.position = pos;

        Vector3 angle = Vector3.zero;

        angle.y = Random.Range(0f, 181f);

        player.transform.rotation = UnityEngine.Quaternion.Euler(angle);
        Character character;


        if (!player.TryGetComponent(out character))
        {
            throw new LoaderMapException("player prefab not have componment Character");
        }

        playerManagerActive.SetPlayer(character);
        GameCamera.Main.CentringToTarget(player.transform);
        GameCamera.Main.SetTarget(player.transform);
        IncrementFinishedCountOperations();
    }

    private void NewStepLoading (string text)
    {
        onNewStepLoading?.Invoke(text);
    }



    }