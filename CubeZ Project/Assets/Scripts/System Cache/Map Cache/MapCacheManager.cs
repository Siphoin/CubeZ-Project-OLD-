using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCacheManager : MonoBehaviour
    {
    private const string FOLBER_NAME = "session/";

    private const string FILE_NAME = "game_session.json";

    private const string TAG_INSTANCE_OBJECT_MANAGER = "InstanceObjectManager";

    private const float AUTO_SAVE_TIME_OUT = 120f;

  private  InstanceObjectManager instanceObjectManager;

    public event Action onSaveSession;
        // Use this for initialization
        void Start()
        {
        instanceObjectManager = GameObject.FindGameObjectWithTag(TAG_INSTANCE_OBJECT_MANAGER).GetComponent<InstanceObjectManager>();
        CallInvokingEveryMethod(SaveSession, AUTO_SAVE_TIME_OUT);
        PlayerManager.Manager.Player.onDead += OffSave;

    }

    private void OffSave()
    {
        CancelInvoke();
        enabled = false;
    }

    private void OnApplicationQuit()
    {
        SaveSession();

    }

    private void SaveSession()
    {
        ClearGarbageData();
        string mapName = SceneManager.GetActiveScene().name;


        CacheObject[] cacheObjects = FindObjectsOfType<CacheObject>().Where(item => item.IsClone).ToArray();

        for (int i = 0; i < cacheObjects.Length; i++)
        {
            CacheObjectAdding(cacheObjects[i]);
        }

        GameCacheManager.gameCache.versionClient = Application.version;
        GameCacheManager.gameCache.mapName = mapName;

        GameCacheManager.gameCache.worldData = new WorldCacheData(WorldManager.Manager);

        var statsPlayer = PlayerManager.Manager.Player.CharacterStats.GetDictonaryNeeds();

        PlayerStatsCache playerStatsCache = new PlayerStatsCache();

        foreach (var stats in statsPlayer)
        {
            playerStatsCache.AddStatsNeed(stats.Value.needType, stats.Value.value);
        }

        if (PlayerManager.Manager.Player.CurrentWeapon != null)
        {
            GameCacheManager.gameCache.currentWeapon = new WeaponPlayerData(PlayerManager.Manager.Player.CurrentWeapon, GameCacheManager.gameCache.inventory.GetIndexByItem(PlayerManager.Manager.Player.CurrentWeapon.data));
        }

        GameCacheManager.gameCache.playerStats = playerStatsCache;

        GameCacheManager.gameCache.containerCacheObjects.objectsInstances = instanceObjectManager.GetInstanceObjects();

        ContainerDataObjects containerDataObjects = new ContainerDataObjects();

        CollectorCacheObjectDataBase[] collectors = FindObjectsOfType<CollectorCacheObjectDataBase>();

        for (int i = 0; i < collectors.Length; i++)
        {
            ICollectorCacheObjectData<CacheObjectData> collectorInterface = (ICollectorCacheObjectData<CacheObjectData>)collectors[i];
            CollectorCacheObjectDataBase collector = collectors[i];
            IEnumerable<CacheObjectData> data = collectorInterface.GetCollection();
            containerDataObjects.objectsData.Add(collector.IdCollection, data);
        }

        GameCacheManager.gameCache.dataContainerObjects = containerDataObjects;


        try
        {
        CacheSystem.SaveSerializeObject(FOLBER_NAME, FILE_NAME, GameCacheManager.gameCache, Newtonsoft.Json.Formatting.Indented);
            onSaveSession?.Invoke();
        }
        catch 
        {

            
        }

    }

    private void CacheObjectAdding (CacheObject cacheObject)
    {          
            SerializedObjectMono serializedObject = new SerializedObjectMono(cacheObject.gameObject, cacheObject.PrefabPath, cacheObject.Id);
                GameCacheManager.gameCache.containerCacheObjects.objectsClones.Add(cacheObject.Id, serializedObject);

    }

    private void ClearGarbageData ()
    {
        GameCache gameCache = GameCacheManager.gameCache;
        gameCache.containerCacheObjects.objectsClones.Clear();
        gameCache.dataContainerObjects.objectsData.Clear();
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }
}